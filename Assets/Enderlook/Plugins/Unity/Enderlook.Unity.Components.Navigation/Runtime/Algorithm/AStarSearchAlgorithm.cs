using Enderlook.Collections;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Enderlook.Unity.Components.Navigation
{
    public static class AStarSearchAlgorithm
    {
        /* https://code.msdn.microsoft.com/windowsdesktop/Dijkstras-Single-Soruce-69faddb3
         * https://www.geeksforgeeks.org/csharp-program-for-dijkstras-shortest-path-algorithm-greedy-algo-7/
         * https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
         * http://theory.stanford.edu/~amitp/GameProgramming/AStarComparison.html
         * https://www.redblobgames.com/pathfinding/a-star/introduction.html
         */

        private static Func<Vector2, Vector2, float> ChooseHeuristicFormula(DistanceFormula distanceFormula)
        {
            switch (distanceFormula)
            {
                case DistanceFormula.Euclidean:
                    return Distances
                        .CalculateEuclideanDistance;
                case DistanceFormula.Manhattan:
                    return Distances.CalculateManhattanDistance;
                case DistanceFormula.Chebyshov:
                    return Distances.CalculateChebyshovDistance;
                default:
                    return null;
            }
        }

        private static float AStarSearchPath(this NavigationGraph navigation, Node source, out Dictionary<Node, Connection> previous, Node target = null, DistanceFormula heuristicFormula = DistanceFormula.Euclidean) => navigation.AStarSearchPath(source, out previous, out Dictionary<Node, float> _, target, heuristicFormula);

        private static float AStarSearchPath(this NavigationGraph navigation, Node source, out Dictionary<Node, Connection> previous, out Dictionary<Node, float> distances, Node target = null, DistanceFormula heuristicFormula = DistanceFormula.Euclidean)
        {
            if (!navigation.Grid.Contains(source)) // Path will never find a way
            {
                previous = null;
                distances = null;
                return -1;
            }

            Func<Vector2, Vector2, float> Heuristic = ChooseHeuristicFormula(target == null ? DistanceFormula.None : heuristicFormula);

            previous = new Dictionary<Node, Connection>();
            distances = InitializeDistances(navigation, source);
            HashSet<Node> visited = new HashSet<Node>();
            BinaryHeapMin<Node, float> toVisit = new BinaryHeapMin<Node, float>();
            toVisit.Enqueue(source, 0);

            while (toVisit.TryDequeue(out Node node, out _))
            {
                if (visited.Contains(node))
                    continue;
                visited.Add(node);

                float distanceFromSource = distances[node];
                foreach (Connection connection in node.Connections)
                {
                    if (connection == null || !connection.IsActive)
                        continue;

                    Node neighbour = connection.end;
                    if (neighbour.IsActive)
                    {
                        float distance = Relax(distances, previous, connection, distanceFromSource);
                        toVisit.Enqueue(neighbour, distance + Heuristic?.Invoke(neighbour.position, target.position) ?? 0);
                        if (neighbour == target)
                            return distance;
                    }
                }
            }

            // Not found path
            return -1;
        }

        public static Dictionary<Node, Connection> SearchRawPath(this NavigationGraph navigation, Node source, Node target = null, DistanceFormula heuristicFormula = DistanceFormula.Euclidean)
        {
            navigation.AStarSearchPath(source, out Dictionary<Node, Connection> connections, target, heuristicFormula);
            return connections;
        }

        public static float SearchRawPath(this NavigationGraph navigation, Node source, out Dictionary<Node, Connection> connections, Node target = null, DistanceFormula heuristicFormula = DistanceFormula.Euclidean) => navigation.AStarSearchPath(source, out connections, target, heuristicFormula);

        public static float SearchRawPath(this NavigationGraph navigation, Node source, out Dictionary<Node, Connection> connections, out Dictionary<Node, float> distances, Node target = null, DistanceFormula heuristicFormula = DistanceFormula.Euclidean) => navigation.AStarSearchPath(source, out connections, out distances, target, heuristicFormula);

        public static float CalculatePathDistance(this NavigationGraph navigation, Node source, Node target = null, DistanceFormula heuristicFormula = DistanceFormula.Euclidean) => navigation.AStarSearchPath(source, out Dictionary<Node, Connection> connections, target, heuristicFormula);

        public static List<Connection> SearchPath(this NavigationGraph navigation, Node source, Node target = null, DistanceFormula heuristicFormula = DistanceFormula.Euclidean)
        {
            navigation.AStarSearchPath(source, out Dictionary<Node, Connection> connections, target, heuristicFormula);
            return FromPreviousDictionaryToListPath(connections, target);
        }

        public static float SearchPath(this NavigationGraph navigation, Node source, out List<Connection> path, Node target = null, DistanceFormula heuristicFormula = DistanceFormula.Euclidean)
        {
            float distance = navigation.AStarSearchPath(source, out Dictionary<Node, Connection> connections, target, heuristicFormula);
            path = FromPreviousDictionaryToListPath(connections, target);
            return distance;
        }

        public static List<Connection> FromPreviousDictionaryToListPath(Dictionary<Node, Connection> previous, Node target)
        {
            LinkedList<Connection> path = new LinkedList<Connection>();
            if (previous.Count == 0)
                return path.ToList();
            Node node = target;
            while (previous.TryGetValue(node, out Connection connection))
            {
                path.AddFirst(connection);
                node = connection.start;
            }
            return path.ToList();
        }

        public static (List<(Connection connection, float totalDistance)> path, HashSet<(Connection connection, float totlaDistance)> others) FromRawToPaths(Dictionary<Node, Connection> previous, Dictionary<Node, float> distances, Node target)
        {
            LinkedList<(Connection connection, float totalDistance)> path = new LinkedList<(Connection connection, float totalDistance)>();
            HashSet<Node> added = new HashSet<Node>();

            Node node = target;
            while (previous.TryGetValue(node, out Connection connection))
            {
                path.AddFirst((connection, distances[node]));
                added.Add(node);
                node = connection.start;
            }

            // TODO: If Unity implements .Net Standard 2.1, this should be constructed using new HashSet(int capacity) to avoid several reallocations
            HashSet<(Connection, float)> hashSet = new HashSet<(Connection, float)>(previous.Where(e => !added.Contains(e.Key)).Select(e => (e.Value, distances[e.Key])));
            return (path.ToList(), hashSet);
        }

        private static Dictionary<Node, float> InitializeDistances(NavigationGraph navigation, Node source)
        {
            Dictionary<Node, float> distances = new Dictionary<Node, float>();
            foreach (Node node in navigation.Grid)
                distances.Add(node, float.MaxValue);
            distances[source] = 0;
            return distances;
        }

        private static float Relax(Dictionary<Node, float> distances, Dictionary<Node, Connection> previous, Connection connection, float distanceFromSource)
        {
            float newDistance = connection.Distance + distanceFromSource;
            if (newDistance < distances[connection.end])
            {
                distances[connection.end] = newDistance;
                previous[connection.end] = connection;
            }
            return newDistance;
        }

        private static void Relax(Dictionary<Node, float> distances, Dictionary<Node, Connection> path, Connection connection) => Relax(distances, path, connection, distances[connection.end]);
    }
}
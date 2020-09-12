using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    /// <inheritdoc cref="AStar{TPath, TNode, TEdge}"/>
    public class AStar : AStar<Path, Node, Edge>
    {
        /// <inheritdoc cref="AStar{TPath, TNode, TEdge}.Heuristic"/>
        protected sealed override float Heuristic(Node to, Node from)
            => Vector3.Distance(to.Position, from.Position);
    }
}
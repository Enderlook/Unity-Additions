using Enderlook.Exceptions;

using System;

using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    /// <summary>
    /// Represents a 2D graph.
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "Graph", menuName = "Enderlook/Navigation/Graph 2D")]
    public class Graph : Graph<Node, Edge, Vector2, RootSerializer>
    {
        /// <summary>
        /// How nodes are oriented.
        /// </summary>
        public enum Orientation
        {
            XY_TO_XY,
            XY_TO_XZ,
            XY_TO_YZ,
        }

#pragma warning disable CS0649
        [SerializeField]
        private Orientation orientation;
#pragma warning restore CS0649

        /// <summary>
        /// Get the normal according to <see cref="orientation"/>.
        /// </summary>
        /// <returns>Normal.</returns>
        public Vector3 GetNormal()
        {
            switch (orientation)
            {
                case Orientation.XY_TO_XY:
                    return Vector3.forward;
                case Orientation.XY_TO_XZ:
                    return Vector3.up;
                case Orientation.XY_TO_YZ:
                    return Vector3.right;
                default:
#if UNITY_EDITOR
                    throw new ImpossibleStateException();
#else
                    return Vector3.zero;
#endif
            }
        }

        /// <summary>
        /// Convert a world vector into a local vector using <see cref="orientation"/>.
        /// </summary>
        /// <param name="world">Vector to convert.</param>
        /// <returns>Converted vector.</returns>
        public Vector2 TweakOrientationToLocal(Vector3 world)
        {
            switch (orientation)
            {
                case Orientation.XY_TO_XY:
                    return world;
                case Orientation.XY_TO_XZ:
                    return new Vector2(world.x, world.z);
                case Orientation.XY_TO_YZ:
                    return new Vector2(world.y, world.z);
                default:
#if UNITY_EDITOR
                    throw new ImpossibleStateException();
#else
                    return Vector3.zero;
#endif
            }
        }

        /// <summary>
        /// Convert a local vector into a world vector using <see cref="orientation"/>.
        /// </summary>
        /// <param name="local">Vector to convert.</param>
        /// <returns>Converted vector.</returns>
        public Vector3 TweakOrientationToWorld(Vector2 local)
        {
            switch (orientation)
            {
                case Orientation.XY_TO_XY:
                    return local;
                case Orientation.XY_TO_XZ:
                    return new Vector3(local.x, 0, local.y);
                case Orientation.XY_TO_YZ:
                    return new Vector3(0, local.x, local.y);
                default:
#if UNITY_EDITOR
                    throw new ImpossibleStateException();
#else
                    return Vector3.zero;
#endif
            }
        }

        /// <inheritdoc cref="Graph{TNode, TEdge, T}.GetDistance(TNode, T)"/>
        protected override float GetDistance(Node node, Vector2 value)
            => Vector2.Distance(node.Position, value);
    }
}
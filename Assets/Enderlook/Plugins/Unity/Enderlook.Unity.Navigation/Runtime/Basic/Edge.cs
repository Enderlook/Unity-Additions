using Enderlook.Unity.Attributes;
using Enderlook.Unity.Attributes.AttributeUsage;

using System;

using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    /// <inheritdoc cref="IEdge{TNode}"/>
    [Serializable]
    public abstract class Edge<TNode> : IEdge<TNode>
    {
        /// <inheritdoc cref="IEdge{TNode}.From"/>
        [field: SerializeField, IsProperty, DoNotCheck(typeof(IsPropertyAttribute))]
        public TNode From { get; protected set; }

        /// <inheritdoc cref="IEdge{TNode}.To"/>
        [field: SerializeField, IsProperty, DoNotCheck(typeof(IsPropertyAttribute))]
        public TNode To { get; protected set; }

        /// <inheritdoc cref="IEdge{TNode}.Cost"/>
        public abstract float Cost { get; }
    }
}
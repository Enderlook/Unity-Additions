namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Representation of a writeable path.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public interface IPathWriter<TNode, TEdge>
    {
        /// <summary>
        /// Clears all the stored information from the path.
        /// </summary>
        void Reset();

        /// <summary>
        /// Set the cost to traveling from start to <paramref name="to"/>.
        /// </summary>
        /// <param name="to">Target to travel.</param>
        /// <param name="cost">Cost to travel to <paramref name="to"/>.</param>
        void SetCostTo(TNode to, float cost);

        /// <summary>
        /// Set the start and end nodes.
        /// </summary>
        /// <param name="from">Start node.</param>
        /// <param name="to">Target node.</param>
        void SetFromTo(TNode from, TNode to);

        /// <summary>
        /// Try to get the cost of traveling from start to <paramref name="node"/>.
        /// </summary>
        /// <param name="node">Target node to reach.</param>
        /// <param name="cost">Cumulative cost of travelling from start to <paramref name="node"/>.</param>
        /// <returns>Whenever the cost was already calculated or not.</returns>
        bool TryGetCostTo(TNode node, out float cost);

        /// <summary>
        /// Set which <see cref="Edge{TNode}"/> is required to reach to <paramref name="to"/>.
        /// </summary>
        /// <param name="to">Target node.</param>
        /// <param name="edge">From which edge can be reached to <paramref name="to"/>.</param>
        void SetEdgeTo(TNode to, TEdge edge);

        /// <summary>
        /// Determines that the path has finished calculation.
        /// </summary>
        /// <param name="success">Whenever the path was or not found.</param>
        void Complete(bool success);
    }
}
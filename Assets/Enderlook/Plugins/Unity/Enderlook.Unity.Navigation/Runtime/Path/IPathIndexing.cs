using System;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Representation of an indexable path.
    /// </summary>
    /// <typeparam name="T">Value to get.</typeparam>
    public interface IPathIndexing<T>
    {
        /// <summary>
        /// Get node at index <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Index to look for node.</param>
        /// <returns>Node at index <paramref name="index"/>.</returns>
        /// <exception cref="IndexOutOfRangeException">When <paramref name="index"/> is not in range.</exception>
        T GetValueAt(int index);

        /// <summary>
        /// TRy get node at index <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Index to look for node.</param>
        /// <param name="value">Node at index <paramref name="index"/>, if any</param>
        /// <returns>Whenever there was a node or not.</returns>
        bool TryGetValueAt(int index, out T value);

        /// <summary>
        /// Try to get next node.
        /// </summary>
        /// <param name="index">Old index.</param>
        /// <param name="value">Node at index <c><paramref name="index"/> + 1</c>, if any.</param>
        /// <returns>Whenever there was a node or not.</returns>
        bool TryGetNext(ref int index, out T value);
    }
}
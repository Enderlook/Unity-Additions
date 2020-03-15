using Enderlook.Unity.Interfaces;

namespace Enderlook.Unity.Atoms
{
    public interface IGetSet<T> : IGet<T>
    {
        /// <summary>
        /// Stores an value in this object.
        /// </summary>
        /// <param name="value">Value to store.</param>
        void SetValue(T value);
    }
}
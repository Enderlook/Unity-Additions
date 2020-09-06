using Enderlook.Unity.Utils.Interfaces;

namespace Enderlook.Unity.Atoms
{
    public interface IGetSet<TValue> : IGet<TValue>
    {
        /// <summary>
        /// Stores an value in this object.
        /// </summary>
        /// <param name="newValue">Value to store.</param>
        void SetValue(TValue newValue);
    }
}
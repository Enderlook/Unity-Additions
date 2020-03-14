namespace Enderlook.Unity.ScriptableObjects.Atoms
{
    public interface IGet<T>
    {
        /// <summary>
        /// Get the value stored in this object.
        /// </summary>
        /// <returns>Value stored in this object.</returns>
        T GetValue();
    }
}
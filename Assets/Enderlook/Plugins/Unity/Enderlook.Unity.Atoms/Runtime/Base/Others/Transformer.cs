using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    public abstract class Transformer<TValue> : ScriptableObject
    {
        /// <summary>
        /// Executes the behavior of this instance.
        /// </summary>
        /// <param name="value">Parameter used to execute.</param>
        /// <returns>Result of execution.</returns>
        public abstract TValue Transform(TValue value);
    }
}
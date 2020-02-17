using UnityEngine;

using Random = UnityEngine.Random;

namespace Enderlook.Unity.Serializables.Ranges
{
    public abstract class VectorRangeTwo
    {
        /// <summary>
        /// Whenever it should return only the initial vector or a random vector made by two vectors.
        /// </summary>
        [Tooltip("Whenever it should return only the initial vector or a random vector made by two vectors.")]
        public bool isRandom = true;

        /// <summary>
        /// Return the start parameter in <see cref="Vector3"/>
        /// </summary>
        protected abstract Vector3 StartVector3 { get; set; }

        /// <summary>
        /// Return the end parameter in <see cref="Vector3"/>
        /// </summary>
        protected abstract Vector3 EndVector3 { get; set; }

        /// <summary>
        /// Whenever it should return only the initial vector or a random vector made by two vectors.
        /// </summary>
        protected Vector3 Vector3 {
            get {
                if (isRandom)
                    return new Vector3(Random.Range(StartVector3.x, EndVector3.x),
                        Random.Range(StartVector3.y, EndVector3.y),
                        Random.Range(StartVector3.z, EndVector3.z));
                else
                    return StartVector3;
            }
        }

        /// <summary>
        /// Return the start parameter in <see cref="Vector2"/>
        /// </summary>
        protected Vector2 StartVector2 => StartVector3;

        /// <summary>
        /// Return the end parameter in <see cref="Vector2"/>
        /// </summary>
        protected Vector2 EndVector2 => EndVector3;

        /// <summary>
        /// Return a <seealso cref="Vector2"/> position. If <see cref="IsRandom"/> is <see langword="true"/>, it will return a random <seealso cref="UnityEngine.Vector2"/> between the <see cref="StartVector2"/> and the <see cref="EndVector2"/>. On <see langword="false"/> it will return the position of the <see cref="StartVector2"/>.
        /// </summary>
        protected Vector2 Vector2 {
            get {
                if (isRandom)
                    return new Vector2(Random.Range(StartVector2.x, EndVector2.x),
                        Random.Range(StartVector2.y, EndVector2.y));
                else
                    return StartVector2;
            }
        }
    }
}
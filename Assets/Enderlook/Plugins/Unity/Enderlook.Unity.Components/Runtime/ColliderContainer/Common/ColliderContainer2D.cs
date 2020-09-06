using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <inheritdoc cref="IColliderContainer"/>
    [AddComponentMenu("Enderlook/Collider Container 2D")]
    public class ColliderContainer2D : BaseColliderContainer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnTriggerEnter2D(Collider2D other) => Add(other.gameObject);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnTriggerExit2D(Collider2D other) => Remove(other.gameObject);
    }
}

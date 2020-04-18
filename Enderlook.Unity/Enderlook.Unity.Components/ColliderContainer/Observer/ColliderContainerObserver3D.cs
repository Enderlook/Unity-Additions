using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <inheritdoc cref="IColliderContainerObserver"/>
    [AddComponentMenu("Enderlook/Collider Container Observer 3D")]
    public class ColliderContainerObserver3D : BaseColliderContainerObserver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnTriggerEnter(Collider other) => Add(other.gameObject);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnTriggerExit(Collider other) => Remove(other.gameObject);
    }
}

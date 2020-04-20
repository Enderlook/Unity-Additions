using System.Linq;

using UnityEngine;

namespace Enderlook.Unity.Components.Destroy
{
    [AddComponentMenu("Enderlook/Destroyers/Destroy When Particle System Is Not Alive")]
    public class DestroyWhenParticleSystemIsNotAlive : MonoBehaviour
    {
        [SerializeField, Tooltip("Whenever it should also include nested particle systems.")]
        private bool includeNestedParticleSystems;

        private ParticleSystem[] particleSystems;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Awake()
            => particleSystems = (includeNestedParticleSystems ? GetComponentsInChildren<ParticleSystem>() : GetComponents<ParticleSystem>())
            .OrderByDescending(e => e.main.duration).ToArray();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Update()
        {
            bool destroy = true;
            for (int i = 0; i < particleSystems.Length; i++)
            {
                if (particleSystems[i].IsAlive())
                    destroy = false;
            }
            if (destroy)
                Destroy(gameObject);
        }
    }
}

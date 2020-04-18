using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <summary>
    /// Configure the <see cref="GameObject"/> where this <see cref="Component"/> is attached to, to not destroy when Unity loads a new scene.
    /// </summary>
    [AddComponentMenu("Enderlook/Don't Destroy On Load")]
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);
    }
}

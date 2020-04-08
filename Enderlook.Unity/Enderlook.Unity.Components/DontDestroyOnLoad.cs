using UnityEngine;

namespace Enderlook.Unity.Components
{
    [AddComponentMenu("Enderlook/Don't Destroy On Load")]
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);
    }
}

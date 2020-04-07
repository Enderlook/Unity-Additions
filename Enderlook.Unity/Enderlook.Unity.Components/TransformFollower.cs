using UnityEngine;

namespace Enderlook.Unity.Components
{
    [AddComponentMenu("Enderlook/Transform Follower"), DefaultExecutionOrder(100)]
    public class TransformFollower : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField, Tooltip("Whenever it will follow position locally or globally.")]
        private bool isLocalPosition;
        [SerializeField, Tooltip("Whenever it will follow the X axis position.")]
        private bool followPosX = true;
        [SerializeField, Tooltip("Whenever it will follow the Y axis position.")]
        private bool followPosY = true;
        [SerializeField, Tooltip("Whenever it will follow the Z axis position.")]
        private bool followPosZ = true;

        [SerializeField, Tooltip("Whenever it will follow rotation locally or globally.")]
        private bool isLocalRotation;
        [SerializeField, Tooltip("Whenever it will follow the X axis rotation.")]
        private bool followRotX = true;
        [SerializeField, Tooltip("Whenever it will follow the Y axis rotation.")]
        private bool followRotY = true;
        [SerializeField, Tooltip("Whenever it will follow the Z axis rotation.")]
        private bool followRotZ = true;

        [SerializeField, Tooltip("Whenever it will follow the X axis scale.")]
        private bool followScaleX = true;
        [SerializeField, Tooltip("Whenever it will follow the Y axis scale.")]
        private bool followScaleY = true;
        [SerializeField, Tooltip("Whenever it will follow the Z axis scale.")]
        private bool followScaleZ = true;

        [Header("Build")]
        [SerializeField, Tooltip("Following transform")]
        private Transform transformToFollow;

        private Vector3 originalPosition, originalRotation, originalScale;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Awake()
        {
            Vector3 targetPosition = isLocalPosition ? transformToFollow.localPosition : transformToFollow.position;
            Vector3 targetRotation = (isLocalRotation ? transformToFollow.localRotation : transformToFollow.rotation).eulerAngles;
            Vector3 targetScale = transformToFollow.localScale;

            Vector3 ourPosition = isLocalPosition ? transform.localPosition : transform.position;
            Vector3 ourRotation = (isLocalRotation ? transform.localRotation : transform.rotation).eulerAngles;
            Vector3 ourScale = transform.localScale;

            originalPosition = new Vector3(
                followPosX ? ourPosition.x - targetPosition.x : ourPosition.x,
                followPosY ? ourPosition.y - targetPosition.y : ourPosition.y,
                followPosZ ? ourPosition.z - targetPosition.z : ourPosition.z
            );

            originalRotation = new Vector3(
                followRotX ? ourRotation.x - targetRotation.x : ourRotation.x,
                followRotY ? ourRotation.y - targetRotation.y : ourRotation.y,
                followRotZ ? ourRotation.z - targetRotation.z : ourRotation.z
            );

            originalScale = new Vector3(
                followScaleX ? ourScale.x / targetScale.x : ourScale.x,
                followScaleY ? ourScale.y / targetScale.y : ourScale.y,
                followScaleZ ? ourScale.z / targetScale.z : ourScale.z
            );
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Update()
        {
            Vector3 targetPosition = isLocalPosition ? transformToFollow.localPosition : transformToFollow.position;
            Vector3 targetRotation = (isLocalRotation ? transformToFollow.localRotation : transformToFollow.rotation).eulerAngles;
            Vector3 targetScale = transformToFollow.localScale;

            Vector3 newPosition = new Vector3(
                followPosX ? targetPosition.x + originalPosition.x : originalPosition.x,
                followPosY ? targetPosition.y + originalPosition.y : originalPosition.y,
                followPosZ ? targetPosition.z + originalPosition.z : originalPosition.z
            );

            Vector3 newRotation = new Vector3(
                followRotX ? targetRotation.x + originalRotation.x : originalRotation.x,
                followRotY ? targetRotation.y + originalRotation.y : originalRotation.y,
                followRotZ ? targetRotation.z + originalRotation.z : originalRotation.z
            );

            Vector3 newScale = new Vector3(
                followScaleX ? targetScale.x * originalScale.x : originalScale.x,
                followScaleY ? targetScale.y * originalScale.y : originalScale.y,
                followScaleZ ? targetScale.z * originalScale.z : originalScale.z
            );

            if (isLocalPosition)
                transform.localPosition = newPosition;
            else
                transform.position = newPosition;

            if (isLocalRotation)
                transform.localRotation = Quaternion.Euler(newRotation);
            else
                transform.rotation = Quaternion.Euler(newRotation);

            transform.localScale = newScale;
        }
    }
}
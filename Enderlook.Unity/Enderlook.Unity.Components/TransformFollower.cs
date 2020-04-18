using System.Runtime.CompilerServices;

using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <summary>
    /// Make the <see cref="GameObject"/> where this <see cref="Component"/> is attached to, to follow another <see cref="GameObject"/> in position, rotation and or scale.
    /// </summary>
    [AddComponentMenu("Enderlook/Transform Follower"), DefaultExecutionOrder(100)]
    public class TransformFollower : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField, Tooltip("Whenever it will follow position locally or globally.")]
        private bool isLocalPosition;

        /// <summary>
        /// Whenever it will follow position locally or globally.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool IsLocalPosition {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => isLocalPosition;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (isLocalPosition != value)
                {
                    // Avoid racing condition
                    Update();
                    isLocalPosition = value;
                    SetPosition();
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the X axis position.")]
        private bool followPosX = true;

        /// <summary>
        /// Whenever it will follow the X axis position.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowPosX {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followPosX;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followPosX != value)
                {
                    // Avoid racing condition
                    Update();
                    followPosX = value;
                    originalPosition.x = Set(value, OurPosition.x, TargetPosition.x);
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the Y axis position.")]
        private bool followPosY = true;

        /// <summary>
        /// Whenever it will follow the Y axis position.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowPosY {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followPosY;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followPosY != value)
                {
                    // Avoid racing condition
                    Update();
                    followPosY = value;
                    originalPosition.y = Set(value, OurPosition.y, TargetPosition.y);
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the Z axis position.")]
        private bool followPosZ = true;

        /// <summary>
        /// Whenever it will follow the Z axis position.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowPosZ {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followPosZ;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followPosZ != value)
                {
                    // Avoid racing condition
                    Update();
                    followPosZ = value;
                    originalPosition.z = Set(value, OurPosition.z, TargetPosition.z);
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow rotation locally or globally.")]
        private bool isLocalRotation;

        /// <summary>
        /// Whenever it will follow rotation locally or globally.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool IsLocalRotation {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => isLocalRotation;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (isLocalRotation != value)
                {
                    // Avoid racing condition
                    Update();
                    isLocalRotation = value;
                    SetRotation();
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the X axis rotation.")]
        private bool followRotX = true;

        /// <summary>
        /// Whenever it will follow the X axis rotation.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowRotX {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followRotX;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followRotX != value)
                {
                    // Avoid racing condition
                    Update();
                    followRotX = value;
                    originalRotation.x = Set(value, OurRotation.x, TargetRotation.x);
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the Y axis rotation.")]
        private bool followRotY = true;

        /// <summary>
        /// Whenever it will follow the Y axis rotation.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowRotY {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followRotY;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followRotY != value)
                {
                    // Avoid racing condition
                    Update();
                    followRotY = value;
                    originalRotation.y = Set(value, OurRotation.y, TargetRotation.y);
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the Z axis rotation.")]
        private bool followRotZ = true;

        /// <summary>
        /// Whenever it will follow the Z axis rotation.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowRotZ {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followRotZ;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followRotZ != value)
                {
                    // Avoid racing condition
                    Update();
                    followRotZ = value;
                    originalRotation.y = Set(value, OurRotation.y, TargetRotation.y);
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the X axis scale.")]
        private bool followScaleX = true;

        /// <summary>
        /// Whenever it will follow the X axis scale.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowScaleX {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followScaleX;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followScaleX != value)
                {
                    // Avoid racing condition
                    Update();
                    followScaleX = value;
                    originalScale.x = Set(value, transform.localScale.x, transformToFollow.localScale.x);
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the Y axis scale.")]
        private bool followScaleY = true;

        /// <summary>
        /// Whenever it will follow the Y axis scale.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowScaleY {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followScaleY;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followScaleY != value)
                {
                    // Avoid racing condition
                    Update();
                    followScaleY = value;
                    originalScale.y = Set(value, transform.localScale.y, transformToFollow.localScale.y);
                }
            }
        }

        [SerializeField, Tooltip("Whenever it will follow the Z axis scale.")]
        private bool followScaleZ = true;

        /// <summary>
        /// Whenever it will follow the Z axis scale.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public bool FollowScaleZ {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => followScaleZ;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (followScaleZ != value)
                {
                    // Avoid racing condition
                    Update();
                    followScaleZ = value;
                    originalScale.z = Set(value, transform.localScale.z, transformToFollow.localScale.z);
                }
            }
        }

        [Header("Build")]
        [SerializeField, Tooltip("Transform that will be followed.")]
        private Transform transformToFollow;

        /// <summary>
        /// Transform that will be followed.<br/>
        /// Changing this value may lead to minor math inaccuracies.
        /// </summary>
        public Transform TransformToFollow {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => transformToFollow;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (transformToFollow != value)
                {
                    transformToFollow = value;
                    Start();
                }
            }
        }

        private Vector3 originalPosition, originalRotation, originalScale;

        private Vector3 OurRotation {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (isLocalRotation ? transform.localRotation : transform.rotation).eulerAngles;
        }

        private Vector3 OurPosition {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => isLocalPosition ? transform.localPosition : transform.position;
        }

        private Vector3 TargetRotation {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (isLocalRotation ? transformToFollow.localRotation : transformToFollow.rotation).eulerAngles;
        }

        private Vector3 TargetPosition {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => isLocalPosition ? transformToFollow.localPosition : transformToFollow.position;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Start()
        {
            SetRotation();
            SetPosition();

            Vector3 targetScale = transformToFollow.localScale;
            Vector3 ourScale = transform.localScale;
            originalScale = new Vector3(
                Set(followScaleX, ourScale.x, targetScale.x),
                Set(followScaleY, ourScale.y, targetScale.y),
                Set(followScaleZ, ourScale.z, targetScale.z)
            );
        }

        private void SetRotation()
        {
            Vector3 targetRotation = TargetRotation;
            Vector3 ourRotation = OurRotation;
            originalRotation = new Vector3(
                Set(followRotX, ourRotation.x, targetRotation.x),
                Set(followRotY, ourRotation.y, targetRotation.y),
                Set(followRotZ, ourRotation.z, targetRotation.z)
            );
        }

        private void SetPosition()
        {
            Vector3 targetPosition = TargetPosition;
            Vector3 ourPosition = OurPosition;
            originalPosition = new Vector3(
                Set(followPosX, ourPosition.x, targetPosition.x),
                Set(followPosY, ourPosition.y, targetPosition.y),
                Set(followPosZ, ourPosition.z, targetPosition.z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float Set(bool follow, float ourPosition, float targetPosition)
            => follow ? ourPosition - targetPosition : ourPosition;

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

        /// <summary>
        /// Add a <see cref="TransformFollower"/> component to <paramref name="gameObject"/>.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> where <see cref="TransformFollower"/> is added.</param>
        /// <param name="transformToFollow">Transform that will be followed.</param>
        /// <param name="isLocalPosition">Whenever it will follow position locally or globally.</param>
        /// <param name="followPosX">Whenever it will follow the X axis position.</param>
        /// <param name="followPosY">Whenever it will follow the Y axis position.</param>
        /// <param name="followPosZ">Whenever it will follow the Z axis position.</param>
        /// <param name="isLocalRotation">Whenever it will follow rotation locally or globally.</param>
        /// <param name="followRotX">Whenever it will follow the X axis rotation.</param>
        /// <param name="followRotY">Whenever it will follow the Y axis rotation.</param>
        /// <param name="followRotZ">Whenever it will follow the Z axis rotation.</param>
        /// <param name="followScaleX">Whenever it will follow the X axis scale.</param>
        /// <param name="followScaleY">Whenever it will follow the Y axis scale.</param>
        /// <param name="followScaleZ">Whenever it will follow the Z axis scale.</param>
        /// <returns>Added component.</returns>
        public static TransformFollower AddTransformFollower(
            GameObject gameObject, Transform transformToFollow,
            bool isLocalPosition, bool followPosX, bool followPosY, bool followPosZ,
            bool isLocalRotation, bool followRotX, bool followRotY, bool followRotZ,
            bool followScaleX, bool followScaleY, bool followScaleZ)
        {
            TransformFollower transformFollower = gameObject.AddComponent<TransformFollower>();
            transformFollower.transformToFollow = transformToFollow;
            transformFollower.isLocalPosition = isLocalPosition;
            transformFollower.isLocalRotation = isLocalRotation;
            transformFollower.followPosX = followPosX;
            transformFollower.followPosY = followPosY;
            transformFollower.followPosZ = followPosZ;
            transformFollower.followRotX = followRotX;
            transformFollower.followRotY = followRotY;
            transformFollower.followRotZ = followRotZ;
            transformFollower.followScaleX = followScaleX;
            transformFollower.followScaleY = followScaleY;
            transformFollower.followScaleZ = followScaleZ;
            return transformFollower;
        }
    }
}
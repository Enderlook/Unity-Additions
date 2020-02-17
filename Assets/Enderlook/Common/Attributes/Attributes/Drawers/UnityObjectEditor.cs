using UnityEditor;

namespace Enderlook.Unity.Attributes
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    internal class UnityObjectEditor : Editor { }
    // This dummy is required by ExpandableDrawer
}
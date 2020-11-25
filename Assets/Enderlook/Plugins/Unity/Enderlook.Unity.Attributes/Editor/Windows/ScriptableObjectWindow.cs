using Enderlook.Enumerables;
using Enderlook.Reflection;
using Enderlook.Unity.Utils.UnityEditor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEditor;

using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Enderlook.Unity.Attributes
{
    internal class ScriptableObjectWindow : EditorWindow
    {
        private static GUIContent CONTEXT_PROPERTY_MENU = new GUIContent("Scriptable Object Menu", "Open the Scriptable Object Menu.");
        private static readonly GUIContent TITLE_CONTENT = new GUIContent("Scriptable Object Manager");
        private static readonly GUIContent INSTANTIATE_TYPE_CONTENT = new GUIContent("Instance type", "Scriptable object instance type to create.");
        private static readonly GUIContent ADD_TO_ASSET_CONTENT = new GUIContent("Instantiate in field and add to asset", "Create and instance and assign to field. The scriptable object will be added to the asset or prefab file.");
        private static readonly GUIContent ADD_TO_SCENE_CONTENT = new GUIContent("Instantiate in field and add to scene", "Create and instance and assign to field. The scriptable object will be added to the scene file.");
        private static readonly GUIContent PATH_TO_FILE_CONTENT = new GUIContent("Path to file", "Path where the asset file is stored or will be saved.");
        private static readonly GUIContent SAVE_ASSET_CONTENT = new GUIContent("Instantiate in field and save asset", "Create and instance, assign to field and save it as an asset file.");
        private static readonly GUIContent CLEAN_FIELD = new GUIContent("Clean field", "Remove current instance of field.");
        private static readonly string[] EXTENSIONS = new string[] { ".asset", ".prefab", ".scene" };

        private const string DEFAULT_PATH = "Resources/";

        private static readonly Type root = typeof(UnityObject); // We don't use ScriptableObjet so this can work with RestrictTypeCheckingAttribute

        // Pool values
        private static readonly List<Type> tmpList = new List<Type>();
        private static readonly List<Type> tmpList2 = new List<Type>();
        private static readonly Stack<Type> tmpStack = new Stack<Type>();
        private static readonly char[] splitBy = new[] { '/' };
        private static readonly Func<Type, string> GetName = (Type type) => type.Name;

        private static ILookup<Type, Type> derivedTypes;

        private List<Type> allowedTypes;
        private string[] allowedTypesNames;
        private int index;

        private string path = DEFAULT_PATH;
        private string scriptableObjectName;
        private string propertyPath;
        private bool scriptableObjectNameAuto = true;

        private HideFlags scriptableObjectHideFlags;

        private ScriptableObject oldScriptableObject;

        private SerializedPropertyWrapper propertyWrapper;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        [InitializeOnLoadMethod]
        private static void AddContextualPropertyMenu()
            => ContextualPropertyMenu.contextualPropertyMenu += (GenericMenu menu, SerializedProperty property) =>
            {
                if (property.propertyPath.EndsWith(".Array.Size"))
                    return;

                FieldInfo fieldInfo = property.GetFieldInfo();
                Type fieldType = fieldInfo.FieldType;
                if (fieldType.IsArrayOrList())
                    fieldType = fieldType.GetElementTypeOfArrayOrList();

                if (typeof(UnityObject).IsAssignableFrom(fieldType))
                    menu.AddItem(
                        CONTEXT_PROPERTY_MENU,
                        false,
                        () => CreateWindow(property, fieldInfo)
                    );
            };

        private static void InitializeDerivedTypes()
        {
            // We don't use AssembliesHelper.GetAllAssembliesOfPlayerAndEditorAssemblies() because that doesn't include dll files from Assets folder
            List<(Assembly assembly, Exception[] exceptions)> errors = new List<(Assembly assembly, Exception[] exceptions)>();
            Debug.Assert(tmpStack.Count == 0);
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.TryGetTypes(out IEnumerable<Type> loadedTypes, out Exception[] exceptions))
                    errors.Add((assembly, exceptions));

                foreach (Type type in loadedTypes)
                    if (root.IsAssignableFrom(type))
                        tmpStack.Push(type);
            }

            if (errors.Count > 0)
            {
                Debug.LogError("While getting Types from loaded assemblies in Scriptable Object Window the following exceptions occurred:");
                foreach ((Assembly assembly, Exception[] exceptions) in errors)
                    foreach (Exception exception in exceptions)
                        Debug.LogWarning($"{assembly.FullName}: {exception.Message}.");
            }

            HashSet<KeyValuePair<Type, Type>> typesKV = new HashSet<KeyValuePair<Type, Type>>();

            while (tmpStack.TryPop(out Type result))
            {
                typesKV.Add(new KeyValuePair<Type, Type>(result, result));
                Type baseType = result.BaseType;
                if (root.IsAssignableFrom(baseType))
                {
                    typesKV.Add(new KeyValuePair<Type, Type>(baseType, result));
                    tmpStack.Push(baseType);
                }
            }

            derivedTypes = typesKV.ToLookup();
        }

        private static IEnumerable<Type> GetDerivedTypes(Type type)
        {
            Debug.Assert(tmpStack.Count == 0);
            foreach (Type t in derivedTypes[type])
                if (t != type)
                    tmpStack.Push(t);

            tmpList.Clear();
            tmpList.Add(type);
            tmpList.AddRange(tmpStack);

            while (tmpStack.TryPop(out Type result))
            {
                foreach (Type t in derivedTypes[result])
                {
                    if (t != result)
                    {
                        tmpStack.Push(t);
                        tmpList.Add(t);
                    }
                }
            }

            return tmpList.OrderBy(GetName);
        }

        private static void CreateWindow(SerializedProperty property, FieldInfo fieldInfo)
        {
            if (derivedTypes == null)
                InitializeDerivedTypes();

            ScriptableObjectWindow window = GetWindow<ScriptableObjectWindow>();

            window.propertyPath = AssetDatabaseHelper.GetAssetPath(property);
            window.propertyWrapper = new SerializedPropertyWrapper(property, fieldInfo);

            Debug.Assert(tmpList2.Count == 0);
            foreach (Type type in GetDerivedTypes(window.propertyWrapper.Type))
                if (!type.IsAbstract)
                    tmpList2.Add(type);

            // RestrictTypeAttribute compatibility
            RestrictTypeAttribute restrictTypeAttribute = fieldInfo.GetCustomAttribute<RestrictTypeAttribute>();
            List<Type> allowedTypes;
            if (restrictTypeAttribute != null)
            {
                allowedTypes = new List<Type>();
                foreach (Type type in tmpList2)
                    if (restrictTypeAttribute.CheckIfTypeIsAllowed(type))
                        allowedTypes.Add(type);
            }
            else
                allowedTypes = new List<Type>(tmpList2);

            tmpList2.Clear();

            window.allowedTypes = allowedTypes;

            window.allowedTypesNames = new string[allowedTypes.Count];

            for (int i = 0; i < allowedTypes.Count; i++)
                window.allowedTypesNames[i] = allowedTypes[i].Name;

            window.index = window.GetIndex(window.propertyWrapper.Type);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnGUI()
        {
            titleContent = TITLE_CONTENT;

            ScriptableObject scriptableObject = (ScriptableObject)propertyWrapper.Accessors?.Get();
            bool hasScriptableObject = scriptableObject != null;

            if (oldScriptableObject != scriptableObject)
            {
                oldScriptableObject = scriptableObject;

                if (hasScriptableObject)
                {
                    index = GetIndex(scriptableObject.GetType());
                    scriptableObjectHideFlags = scriptableObject.hideFlags;
                    scriptableObjectName = scriptableObject.name;
                }
            }

            // Instance Type
            EditorGUI.BeginDisabledGroup(hasScriptableObject);
            index = EditorGUILayout.Popup(INSTANTIATE_TYPE_CONTENT, index, allowedTypesNames);
            EditorGUI.EndDisabledGroup();

            // Get Name
            if (scriptableObjectNameAuto && !hasScriptableObject)
                scriptableObjectName = path.Split(splitBy).Last().Split(EXTENSIONS, StringSplitOptions.None).First();

            UnityObject targetObject = propertyWrapper.Property.serializedObject.targetObject;

            if (hasScriptableObject)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.TextField("Name of Scriptable Object", scriptableObject.name);
                EditorGUI.EndDisabledGroup();
                scriptableObjectName = EditorGUILayout.TextField("New name", scriptableObjectName);
                scriptableObjectHideFlags = (HideFlags)EditorGUILayout.EnumFlagsField("New Hide Flags", scriptableObjectHideFlags);

                if (GUILayout.Button("Rename Scriptable Object"))
                {
                    scriptableObject.name = scriptableObjectName;
                    propertyWrapper.Property.serializedObject.ApplyModifiedProperties();
                }
            }
            else
            {
                string old = scriptableObjectName;
                scriptableObjectName = EditorGUILayout.TextField("Name of Scriptable Object", scriptableObjectName);
                scriptableObjectNameAuto = scriptableObjectNameAuto && scriptableObjectName == old;
            }

            if (!hasScriptableObject)
            {
                // Create
                EditorGUI.BeginDisabledGroup(index == -1 || string.IsNullOrEmpty(scriptableObjectName));
                if (GUILayout.Button(ADD_TO_ASSET_CONTENT))
                {
                    scriptableObject = InstantiateAndApply(targetObject, scriptableObjectName);
                    AssetDatabase.AddObjectToAsset(scriptableObject, propertyPath);
                    AssetDatabase.Refresh();
                }
                if (GUILayout.Button(ADD_TO_SCENE_CONTENT))
                {
                    scriptableObject = InstantiateAndApply(targetObject, scriptableObjectName);
                }
                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.Space();

            // Get path to file
            EditorGUI.BeginDisabledGroup(hasScriptableObject);
            string pathToAsset = AssetDatabase.GetAssetPath(scriptableObject);
            bool hasAsset = !string.IsNullOrEmpty(pathToAsset);
            path = hasAsset ? pathToAsset : path;
            path = EditorGUILayout.TextField(PATH_TO_FILE_CONTENT, path);
            string _path = path.StartsWith("Assets/") ? path : "Assets/" + path;
            _path = _path.EndsWith(".asset") ? _path : _path + ".asset";
            if (!hasAsset)
                EditorGUILayout.LabelField("Path to save:", _path);
            EditorGUI.EndDisabledGroup();

            // Create file
            if (!hasScriptableObject)
            {
                EditorGUI.BeginDisabledGroup(index == -1);
                if (GUILayout.Button(SAVE_ASSET_CONTENT))
                {
                    scriptableObject = InstantiateAndApply(targetObject, scriptableObjectName);
                    AssetDatabaseHelper.CreateAsset(scriptableObject, _path);
                }
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                // Clean
                if (GUILayout.Button(CLEAN_FIELD))
                {
                    Undo.RecordObject(targetObject, "Clean field");
                    propertyWrapper.Accessors.Set(null);
                    path = DEFAULT_PATH;
                    propertyWrapper.Property.serializedObject.ApplyModifiedProperties();
                }
            }
        }

        private ScriptableObject InstantiateAndApply(UnityObject targetObject, string name)
        {
            ScriptableObject scriptableObject;
            Undo.RecordObject(targetObject, "Instantiate field");
            scriptableObject = CreateInstance(allowedTypes[index]);
            scriptableObject.name = name;
            propertyWrapper.Accessors.Set(scriptableObject);
            propertyWrapper.Property.serializedObject.ApplyModifiedProperties();
            return scriptableObject;
        }

        private int GetIndex(Type type) => allowedTypes.IndexOf(type);
    }
}

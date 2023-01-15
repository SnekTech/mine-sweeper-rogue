using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SnekTech.Core.Animation;
using SnekTech.Core.CustomAttributes;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace SnekTech.Editor.Animation
{
    public class ClipDataGenerator : VisualElement
    {
        #region UI Builder boilerplate

        private const string UxmlAssetPath = "Assets/_Project/Editor/Animation/ClipDataGenerator.uxml";

        public new class UxmlFactory : UxmlFactory<ClipDataGenerator>
        {
        }

        #endregion

        #region visual elment fields

        private readonly VisualElement _root;
        private ObjectField _acField;
        private TextField _clipDataFolderNameField;
        private Toggle _shouldOverwriteToggle;
        private Button _generateButton;

        #endregion

        #region static fields related to ClipData holder type

        private static DropdownField s_holderTypeDropdownField;
        private static Type s_currentClipDataHolderType;
        private static readonly Dictionary<string, Type> holderTypeNameToType = new Dictionary<string, Type>();

        #endregion

        #region getters

        private AnimatorController CurrentAc => _acField.value as AnimatorController;
        private string ClipDataSaveFolderName => _clipDataFolderNameField.value;
        private bool ShouldOverwrite => _shouldOverwriteToggle.value;

        #endregion

        public ClipDataGenerator()
        {
            _root = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlAssetPath).Instantiate();
            hierarchy.Add(_root);

            FindControls();
            SetupHolderTypeDropdown();
            SetupEventHandlers();
        }

        private void SetupHolderTypeDropdown()
        {
            var dropdownParent = _root.Q("outputConfig");
            var dropdownPlaceholder = _root.Q<DropdownField>("holderTypePlaceHolder");
            s_holderTypeDropdownField.label = dropdownPlaceholder.label;

            dropdownPlaceholder.style.display = DisplayStyle.None;
            dropdownParent.Add(s_holderTypeDropdownField);
        }

        private void FindControls()
        {
            _acField = _root.Q<ObjectField>("ac");
            _acField.objectType = typeof(AnimatorController);

            _clipDataFolderNameField = _root.Q<TextField>();
            _shouldOverwriteToggle = _root.Q<Toggle>();
            _generateButton = _root.Q<Button>();
        }

        private void SetupEventHandlers()
        {
            _generateButton.clickable = new Clickable(GenerateAssets);

            _acField.RegisterValueChangedCallback(HandleAcChange);

            void HandleAcChange(ChangeEvent<Object> changeEvent)
            {
                var newAc = (AnimatorController) changeEvent.newValue;
                _clipDataFolderNameField.value = $"{newAc.name}-ClipData";
            }

            void HandleHolderTypeDropdownChanged(ChangeEvent<string> evt)
            {
                string typeName = evt.newValue;
                var holderType = holderTypeNameToType[typeName];
                s_currentClipDataHolderType = holderType;
            }

            s_holderTypeDropdownField.UnregisterValueChangedCallback(HandleHolderTypeDropdownChanged);
            s_holderTypeDropdownField.RegisterValueChangedCallback(HandleHolderTypeDropdownChanged);
        }

        private void GenerateAssets()
        {
            var clipDataList = GenerateClipDataListAsset();
            var clipDataHolderAsset = GenerateClipDataHolderAsset(clipDataList);
            if (clipDataHolderAsset != null)
            {
                Selection.activeObject = clipDataHolderAsset;
            }

            AssetDatabase.SaveAssets();
        }

        private string GetCurrentAcParentFolder()
        {
            return CurrentAc == null ? null : FileUtils.GetAssetParentFolder(AssetDatabase.GetAssetPath(CurrentAc));
        }

        private List<ClipData> GenerateClipDataListAsset()
        {
            if (CurrentAc == null) return null;

            string clipDataSaveFolderName = ClipDataSaveFolderName;
            if (clipDataSaveFolderName == null)
            {
                return null;
            }

            // the main save folder
            string acParentPath = GetCurrentAcParentFolder();

            string clipDataSaveFolderPath = FileUtils.AssetPathCombine(acParentPath, clipDataSaveFolderName);
            bool isTargetFolderExisting = AssetDatabase.GetSubFolders(acParentPath)
                .Any(subFolderPath => subFolderPath == clipDataSaveFolderPath);

            if (ShouldOverwrite && isTargetFolderExisting)
            {
                AssetDatabase.DeleteAsset(clipDataSaveFolderPath);
            }

            // reassign here in case a different folder is created, when we should not overwrite
            string createdFolderGuid = AssetDatabase.CreateFolder(acParentPath, clipDataSaveFolderName);
            clipDataSaveFolderPath = AssetDatabase.GUIDToAssetPath(createdFolderGuid);

            var clipNameToClip = new Dictionary<string, AnimationClip>();
            foreach (var animationClip in CurrentAc.animationClips)
            {
                clipNameToClip[animationClip.name] = animationClip;
            }

            // to get the correct hash for a state in Animator Controller,
            // we need to traverse the states in the Animator Controller StateMachine
            var clipDataList = new List<ClipData>();
            foreach (var childAnimatorState in CurrentAc.GetStates())
            {
                var state = childAnimatorState.state;
                string clipName = state.motion.name;
                var clip = clipNameToClip[clipName];

                var newClipData = ScriptableObject.CreateInstance<ClipData>();
                newClipData.ClipName = clipName;
                newClipData.NameHash = state.nameHash;
                newClipData.FrameCount = Mathf.CeilToInt(clip.frameRate * clip.length);

                clipDataList.Add(newClipData);
                AssetDatabase.CreateAsset(newClipData, $"{clipDataSaveFolderPath}/{newClipData.ClipName}.asset");
            }

            return clipDataList;
        }

        private ScriptableObject GenerateClipDataHolderAsset(List<ClipData> clipDataList)
        {
            string clipDataHolderSavePath = $"{GetCurrentAcParentFolder()}/{s_currentClipDataHolderType.Name}.asset";
            if (ShouldOverwrite)
            {
                AssetDatabase.DeleteAsset(clipDataHolderSavePath);
            }

            // var clipDataHolderAsset = Activator.CreateInstance(s_currentClipDataHolderType) as ScriptableObject;
            var clipDataHolderAsset = ScriptableObject.CreateInstance(s_currentClipDataHolderType);

            if (clipDataHolderAsset == null)
            {
                UnityEngine.Debug.Log("ClipDataHolder asset not created");
                return null;
            }

            AssetDatabase.CreateAsset(clipDataHolderAsset, clipDataHolderSavePath);

            SetFieldsInNameOrder(clipDataList, clipDataHolderAsset);

            return clipDataHolderAsset;
        }

        private static void SetFieldsInNameOrder<T1, T2>(List<T1> values, T2 target)
            where T1 : Object
            where T2 : Object
        {
            var targetFields =
                target.GetType().GetInstanceFieldsWithAttributeOfType(typeof(ClipDataTargetFieldAttribute));

            if (values.Count != targetFields.Count)
            {
                UnityEngine.Debug.LogWarning("values count and target serialized field count not match, return");
                return;
            }

            targetFields.Sort((fieldA, fieldB) => string.CompareOrdinal(fieldA.Name.ToLower(), fieldB.Name.ToLower()));
            values.Sort((a, b) => string.CompareOrdinal(a.name.ToLower(), b.name.ToLower()));

            if (values.Select(o => o.name).ToList().HasDuplicates())
            {
                UnityEngine.Debug.LogWarning("there are duplicate names in values, unstable sorting");
            }

            using (var serializedObject = new SerializedObject(target))
            {
                serializedObject.Update();

                for (int i = 0; i < values.Count; i++)
                {
                    var value = values[i];
                    var targetField = targetFields[i];

                    var propertyField = serializedObject.FindProperty(targetField.Name);
                    propertyField.objectReferenceValue = value;
                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        [DidReloadScripts]
        private static void UpdateClipDataHolderTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var holderTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    type.IsDefined(typeof(ClipDataHolderAttribute)) &&
                    type.IsSubclassOf(typeof(ScriptableObject)))
                .ToList();

            foreach (var type in holderTypes)
            {
                holderTypeNameToType[type.Name] = type;
            }

            var kvPairs = holderTypeNameToType.ToList();
            s_holderTypeDropdownField = new DropdownField
            {
                choices = kvPairs.Select(kv => kv.Key).ToList(),
            };

            if (kvPairs.Count > 0)
            {
                var firstPair = kvPairs[0];
                s_holderTypeDropdownField.value = firstPair.Key;
                s_currentClipDataHolderType = firstPair.Value;
            }
        }
    }
}
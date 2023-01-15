using System.Collections.Generic;
using System.Linq;
using SnekTech.Core.Animation;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Animation
{
    public class ClipDataGenerator : VisualElement
    {
        private const string UxmlAssetPath = "Assets/_Project/Editor/Animation/ClipDataGenerator.uxml";

        public new class UxmlFactory : UxmlFactory<ClipDataGenerator>
        {
        }

        private readonly ObjectField _acField;
        private readonly ObjectField _clipDataTargetField;
        private readonly TextField _clipDataFolderNameField;
        private readonly Toggle _shouldOverwriteToggle;

        private AnimatorController CurrentAc => _acField.value as AnimatorController;

        public ClipDataGenerator()
        {
            var root = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlAssetPath).Instantiate();
            hierarchy.Add(root);

            _acField = root.Q<ObjectField>("ac");
            _clipDataTargetField = root.Q<ObjectField>("clipDataTarget");
            _clipDataFolderNameField = root.Q<TextField>();
            _shouldOverwriteToggle = root.Q<Toggle>();

            var generateButton = root.Q<Button>();

            generateButton.clickable = new Clickable(GenerateClips);

            void HandleAcChange(ChangeEvent<Object> changeEvent)
            {
                var newAc = (AnimatorController) changeEvent.newValue;
                _clipDataFolderNameField.value = $"{newAc.name}-ClipData";
            }

            _acField.RegisterValueChangedCallback(HandleAcChange);
        }

        private void GenerateClips()
        {
            if (CurrentAc == null) return;

            string clipDataSaveFolderName = _clipDataFolderNameField.value;

            string pathToAc = AssetDatabase.GetAssetPath(CurrentAc);
            string acParentPath = FileUtils.GetAssetParentFolder(pathToAc);
            string clipDataSaveFolderPath = FileUtils.AssetPathCombine(acParentPath, clipDataSaveFolderName);

            bool isTargetFolderExisting = AssetDatabase.GetSubFolders(acParentPath)
                .Any(subFolderPath => subFolderPath == clipDataSaveFolderPath);
            if (_shouldOverwriteToggle.value && isTargetFolderExisting)
            {
                AssetDatabase.DeleteAsset(clipDataSaveFolderPath);
            }

            string createdFolderGuid = AssetDatabase.CreateFolder(acParentPath, clipDataSaveFolderName);
            clipDataSaveFolderPath = AssetDatabase.GUIDToAssetPath(createdFolderGuid);

            var clipNameToClip = new Dictionary<string, AnimationClip>();
            foreach (var animationClip in CurrentAc.animationClips)
            {
                clipNameToClip[animationClip.name] = animationClip;
            }

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

                EditorUtility.SetDirty(newClipData);
            }

            var target = _clipDataTargetField.value;
            if (target != null)
            {
                SetFieldsInNameOrder(clipDataList, target);
                Selection.activeObject = target;
            }
            else
            {
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(clipDataSaveFolderPath);
            }

            AssetDatabase.SaveAssets();
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

            for (int i = 0; i < values.Count; i++)
            {
                var value = values[i];
                var targetField = targetFields[i];
                targetField.SetValue(target, value);
            }

            EditorUtility.SetDirty(target);
        }
    }
}
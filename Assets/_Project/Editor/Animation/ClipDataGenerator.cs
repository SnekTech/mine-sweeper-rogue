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
        private readonly TextField _clipDataFolderNameField;
        private readonly Toggle _shouldOverwriteToggle;
        
        private AnimatorController CurrentAc => _acField.value as AnimatorController;

        public ClipDataGenerator()
        {
            var root = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlAssetPath).Instantiate();
            hierarchy.Add(root);

            _acField = root.Q<ObjectField>();
            _clipDataFolderNameField = root.Q<TextField>();
            _shouldOverwriteToggle = root.Q<Toggle>();
            
            var generateButton = root.Q<Button>();

            generateButton.clickable = new Clickable(GenerateClips);

            void HandleAcChange(ChangeEvent<Object> changeEvent)
            {
                var newAc = (AnimatorController)changeEvent.newValue;
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

            foreach (var clip in CurrentAc.animationClips)
            {
                var newClipData = ScriptableObject.CreateInstance<ClipData>();
                newClipData.ClipName = clip.name;
                newClipData.Hash = Animator.StringToHash(clip.name);
                newClipData.FrameCount = Mathf.CeilToInt(clip.frameRate * clip.length);

                AssetDatabase.CreateAsset(newClipData, $"{clipDataSaveFolderPath}/{newClipData.ClipName}.asset");
                
                EditorUtility.SetDirty(newClipData);
            }

            AssetDatabase.SaveAssets();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(clipDataSaveFolderPath);
        }
    }
}
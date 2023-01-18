using System;
using System.Collections.Generic;
using SnekTech.Core.Animation;
using SnekTech.Core.CustomAttributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace SnekTech.Editor.Animation
{
    public class SnekClipGenerator : VisualElement
    {
        #region UI Builder boilerplate

        private const string UxmlAssetPath = "Assets/_Project/Editor/Animation/SnekClipGenerator.uxml";

        public new class UxmlFactory : UxmlFactory<SnekClipGenerator>
        {
        }

        #endregion

        #region visual elment fields

        private readonly VisualElement _root;
        private ObjectField _jsonAssetField;
        private ObjectField _spriteSheetTextureField;
        private TextField _clipDataFolderNameField;
        private Button _generateButton;
        private Button _refreshButton;

        #endregion

        private Type _currentHolderType;

        #region getters for convience

        private TextAsset JsonAsset => _jsonAssetField.value as TextAsset;
        private string JsonAssetParentFolder => _jsonAssetField.value == null
            ? null
            : FileUtils.GetAssetParentFolderPath(AssetDatabase.GetAssetPath(JsonAsset));

        private Texture2D SpriteSheetAsset => _spriteSheetTextureField.value as Texture2D;
        private string SpriteSheetAssetPath => AssetDatabase.GetAssetPath(SpriteSheetAsset);
        
        private string ClipDataSaveFolderName => _clipDataFolderNameField.value;

        #endregion

        public SnekClipGenerator()
        {
            _root = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlAssetPath).Instantiate();
            hierarchy.Add(_root);

            FindControls();
            SetupEventHandlers();
        }

        private void FindControls()
        {
            _jsonAssetField = _root.Q<ObjectField>("jsonAsset");
            _jsonAssetField.objectType = typeof(TextAsset);

            _spriteSheetTextureField = _root.Q<ObjectField>("spriteSheet");
            _spriteSheetTextureField.objectType = typeof(Texture2D);

            _clipDataFolderNameField = _root.Q<TextField>();
            _generateButton = _root.Q<Button>("generateButton");
        }

        private void SetupEventHandlers()
        {
            _generateButton.clickable = new Clickable(GenerateAssets);

            _jsonAssetField.RegisterValueChangedCallback(HandleJsonChange);

            void HandleJsonChange(ChangeEvent<Object> changeEvent)
            {
                var newJsonAsset = (TextAsset) changeEvent.newValue;
                _clipDataFolderNameField.value = $"{newJsonAsset.name}-ClipData";
            }

            void HandleHolderTypeChange(Type type) => _currentHolderType = type;
            ClipHolderTypeDropdown.OnTypeValueChange -= HandleHolderTypeChange;
            ClipHolderTypeDropdown.OnTypeValueChange += HandleHolderTypeChange;
        }

        private void GenerateAssets()
        {
            var clipDataList = GenerateClipAssets();
            var clipDataHolderAsset = GenerateClipDataHolderAsset(clipDataList);
            if (clipDataHolderAsset != null)
            {
                Selection.activeObject = clipDataHolderAsset;
            }

            AssetDatabase.SaveAssets();
        }

        private List<SnekAnimationClip> GenerateClipAssets()
        {

            string clipDataSaveFolderName = ClipDataSaveFolderName;
            if (clipDataSaveFolderName == null)
            {
                return null;
            }

            // the main save folder
            string parentFolderToSave = JsonAssetParentFolder;
            if (parentFolderToSave == null)
            {
                return null;
            }

            string clipSaveFolderPath = FileUtils.AssetPathCombine(parentFolderToSave, clipDataSaveFolderName);
            bool isClipSaveFolderExisting = FileUtils.ContainsAssetAtPath<Object>(clipSaveFolderPath);

            if (isClipSaveFolderExisting)
            {
                AssetDatabase.DeleteAsset(clipSaveFolderPath);
            }

            AssetDatabase.CreateFolder(parentFolderToSave, clipDataSaveFolderName);


            var asepriteJsonData = AsepriteJsonHandler.ExtractClipMetaData(JsonAsset.text);
            var clipAssets = AsepriteJsonHandler.GetSnekClipsFromAsepriteJson(asepriteJsonData, SpriteSheetAssetPath);
          
            foreach (var clipAsset in clipAssets)
            {
                AssetDatabase.CreateAsset(clipAsset, $"{clipSaveFolderPath}/{clipAsset.AnimName}.asset");
            }

            return clipAssets;
        }

        private ScriptableObject GenerateClipDataHolderAsset(List<SnekAnimationClip> clipDataList)
        {
            if (clipDataList == null)
            {
                return null;
            }

            if (_currentHolderType == null)
            {
                UnityEngine.Debug.LogWarning("holderType need to refresh, select another value in dropdown and select back");
                return null;
            }

            string saveParentFolderPath = JsonAssetParentFolder;
            string clipDataHolderSavePath = $"{saveParentFolderPath}/{_currentHolderType.Name}.asset";
            if (FileUtils.ContainsAssetAtPath<Object>(clipDataHolderSavePath))
            {
                AssetDatabase.DeleteAsset(clipDataHolderSavePath);
            }

            var clipDataHolderAsset = ScriptableObject.CreateInstance(_currentHolderType);

            if (clipDataHolderAsset == null)
            {
                UnityEngine.Debug.Log("ClipDataHolder asset not created");
                return null;
            }

            AssetDatabase.CreateAsset(clipDataHolderAsset, clipDataHolderSavePath);

            SetClipsField(clipDataList, clipDataHolderAsset);

            return clipDataHolderAsset;
        }

        private static void SetClipsField(List<SnekAnimationClip> clips, ScriptableObject target)
        {
            var attributedFields =
                target.GetType().GetInstanceFieldsWithAttributeOfType(typeof(SnekClipsFieldAttribute));

            if (attributedFields.Count != 1)
            {
                throw new Exception(
                    $"target {target} should have exactly 1 clips field with attribute {nameof(SnekClipsFieldAttribute)}");
            }

            var clipsField = attributedFields[0];

            using (var serializedObject = new SerializedObject(target))
            {
                var clipsSerializedField = serializedObject.FindProperty(clipsField.Name);
                clipsSerializedField.ClearArray();

                for (int i = 0; i < clips.Count; i++)
                {
                    clipsSerializedField.InsertArrayElementAtIndex(i);
                    clipsSerializedField.GetArrayElementAtIndex(i).objectReferenceValue = clips[i];
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
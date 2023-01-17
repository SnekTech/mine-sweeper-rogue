using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private const string ClipsHolderTypeLabel = "Clips Holder Type";

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

        #region static fields related to ClipData holder type

        private static VisualElement s_holderDropdownParent;
        private static Type s_currentClipDataHolderType;
        private static readonly Dictionary<string, Type> holderTypeNameToType = new Dictionary<string, Type>();

        #endregion

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

        private static void SetupHolderTypeDropdown()
        {
            var kvPairs = holderTypeNameToType.ToList();
            var holderTypeDropdownField = new DropdownField
            {
                choices = kvPairs.Select(kv => kv.Key).ToList(),
            };

            if (kvPairs.Count > 0)
            {
                var firstPair = kvPairs[0];
                holderTypeDropdownField.value = firstPair.Key;
                s_currentClipDataHolderType = firstPair.Value;
            }
            
            s_holderDropdownParent.Clear();
            s_holderDropdownParent.Add(holderTypeDropdownField);
            holderTypeDropdownField.label = ClipsHolderTypeLabel;
            holderTypeDropdownField.RegisterValueChangedCallback(HandleHolderTypeDropdownChanged);
        }

        private void FindControls()
        {
            _jsonAssetField = _root.Q<ObjectField>("jsonAsset");
            _jsonAssetField.objectType = typeof(TextAsset);
            
            s_holderDropdownParent = _root.Q("clipHolderParent");
            
            _spriteSheetTextureField = _root.Q<ObjectField>("spriteSheet");
            _spriteSheetTextureField.objectType = typeof(Texture2D);

            _clipDataFolderNameField = _root.Q<TextField>();
            _generateButton = _root.Q<Button>("generateButton");
            _refreshButton = _root.Q<Button>("refreshButton");
        }

        private void SetupEventHandlers()
        {
            _generateButton.clickable = new Clickable(GenerateAssets);

            _refreshButton.clickable = new Clickable(UpdateClipDataHolderTypes);

            _jsonAssetField.RegisterValueChangedCallback(HandleJsonChange);

            void HandleJsonChange(ChangeEvent<Object> changeEvent)
            {
                var newJsonAsset = (TextAsset) changeEvent.newValue;
                _clipDataFolderNameField.value = $"{newJsonAsset.name}-ClipData";
            }

        }

        private static void HandleHolderTypeDropdownChanged(ChangeEvent<string> evt)
        {
            string typeName = evt.newValue;
            var holderType = holderTypeNameToType[typeName];
            s_currentClipDataHolderType = holderType;
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


            var clipMetaDataList = AsepriteJsonHandler.ExtractClipMetaData(JsonAsset.text);
            var clipList = new List<SnekAnimationClip>();
            foreach (var clipMetaData in clipMetaDataList)
            {
                var newClip = ScriptableObject.CreateInstance<SnekAnimationClip>();
                newClip.FrameDurations = clipMetaData.FrameDurations;
                SetClipSprites(newClip, clipMetaData.StartIndex);

                clipList.Add(newClip);
                AssetDatabase.CreateAsset(newClip, $"{clipSaveFolderPath}/{clipMetaData.Name}.asset");
            }

            return clipList;
        }

        private ScriptableObject GenerateClipDataHolderAsset(List<SnekAnimationClip> clipDataList)
        {
            if (clipDataList == null)
            {
                return null;
            }

            string saveParentFolderPath = JsonAssetParentFolder;
            string clipDataHolderSavePath = $"{saveParentFolderPath}/{s_currentClipDataHolderType.Name}.asset";
            if (FileUtils.ContainsAssetAtPath<Object>(clipDataHolderSavePath))
            {
                AssetDatabase.DeleteAsset(clipDataHolderSavePath);
            }

            var clipDataHolderAsset = ScriptableObject.CreateInstance(s_currentClipDataHolderType);

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

        private void SetClipSprites(SnekAnimationClip clip, int startIndex)
        {
            var spriteExtractor = new SpriteExtractorFromSpriteSheet(SpriteSheetAssetPath);
            clip.Sprites = spriteExtractor.GetSpritesWithInRange(startIndex, clip.FrameCount);
        }

        private static void UpdateClipDataHolderTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var holderTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    type.IsDefined(typeof(SnekClipHolderAttribute)) &&
                    type.IsSubclassOf(typeof(ScriptableObject)))
                .ToList();

            foreach (var type in holderTypes)
            {
                holderTypeNameToType[type.Name] = type;
            }
            
            SetupHolderTypeDropdown();
        }
    }
}
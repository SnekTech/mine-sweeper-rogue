using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SnekTech.Core.CustomAttributes;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Animation
{
    public class ClipHolderTypeDropdown : VisualElement
    {
        private const string ClipsHolderTypeLabel = "Clips Holder Type";

        private static DropdownField s_dropdownField;
        private static readonly Dictionary<string, Type> holderTypeByTypeName = new Dictionary<string, Type>();
        public static Type CurrentType => holderTypeByTypeName[s_dropdownField.value];
        
        public new class UxmlFactory : UxmlFactory<ClipHolderTypeDropdown>
        {
        }

        public ClipHolderTypeDropdown()
        {
            s_dropdownField = new DropdownField
            {
                label = ClipsHolderTypeLabel,
            };
            hierarchy.Add(s_dropdownField);
            UpdateClipDataHolderTypes();
        }

        [DidReloadScripts]
        private static void UpdateClipDataHolderTypes()
        {
            if (s_dropdownField == null) return;
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var holderTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    type.IsDefined(typeof(SnekClipHolderAttribute)) &&
                    type.IsSubclassOf(typeof(ScriptableObject)))
                .ToList();
            // todo: create an SO to store the type

            foreach (var type in holderTypes)
            {
                holderTypeByTypeName[type.Name] = type;
            }
            
            SetupHolderTypeDropdown();
        }

        private static void SetupHolderTypeDropdown()
        {
            var kvPairs = holderTypeByTypeName.ToList();
            s_dropdownField.choices = holderTypeByTypeName.Keys.ToList();

            if (kvPairs.Count > 0)
            {
                var firstPair = kvPairs[0];
                s_dropdownField.value = firstPair.Key;
                
                s_dropdownField.SendEvent(new ChangeEvent<Type>());
            }
        }
    }
}
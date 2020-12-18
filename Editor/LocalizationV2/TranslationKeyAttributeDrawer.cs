using UnityEngine;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace Unisloth.Localization.Editor
{
    public sealed class TranslationKeyAttributeDrawer : OdinAttributeDrawer<TranslationKeyAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }
            string key = ValueEntry.SmartValue;

            List<string> keys = GetKeys().ToList();
            int dropdownIndex = Mathf.Max(0, keys.IndexOf(key));
            dropdownIndex = EditorGUI.Popup(rect, dropdownIndex, keys.ToArray());
            string newValue = keys.Count == 0 ? "" : keys.ElementAt(dropdownIndex);
            ValueEntry.SmartValue = newValue;
        }

        private IEnumerable<string> GetKeys()
        {
            return LocalizationManager.Instance.AllKeys();
        }
    }
}

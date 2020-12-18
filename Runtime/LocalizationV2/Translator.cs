using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Unisloth.Localization
{
    [ExecuteAlways]
    public abstract class Translator : MonoBehaviour
    {

        [TranslationKey, OnValueChanged("UpdateValue")]
        public string key;

        private void Awake()
        {
            LocalizationManager.OnLanguageChanged += UpdateValue;
        }

        public void UpdateValue()
        {
            SetText(LocalizationManager.Instance.GetTranslation(key));
/*#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif*/
        }

        private void OnEnable()
        {
            UpdateValue();
        }

        private void OnDestroy()
        {
            LocalizationManager.OnLanguageChanged -= UpdateValue;
        }

        public abstract void SetText(string text);
    }
}

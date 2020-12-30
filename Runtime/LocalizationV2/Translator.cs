using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Unisloth.Localization
{
    [ExecuteAlways]
    public abstract class Translator : MonoBehaviour
    {
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    UpdateValue();
                }
            }
        }

        [SerializeField, TranslationKey, OnValueChanged("UpdateValue")]
        private string _key;

        private void Awake()
        {
            LocalizationManager.OnLanguageChanged += UpdateValue;
        }

        public void UpdateValue()
        {
            SetText(LocalizationManager.Instance.GetTranslation(_key));
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

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using TranslationKeyString = System.String;

namespace Unisloth.Localization
{
    [ExecuteAlways]
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        public static LocalizationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LocalizationManager>();
                }
                return _instance;
            }
            private set
            {
                if (value != _instance)
                {
                    _instance = value;
                }
            }
        }

        private static LocalizationManager _instance;
        private static readonly string NO_FILE = "Translation file missing";

        [ValueDropdown("AllLanguages"), OnValueChanged("OnLanguageUpdated")]
        public SystemLanguage currentLanguage = SystemLanguage.English;
        [SerializeField, Required] private TranslationsList _translationsList;

        public delegate void LanguageChanged();
        public static LanguageChanged OnLanguageChanged;

        [Button("Update translations")]
        private void UpdateAllTranslations()
        {
            foreach (Translator tl in FindObjectsOfType<Translator>())
            {
                tl.UpdateValue();
            }
        }


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
#if !UNITY_EDITOR
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
#endif
        }

        public string GetTranslation(TranslationKeyString key)
        {
            if (_translationsList == null)
            {
                Debug.LogError("Translation file not defined");
                return NO_FILE;
            }

            return _translationsList.GetValue(key, currentLanguage);
        }

        private void OnLanguageUpdated()
        {
            OnLanguageChanged?.Invoke();
        }

        public IEnumerable<string> AllKeys()
        {
            return _translationsList == null ? null : _translationsList.GetAvailableTranslationKeys();
        }

        public IEnumerable<SystemLanguage> AllLanguages()
        {
            return _translationsList == null ? null : _translationsList.GetAvailableLanguages();
        }
    }
}

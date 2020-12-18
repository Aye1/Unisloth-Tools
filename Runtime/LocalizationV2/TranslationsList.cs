using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using System;

using TranslationKeyString = System.String;

namespace Unisloth.Localization
{
    [CreateAssetMenu(fileName = "New Translation List", menuName = "Unisloth/Localization/Translation List"), Serializable]
    public class TranslationsList : ScriptableObject
    {
        public static readonly string NOT_TRANSLATED = "_NOT_TRANSLATED_";
        public static readonly string MISSING_TRANSLATIONS = "TRANSLATION_FILE_MISSING";

        [HorizontalGroup("Add Key")]
        public string newKey = "";

        [HorizontalGroup("Add Key")]
        [Button("Add new key")]
        [DisableIf("@string.IsNullOrWhiteSpace(this.newKey)")]
        public void AddKey()
        {
            //TranslationKey newTranslationKey = new TranslationKey(newKey);
            _translationKeys.Add(newKey);
            CreateEmptyTranslationsForNewKey(newKey);
            newKey = "";
        }
        [Space(10)]

        [InfoBox("@GetMissingTranslationsText()", VisibleIf = "AreTranslationsMissing", InfoMessageType = InfoMessageType.Warning)]

        [SerializeField]
        [OnCollectionChanged(After = "OnKeysCollectionChanged")]
        [ListDrawerSettings(HideAddButton = true, CustomRemoveElementFunction = "OnKeyRemoved")]
        private List<TranslationKeyString> _translationKeys;

        [Space(10)]

        [SerializeField]
        [OnCollectionChanged(After = "OnLanguageCollectionChanged")]
        private List<SystemLanguage> _availableLanguages;

        [Space(10)]

        [SerializeField]
        [OnCollectionChanged(After = "OnTranslationCollectionChanged")]
        private List<Translation> _translations;

        private List<KeyValuePair<TranslationKeyString, SystemLanguage>> _missingTranslations;

        private void Awake()
        {
            _translationKeys = new List<TranslationKeyString>();
            _availableLanguages = new List<SystemLanguage>();
            _translations = new List<Translation>();
            _missingTranslations = new List<KeyValuePair<TranslationKeyString, SystemLanguage>>();
        }

        private void OnEnable()
        {
            UpdateMissingTranslations();
        }

        public void AddLanguage(SystemLanguage language)
        {
            if (!_availableLanguages.Contains(language))
            {
                _availableLanguages.Add(language);
            }
        }

        public void AddTranslation(Translation translation)
        {
            if (!IsLanguageAvailable(translation.language))
            {
                Debug.LogWarning(translation.language + " language not available, translation not added");
                return;
            }

            if (!KeyExists(translation.key))
            {
                Debug.LogWarning(translation.key + " does not exist, translation not added");
                return;
            }

            if (!TranslationExists(translation))
            {
                _translations.Add(translation);
                translation.parent = this;
                //translation.OnKeyOrLanguageChanged();
            }
        }

        public string GetValue(TranslationKeyString key, SystemLanguage language)
        {
            if(_translations == null)
            {
                return MISSING_TRANSLATIONS;
            }
            Translation translation = _translations.Where(t => t.key == key && t.language == language).FirstOrDefault();
            return translation == null ? NOT_TRANSLATED : translation.value;
        }

        public bool TranslationExists(TranslationKeyString key, SystemLanguage language)
        {
            return _translations.Any(t => t.key == key && t.language == language && !string.IsNullOrWhiteSpace(t.value));
        }

        public bool TranslationExists(Translation translation)
        {
            return TranslationExists(translation.key, translation.language);
        }

        public bool IsTranslationDuplicated(TranslationKeyString key, SystemLanguage language)
        {
            return _translations.Count(t => t.key == key && t.language == language) > 1;
        }

        public bool IsLanguageAvailable(SystemLanguage language)
        {
            return _availableLanguages.Contains(language);
        }

        public bool KeyExists(TranslationKeyString key)
        {
            return _translationKeys.Contains(key);
        }

        public IEnumerable<TranslationKeyString> GetAvailableTranslationKeys()
        {
            return _translationKeys;
        }

        public IEnumerable<SystemLanguage> GetAvailableLanguages()
        {
            return _availableLanguages;
        }

        public void DeleteDuplicates(Translation t)
        {
            _translations.RemoveAll(x => x.key == t.key && x.language == t.language && x != t);
        }

        private void OnTranslationCollectionChanged(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType.Equals(CollectionChangeType.Add))
            {
                Translation newTranslation = (Translation)info.Value;
                newTranslation.parent = this;
                newTranslation.key = _translationKeys.LastOrDefault();
                newTranslation.language = _availableLanguages.LastOrDefault();
                //newTranslation.OnKeyOrLanguageChanged();
            }
            UpdateMissingTranslations();
        }

        private void OnKeysCollectionChanged(CollectionChangeInfo info, object value)
        {
            TranslationKeyString keyChanged = (TranslationKeyString)info.Value;
            switch (info.ChangeType)
            {
                case CollectionChangeType.Add:
                    CreateEmptyTranslationsForNewKey(keyChanged);
                    break;
            }
            UpdateMissingTranslations();
        }

        private void OnLanguageCollectionChanged(CollectionChangeInfo info, object value)
        {
            UpdateMissingTranslations();
        }

        public void AddEmptyTranslation(TranslationKeyString key, SystemLanguage language)
        {
            Translation t = new Translation
            {
                key = key,
                language = language,
                value = NOT_TRANSLATED
            };
            AddTranslation(t);
        }

        private void CreateEmptyTranslationsForNewKey(TranslationKeyString key)
        {
            _availableLanguages.ForEach(l => AddEmptyTranslation(key, l));
        }

        private void OnKeyRemoved(TranslationKeyString key)
        {
            _translationKeys.Remove(key);
            _translations.RemoveAll(t => t.key == key);
            UpdateMissingTranslations();
        }

        private bool AreTranslationsMissing()
        {
            return _missingTranslations != null && _missingTranslations.Count > 0;
        }

        private string GetMissingTranslationsText()
        {
            if (_missingTranslations == null)
            {
                _missingTranslations = new List<KeyValuePair<TranslationKeyString, SystemLanguage>>();
            }
            if (_missingTranslations.Count == 0)
            {
                return "";
            }

            string res = "Missing translations:";
            IEnumerable<IGrouping<SystemLanguage, KeyValuePair<TranslationKeyString, SystemLanguage>>> missingLocales = _missingTranslations.GroupBy(p => p.Value);

            foreach (IGrouping<SystemLanguage, KeyValuePair<TranslationKeyString, SystemLanguage>> lang in missingLocales)
            {
                res += "\n" + lang.Key + ":";
                foreach (KeyValuePair<TranslationKeyString, SystemLanguage> pair in lang)
                {
                    res += " " + pair.Key;
                }
            }
            return res;
        }

        public void UpdateMissingTranslations()
        {
            _missingTranslations?.Clear();
            for (int i = 0; i < _availableLanguages.Count; i++)
            {
                for (int j = 0; j < _translationKeys.Count; j++)
                {
                    if (!TranslationExists(_translationKeys[j], _availableLanguages[i]))
                    {
                        _missingTranslations?.Add(new KeyValuePair<TranslationKeyString, SystemLanguage>(_translationKeys[j], _availableLanguages[i]));
                    }
                }
            }
        }
    }
}
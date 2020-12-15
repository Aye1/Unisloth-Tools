using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;

using TranslationKey = System.String;

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
            _translationKeys.Add(newKey);
            CreateEmptyTranslationsForNewKey(newKey);
            newKey = "";
        }

        [InfoBox("@GetMissingTranslationsText()", VisibleIf = "AreTranslationsMissing", InfoMessageType = InfoMessageType.Warning)]

        [SerializeField]
        [OnCollectionChanged(After = "OnKeysCollectionChanged")]
        [ListDrawerSettings(HideAddButton = true, DraggableItems = false, CustomRemoveElementFunction = "OnKeyRemoved")]
        private List<TranslationKey> _translationKeys;

        [SerializeField]
        [OnCollectionChanged(After = "OnLanguageCollectionChanged")]
        private List<SystemLanguage> _availableLanguages;

        [SerializeField]
        [OnCollectionChanged(After = "OnTranslationCollectionChanged")]
        private List<Translation> _translations;

        private List<KeyValuePair<TranslationKey, SystemLanguage>> _missingTranslations;

        private void Awake()
        {
            _translationKeys = new List<TranslationKey>();
            _availableLanguages = new List<SystemLanguage>();
            _translations = new List<Translation>();
            _missingTranslations = new List<KeyValuePair<TranslationKey, SystemLanguage>>();
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

        public string GetValue(TranslationKey key, SystemLanguage language)
        {
            return _translations == null ? MISSING_TRANSLATIONS : _translations.Where(t => t.key == key && t.language == language).FirstOrDefault().value;
        }

        public bool TranslationExists(TranslationKey key, SystemLanguage language)
        {
            return _translations.Any(t => t.key == key && t.language == language && !string.IsNullOrWhiteSpace(t.value));
        }

        public bool TranslationExists(Translation translation)
        {
            return TranslationExists(translation.key, translation.language);
        }

        public bool IsTranslationDuplicated(TranslationKey key, SystemLanguage language)
        {
            return _translations.Count(t => t.key == key && t.language == language) > 1;
        }

        public bool IsLanguageAvailable(SystemLanguage language)
        {
            return _availableLanguages.Contains(language);
        }

        public bool KeyExists(TranslationKey key)
        {
            return _translationKeys.Contains(key);
        }

        public IEnumerable<TranslationKey> GetAvailableTranslationKeys()
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
            TranslationKey keyChanged = (TranslationKey)info.Value;
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

        public void AddEmptyTranslation(TranslationKey key, SystemLanguage language)
        {
            Translation t = new Translation
            {
                key = key,
                language = language,
                value = NOT_TRANSLATED
            };
            AddTranslation(t);
        }

        private void CreateEmptyTranslationsForNewKey(TranslationKey key)
        {
            _availableLanguages.ForEach(l => AddEmptyTranslation(key, l));
        }

        private void OnKeyRemoved(TranslationKey key)
        {
            _translationKeys.Remove(key);
            _translations.RemoveAll(t => t.key == key);
        }

        private bool AreTranslationsMissing()
        {
            return _missingTranslations == null || _missingTranslations.Count > 0;
        }

        private string GetMissingTranslationsText()
        {
            if (_missingTranslations == null)
            {
                _missingTranslations = new List<KeyValuePair<TranslationKey, SystemLanguage>>();
            }
            if (_missingTranslations.Count == 0)
            {
                return "";
            }

            string res = "Missing translations:";
            IEnumerable<IGrouping<SystemLanguage, KeyValuePair<TranslationKey, SystemLanguage>>> missingLocales = _missingTranslations.GroupBy(p => p.Value);

            foreach (IGrouping<SystemLanguage, KeyValuePair<TranslationKey, SystemLanguage>> lang in missingLocales)
            {
                res += "\n" + lang.Key + ":";
                foreach (KeyValuePair<TranslationKey, SystemLanguage> pair in lang)
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
                        _missingTranslations.Add(new KeyValuePair<TranslationKey, SystemLanguage>(_translationKeys[j], _availableLanguages[i]));
                    }
                }
            }
        }
    }
}
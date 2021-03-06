﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

using TranslationKeyString = System.String;

namespace Unisloth.Localization
{
    [Serializable]
    public class Translation
    {
        [InfoBox("This translation is duplicated", InfoMessageType = InfoMessageType.Warning, VisibleIf = "IsDuplicate")]
        [ShowIf("IsDuplicate")]
        [Button("Delete duplicates")]
        [PropertyOrder(Order = -1)]
        private void DeleteDuplicates()
        {
            parent.DeleteDuplicates(this);
        }

        [OnValueChanged("OnKeyOrLanguageChanged")]
        [ValueDropdown("GetKeys")]
        public TranslationKeyString key;

        [OnValueChanged("OnKeyOrLanguageChanged")]
        [ValueDropdown("GetLanguages")]
        public SystemLanguage language;

        [GUIColor("GetValueFieldColor")]
        public string value;

        [HideInInspector]
        public TranslationsList parent;

        private IEnumerable<TranslationKeyString> GetKeys()
        {
            return parent == null ? null : parent.GetAvailableTranslationKeys();
        }

        private IEnumerable<SystemLanguage> GetLanguages()
        {
            return parent == null ? null : parent.GetAvailableLanguages();
        }

        public void OnKeyOrLanguageChanged()
        {
            parent.UpdateMissingTranslations();
        }

        private bool IsDuplicate()
        {
            return parent.IsTranslationDuplicated(key, language);
        }

        private Color GetValueFieldColor()
        {
            return string.IsNullOrWhiteSpace(value) || value.Equals(TranslationsList.NOT_TRANSLATED) ? Color.yellow : Color.white;
        }
    }
}
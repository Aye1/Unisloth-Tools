using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

using TranslationKey = System.String;

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
    public TranslationKey key;

    [OnValueChanged("OnKeyOrLanguageChanged")]
    [ValueDropdown("GetLanguages")]
    public SystemLanguage language;

    [GUIColor("GetValueFieldColor")]
    public string value;


    [HideInInspector]
    public TranslationsList parent;

    private IEnumerable<TranslationKey> GetKeys()
    {
        return parent == null ? null : parent.GetAvailableTranslationKeys();
    }

    private IEnumerable<SystemLanguage> GetLanguages()
    {
        return parent == null ? null : parent.GetAvailableLanguages();
    }

    public void OnKeyOrLanguageChanged()
    {
        /*if (parent.TranslationExists(key, language))
        {
            Debug.Log("translation exists");
            value = parent.GetValue(key, language);
        }*/
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

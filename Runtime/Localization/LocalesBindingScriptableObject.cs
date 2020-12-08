using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[Serializable]
public struct LocaleBinding
{
    public string locale;
    public SystemLanguage language;

    public static bool operator ==(LocaleBinding b1, LocaleBinding b2)
    {
        return b1.locale == b2.locale;
    }
    public static bool operator !=(LocaleBinding b1, LocaleBinding b2)
    {
        return b1.locale != b2.locale;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

[CreateAssetMenu(fileName = "Locales Binding", menuName = "Unisloth/Localization/Locales Bindings")]
[Serializable]
public class LocalesBindingScriptableObject : ScriptableObject
{
    [TableList]
    public List<LocaleBinding> bindings;
}

using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class LocalizationManager : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, string>> _allLocalizedStrings;
    private readonly string _stringNotFound = "Unknown string";

    public bool isReady = false;
    public SystemLanguage locale;

    [SerializeField] private LocalesBindingScriptableObject _localesBinding;

    public static LocalizationManager Instance { get; private set; }

    public Dictionary<string, Dictionary<string, string>> AllLocalizedStrings {
        get {
            return _allLocalizedStrings;
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        LoadLocalizedStrings();
    }

    private void LoadLocalizedStrings()
    {
        _allLocalizedStrings = CSVLoader.LoadDicoFromCSV("Localizations");
        isReady = true;
        Debug.Log("Localizations loaded - " + _allLocalizedStrings.Count + " languages");
    }

    public string GetLocString(string key)
    {
        if (!isReady)
        {
            return null;
        }
        string localeString = SystemLanguageToString(locale);
        if (_allLocalizedStrings.ContainsKey(localeString))
        {
            if (_allLocalizedStrings[localeString].ContainsKey(key))
            {
                return _allLocalizedStrings[localeString][key];
            }
        }
        return _stringNotFound;
    }

    public string SystemLanguageToString(SystemLanguage language)
    {
        LocaleBinding binding = _localesBinding.bindings.Where(b => b.language == language).FirstOrDefault();
        return binding == default(LocaleBinding) ? Locales.en_GB : binding.locale;
    }

    public SystemLanguage StringToSystemLanguage(string locale)
    {
        LocaleBinding binding = _localesBinding.bindings.Where(b => b.locale == locale).FirstOrDefault();
        return binding == default(LocaleBinding) ? SystemLanguage.English : binding.language; 
    }

#if UNITY_EDITOR
    [MenuItem("Debug/Dump available localizations")]
    public static void DisplayAvailableLoc() {

        foreach(Dictionary<string, string> locale in LocalizationManager.Instance.AllLocalizedStrings.Values) {
            foreach(string value in locale.Values) {
                Debug.Log(value);
            }
        }
    }
#endif

}

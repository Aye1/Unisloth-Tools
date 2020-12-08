using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using ODFilePath = Sirenix.OdinInspector.FilePathAttribute;

public class LocalizationManagerOdinWindow : OdinEditorWindow
{
    [ODFilePath(ParentFolder = "Assets/Resources", Extensions = "csv")]
    [OnValueChanged("LoadLocalizations")]
    public string filePath;

    [ShowIf("@this.filePath != null")]
    [DictionaryDrawerSettings(KeyLabel = "Locale", ValueLabel = "Translations")]
    public Dictionary<string, Dictionary<string, string>> localizations;

    [MenuItem("Tools/Localization/Open Window")]
    private static void OpenWindow()
    {
        GetWindow<LocalizationManagerOdinWindow>().Show();
    }

    private void LoadLocalizations()
    {
        string noExtensionFilename = System.IO.Path.GetFileNameWithoutExtension(filePath);
        Debug.Log(noExtensionFilename);
        localizations = CSVLoader.LoadDicoFromCSV(noExtensionFilename);
    }
}

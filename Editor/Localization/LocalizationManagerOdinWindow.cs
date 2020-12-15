using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using ODFilePath = Sirenix.OdinInspector.FilePathAttribute;
namespace Unisloth.Localization.CSV
{
    public class LocalizationManagerOdinWindow : OdinEditorWindow
    {
        //[Title("Create translations file")]
        //[InfoBox("Translations file is created in \"Assets/Resources\"")]

        /*[FolderPath(ParentFolder = "Assets/Resources")]
        public string folder;*/

        //public string fileName;

        //private TranslationsDatabase _dataBase;

        /*[DisableIf("@String.IsNullOrEmpty(fileName)")]
        [Button("Create")]
        private void CreateFile()
        {
            _dataBase = new TranslationsDatabase();
            rawData = _dataBase.MatrixData;
            _dataBase.filePath = "Assets/Resources/"+fileName;
            _dataBase.CreateTranslations();
            var test = _dataBase.KeyLocaleDictionary;
        }*/

        private bool _isDirty;
        private Array2D<string> _translationsArray2D;


        [ODFilePath(ParentFolder = "Assets/Resources", Extensions = "csv")]
        [OnValueChanged("LoadLocalizations")]
        public string filePath;

        [ShowIf("@this.rawData != null")]
        [TabGroup("Tab", "Raw Matrix")]
        [TableMatrix(HorizontalTitle = "Locales", HideColumnIndices = true, HideRowIndices = true)]
        public string[,] rawData;

        [ShowIf("@this.keyLocaleDictionary != null")]
        [TabGroup("Tab", "Key-Locale Dictionary")]
        //[OnCollectionChanged(After = "UpdateData")]
        public Dictionary<string, Dictionary<string, string>> keyLocaleDictionary;

        [ShowIf("@this.localeKeyDictionary != null")]
        [TabGroup("Tab", "Locale-Key Dictionary")]
        //[OnCollectionChanged(After = "UpdateData")]
        public Dictionary<string, Dictionary<string, string>> localeKeyDictionary;

        [ShowIf("@this.listData != null")]
        [TabGroup("Tab", "List")]
        //[OnCollectionChanged(After = "UpdateData")]
        public List<List<string>> listData;

        /*[Button("Save")]
        [ShowIf("@_isDirty")]
        private void Save()
        {
            if(_isDirty)
            {
                Debug.Log("Saving");
            }
        }*/

        private void LoadLocalizations()
        {
            string noExtensionFilename = System.IO.Path.GetFileNameWithoutExtension(filePath);
            rawData = CSVLoader.LoadRawCSVIntoMatrix(noExtensionFilename);
            keyLocaleDictionary = CSVLoader.LoadDicoFromCSVKeyFirst(noExtensionFilename);
            localeKeyDictionary = CSVLoader.LoadDicoFromCSV(noExtensionFilename);
            _translationsArray2D = new Array2D<string>(rawData);
            listData = _translationsArray2D.DataAsLists;
        }

        /*private void UpdateData(CollectionChangeInfo info, object value)
        {
            Debug.Log("Update data");
            _isDirty = true;

            Debug.Log("object changed " + value.ToString());
        }*/

        [MenuItem("Tools/Localization/Open Window")]
        private static void OpenWindow()
        {
            GetWindow<LocalizationManagerOdinWindow>().Show();
        }
    }
}

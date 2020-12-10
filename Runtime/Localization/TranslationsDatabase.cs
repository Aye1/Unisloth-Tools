using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.IO;

public class TranslationsDatabase
{
    public string folder;
    public string filePath;

    private string FullFilePath
    {
        get { return filePath + ".json"; }
    }

    public string[,] MatrixData
    {
        get { return _data == null ? null : _data.translations; }
    }

    public Dictionary<string, Dictionary<string, string>> KeyLocaleDictionary
    {
        get
        {
            if(_data == null || _data.translations == null) { return null; }
            int keyCount = _data.translations.GetLength(0);
            int localeCount = _data.translations.GetLength(1);
            Debug.Log(keyCount + " " + localeCount);
            return null;
        }
    }

    [SerializeField, HideInInspector]
    private TranslationsRawData _data;

    public TranslationsDatabase()
    {
        _data = new TranslationsRawData();
    }

    public void LoadTranslations()
    {
        if(!File.Exists(FullFilePath)) { return; }
        byte[] bytes = File.ReadAllBytes(FullFilePath);
        _data = SerializationUtility.DeserializeValue<TranslationsRawData>(bytes, DataFormat.JSON);
    }

    public void CreateTranslations()
    {
        if(filePath == "") { return; }
        _data = new TranslationsRawData();
        SaveTranslations();
    }

    public void SaveTranslations()
    {
        if(filePath == "") { return; }

        byte[] bytes = SerializationUtility.SerializeValue(_data, DataFormat.JSON);
        File.WriteAllBytes(FullFilePath, bytes);
    }

    [Serializable]
    public class TranslationsRawData
    {
        public string[,] translations;
    }
}

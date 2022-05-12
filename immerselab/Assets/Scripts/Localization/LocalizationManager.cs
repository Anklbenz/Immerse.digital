using System;
using System.Collections.Generic;
using System.Xml;
using Enums;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static event Action LanguageChangeEvent;
    public static Language SelectedLanguage{ get; private set; }

    [SerializeField] private TextAsset translateFile;
    private static Dictionary<string, List<string>> _localization;

    private void Awake(){
        if (_localization == null)
            LoadLocalization();
    }

    private void LoadLocalization(){
        _localization = new Dictionary<string, List<string>>();

        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(translateFile.text);

        foreach (XmlNode key in xmlDocument["Keys"]?.ChildNodes){
            string keyString = key.Attributes["name"].Value;
            var values = new List<string>();

            foreach (XmlNode translate in key["Translate"].ChildNodes)
                values.Add(translate.InnerText);

            _localization[keyString] = values;
        }
    }

    public void SetLanguage(Language id){
        SelectedLanguage = id;
        LanguageChangeEvent?.Invoke();
    }

    public static string GetTranslate(string key){
        return _localization.ContainsKey(key) ? _localization[key][(int) SelectedLanguage] : key;
    }
}

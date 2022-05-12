using System;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeText : MonoBehaviour
{
    private Text _text;
    private string _key;

    private void Start(){
        _text = GetComponentInChildren<Text>();
        _key = _text.text;
        Localize();
        LocalizationManager.LanguageChangeEvent += OnLanguageChange;
    }

    private void OnEnable(){
        if(_text==null) return;
        
        _key = _text.text;
        Localize();
     }

    private void OnDestroy(){
        LocalizationManager.LanguageChangeEvent -= OnLanguageChange;
    }

    private void OnLanguageChange(){
        Localize();
    }

    private void Localize(){
        if (_text != null)
            _text.text = (LocalizationManager.GetTranslate(_key)).ToUpper();
    }
}
using UnityEngine;
using UnityEngine.UI;

public class LocalizeText : MonoBehaviour
{
    public string key;
    private Text _text;

    private void Start(){
        _text = GetComponentInChildren<Text>();
        Localize();
        LocalizationManager.LanguageChangeEvent += OnLanguageChange;
    }

    private void OnEnable(){
        if (_text == null) return;

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
            _text.text = (LocalizationManager.GetTranslate(key)).ToUpper();
    }
}
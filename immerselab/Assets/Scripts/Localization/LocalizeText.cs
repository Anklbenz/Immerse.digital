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

    private void OnDestroy(){
        LocalizationManager.LanguageChangeEvent -= OnLanguageChange;
    }

    private void OnLanguageChange(){
        Localize();
    }
    
    private void Localize(){
        _text.text = (LocalizationManager.GetTranslate(_key)).ToUpper();
    }
}

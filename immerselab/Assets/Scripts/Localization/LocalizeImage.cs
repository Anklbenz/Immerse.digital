using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class LocalizeImage : MonoBehaviour
{
    private const string LOCALIZATION_PREFIX = "Localization/Sprites/";

    [SerializeField] private string key;
    [SerializeField] private UnityEngine.UI.Image image;

    private void Start(){
        image = GetComponent<UnityEngine.UI.Image>();
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
        var sprite = Resources.Load<Sprite>(LOCALIZATION_PREFIX + LocalizationManager.SelectedLanguage.ToString() + "/" + key);
        if (sprite != null)
            image.sprite = sprite;
        else
            Debug.LogError($"Image {key} not found on path:{LOCALIZATION_PREFIX + LocalizationManager.SelectedLanguage.ToString()}/");
    }
}

using System.Threading.Tasks;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private const string MESSAGE_LOADING = "Loading...";
    private const string MESSAGE_ERROR = "Someting Went Wrong Try Again Later...";

    [SerializeField] private MainLogic mainLogic;
    [SerializeField] private LocalizationManager localizationManager;
    [SerializeField] private GameObject searchPanel, viewPanel, view2DPanel, cube3D;
    [SerializeField] private InputField addressField;
    [SerializeField] private Toggle owner, creator, collection;
    [SerializeField] private Text notifyLabel;

    private void OnEnable() => mainLogic.SearchStatusEvent += HandleSearchResult;

    private void OnDisable() => mainLogic.SearchStatusEvent -= HandleSearchResult;

    private void Start(){
        PanelsVisibleState(true, false, false, false);
    }

    public void OnSearchClick(){
        var searchType = Search.None;

        if (owner.isOn)
            searchType = Search.ByOwner;
        else if (collection.isOn)
            searchType = Search.ByCollection;
        else if (creator.isOn)
            searchType = Search.ByCreator;
        else
            return;

        MessageNotify(MESSAGE_LOADING);
        mainLogic.Search(addressField.text, searchType);
    }

    private void HandleSearchResult(bool searchSuccess){
        if (searchSuccess)
            PanelsVisibleState(false, true, true, false);
        else
            MessageNotify(MESSAGE_ERROR);
    }

    public void OnView2DClick() => PanelsVisibleState(false, true, true, false);

    public void OnView3DClick() => PanelsVisibleState(false, true, false, true);

    public void OnBackButtonClick() => PanelsVisibleState(true, false, false, false);

    public void OnEnLanguageButtonClick() => localizationManager.SetLanguage(Language.En);

    public void OnRuLanguageButtonClick() => localizationManager.SetLanguage(Language.Ru);

    private void PanelsVisibleState(bool search, bool view, bool view2d, bool view3d){
        if (!search) notifyLabel.enabled = false;

        searchPanel.SetActive(search);
        viewPanel.SetActive(view);
        view2DPanel.SetActive(view2d);
        cube3D.SetActive(view3d);
    }

    private void MessageNotify(string message){
        notifyLabel.text = message;
        notifyLabel.enabled = true;
    }
}
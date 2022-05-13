using System.Threading.Tasks;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private const string BAD_REQUEST_ERROR = "text_badRequestError";
    private const string SOMETHING_WRONG_ERROR = "text_otherError";
    private const string SERVER_ERROR = "text_serverError";
    private const string MESSAGE_LOADING = "text_loading";

    [SerializeField] private MainLogic mainLogic;
    [SerializeField] private LocalizationManager localizationManager;
    [SerializeField] private GameObject searchPanel, viewPanel, view2DPanel, cube3D;
    [SerializeField] private InputField addressField;
    [SerializeField] private Toggle owner, creator, collection;
    [Header("UIErrorPopup")]
    [SerializeField] private GameObject uiPopupCanvas;
    [SerializeField] private LocalizeText notifyLabel;
    [Header("UIInfoPopup")]
    [SerializeField] private GameObject uiInfoCanvas;
    [SerializeField] private LocalizeText infoLabel;

    private void OnEnable() => mainLogic.SearchStatusEvent += SearchResultHandle;

    private void OnDisable() => mainLogic.SearchStatusEvent -= SearchResultHandle;

    private void Start(){
        PanelsVisibleState(true, false, false, false, false);
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

        InfoNotify(MESSAGE_LOADING);
        mainLogic.Search(addressField.text, searchType);
    }

    private void SearchResultHandle(bool searchSuccess, int searchCode){
        if (searchSuccess)
            PanelsVisibleState(false, true, true, false, false);
        else
            ErrorPopupNotify(searchCode);
    }

    public void OnView2DClick() => PanelsVisibleState(false, true, true, false, false);

    public void OnView3DClick() => PanelsVisibleState(false, true, false, true, false);

    public void OnBackButtonClick() => PanelsVisibleState(true, false, false, false, false);

    public void OnEnLanguageButtonClick() => localizationManager.SetLanguage(Language.En);

    public void OnRuLanguageButtonClick() => localizationManager.SetLanguage(Language.Ru);

    private void PanelsVisibleState(bool search, bool view, bool view2d, bool view3d, bool popups){
        searchPanel.SetActive(search);
        viewPanel.SetActive(view);
        view2DPanel.SetActive(view2d);
        cube3D.SetActive(view3d);
        
        uiPopupCanvas.SetActive(popups);
        uiInfoCanvas.SetActive(popups);
    }

    private void ErrorPopupNotify(int errorCode){
        switch (errorCode){
            case 400:
                notifyLabel.key = BAD_REQUEST_ERROR;
                break;
            case 500:
                notifyLabel.key = SERVER_ERROR;
                break;
            default:
                notifyLabel.key = SOMETHING_WRONG_ERROR;
                break;
        }

        uiPopupCanvas.SetActive(true);
    }

    private void InfoNotify(string msg){
        infoLabel.key = msg;
        uiInfoCanvas.SetActive(true);
    }
}
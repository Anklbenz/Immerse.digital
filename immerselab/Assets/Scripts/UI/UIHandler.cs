using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private MainLogic mainLogic;
    [SerializeField] private GameObject searchPanel, viewPanel, view2DPanel, cube3D;
    [SerializeField] private InputField addressField;
    [SerializeField] private Toggle owner, creator, collection;

    private void OnEnable() => mainLogic.SearchStatusEvent += HandleSearchResult;

    private void OnDisable() => mainLogic.SearchStatusEvent -= HandleSearchResult;

    private void Start(){
        PanelsVisibleState(true, false, false, false);
    }

    public void OnSearchClick(){
        mainLogic.Search(addressField.text);
    }

    private void HandleSearchResult(bool searchSuccess){
        if (searchSuccess)
            PanelsVisibleState(false, true, true, false);
    }

    public void OnView2DClick()=>  PanelsVisibleState(false, true, true, false);
    
    public void OnView3DClick()=> PanelsVisibleState(false, true, false, true);
    
    public void OnBackButtonClick()=> PanelsVisibleState(true, false, false, false);

    private void PanelsVisibleState(bool search, bool view, bool view2d, bool view3d){
        searchPanel.SetActive(search);
        viewPanel.SetActive(view);
        view2DPanel.SetActive(view2d);
        cube3D.SetActive(view3d);
    }
}
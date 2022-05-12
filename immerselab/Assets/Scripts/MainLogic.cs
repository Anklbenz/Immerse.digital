using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour
{
   public Action<bool,int> SearchStatusEvent;
   
   [Header("RotatableObject")]
   [SerializeField] private Transform rotatableObject;
   [SerializeField] private float rotationSpeed;
   [Header("Images")]
   [SerializeField] private List<RawImage> imagePanels2D;
   [SerializeField] private List<MeshRenderer> cubeMeshRenderersList;

   private ImageDownloader _imageDownloader;
   private ObjectRotator _objectRotator;
   private PanelsDrawer _panelsDrawer;
   private JsonHandler _jsonHandler;
   private InputReceiver _inputReceiver;

   private void Awake(){
      _jsonHandler = new JsonHandler();
      _inputReceiver = new InputReceiver();
      _imageDownloader = new ImageDownloader();
      _objectRotator = new ObjectRotator(rotatableObject, rotationSpeed, _inputReceiver);
      _panelsDrawer = new PanelsDrawer(imagePanels2D, cubeMeshRenderersList);
   }

   public async void Search(string url, Search searchType){
      var urlList = new List<string>();
      
      try{
         urlList = await _jsonHandler.GetUrlList(url, searchType);
      }
      catch{
         SearchStatusEvent?.Invoke(false, _jsonHandler.ServerResponseCode);
         return;
      }

      var textureList = await _imageDownloader.GetImages(urlList);

      _panelsDrawer.Draw2DImages(textureList);
      _panelsDrawer.DrawCubeSides(textureList);
      SearchStatusEvent?.Invoke(true, _jsonHandler.ServerResponseCode);
   }
   private void Update() => _inputReceiver.Update();
}

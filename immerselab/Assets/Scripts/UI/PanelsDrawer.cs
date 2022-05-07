using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelsDrawer
{
   private readonly List<RawImage> _imagesUI;
   private readonly List<MeshRenderer> _cubeMeshes;

   public PanelsDrawer(List<RawImage> imagesUI, List<MeshRenderer> cubeMeshes){
      _imagesUI = imagesUI;
      _cubeMeshes = cubeMeshes;
   }

   public void Draw2DImages(List<Texture2D> texturesList){
      for (var i = 0; i < _imagesUI.Count; i++)
         _imagesUI[i].texture = texturesList[i];
   }

   public void DrawCubeSides(List<Texture2D> texturesList){
      for (var i = 0; i < _imagesUI.Count; i++)
         _cubeMeshes[i].material.mainTexture = texturesList[i];
   }
}

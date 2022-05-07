using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader
{
    private readonly List<Texture2D> _textureList = new List<Texture2D>();

    public async UniTask<List<Texture2D>> GetImages(List<string> urls){

        foreach (var url in urls){
            var request = UnityWebRequestTexture.GetTexture(url);
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
                Debug.LogError("Image Downloading Error.");
            else
                _textureList.Add(DownloadHandlerTexture.GetContent(request));
        }

        return _textureList;
    }
}

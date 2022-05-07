using Enums;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class JsonHandler
{
    private const string BY_OWNER_URL = "https://ethereum-api.rarible.org/v0.1/nft/items/byOwner?owner=";
    private const string BY_CREATOR_URL = "https://ethereum-api.rarible.org/v0.1/nft/items/byCreator?creator=";
    private const string BY_COLLECTION_URL = "https://ethereum-api.rarible.org/v0.1/nft/items/byCollection?collection=";
    private const string NUMBER_OF_ITEMS = "&size=6";
    private readonly List<string> _spriteUrlList = new List<string>();

    public async UniTask<List<string>> GetUrlList(string address, Search searchType){
        var searchBy = string.Empty;
       
        switch (searchType){
            case Search.ByCollection:
                searchBy = BY_COLLECTION_URL;
                break;
            case Search.ByOwner:
                searchBy = BY_OWNER_URL;
                break;
            case Search.ByCreator:
                searchBy = BY_CREATOR_URL;
                break;
            case Search.None:
                return null;
        }
        
        var url = searchBy + address + NUMBER_OF_ITEMS;
        var imageData = await GetJsonImageData(url);

        foreach (var item in imageData.items)
            _spriteUrlList.Add(item.meta.image.url.ORIGINAL);

        return _spriteUrlList;
    }

    private async UniTask<ImageData> GetJsonImageData(string url){
        var request = UnityWebRequest.Get(url);
        await request.SendWebRequest();
        if (request.isDone)
            //  Debug.Log(request.downloadHandler.text);
            return JsonUtility.FromJson<ImageData>(request.downloadHandler.text);
        
        Debug.LogError("Request undone");
        return null;
    }
}

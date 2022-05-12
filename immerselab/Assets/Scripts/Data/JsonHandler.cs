using System;
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

    private const int REQUEST_OK = 200;
    private const int REQUEST_BAD = 400;
    private const int REQUEST_BAD_SERVER = 500;
    private const int REQUEST_UNKNOWN = 0;
    
    public int ServerResponseCode{ get; private set; } 

    private readonly List<string> _spriteUrlList = new List<string>();

    public async UniTask<List<string>> GetUrlList(string address, Search searchType){
        var url = GetUrl(address, searchType);
        var request = await GetWebRequest(url);
        var imageData = GetJsonImageData(request);

        foreach (var item in imageData.items)
            _spriteUrlList.Add(item.meta.image.url.ORIGINAL);

        return _spriteUrlList;
    }

    private ImageData GetJsonImageData(string request){
        return JsonUtility.FromJson<ImageData>(request);
    }

    private string GetUrl(string address, Search searchType){
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

        return searchBy + address + NUMBER_OF_ITEMS;
    }

    private async UniTask<string> GetWebRequest(string url){
        var request = UnityWebRequest.Get(url);
        var operation = request.SendWebRequest();

        while (!operation.isDone)
            await UniTask.Yield();

        var response = request.responseCode;

        switch (response){
            case REQUEST_OK:
                ServerResponseCode =REQUEST_OK;
                return request.downloadHandler.text;
            case REQUEST_BAD:
                ServerResponseCode =REQUEST_BAD;
                return null;
            case REQUEST_BAD_SERVER:
                ServerResponseCode =REQUEST_BAD_SERVER;
                return null;
        }

        ServerResponseCode =REQUEST_UNKNOWN;
        return null;
    }
}

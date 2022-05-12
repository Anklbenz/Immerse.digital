using System;
using System.Collections.Generic;
/*[System.Serializable]
public class Attribute
{
    public string key;
    public string value;
}*/
[System.Serializable]
public class Creator
{
    public string account;
    public int value;
}
[System.Serializable]
public class Image
{
    public Url url;
   // public Meta meta;
}

[System.Serializable]
public class Item
{
    /*public string id;
    public string contract;
    public string tokenId;*/
    public List<Creator> creators;
    /*public string supply;
    public string lazySupply;*/
    public List<string> owners;
    /*public List<object> royalties;
    public DateTime lastUpdatedAt;
    public DateTime mintedAt;
    public List<object> pending;
    public bool deleted;*/
    public Meta meta;
}
[System.Serializable]
public class Meta
{
    /*public string name;
    public string description;*/
   // public List<Attribute> attributes;
    public Image image;
   // public PREVIEW PREVIEW;
}
/*[System.Serializable]
public class PREVIEW
{
    public string type;
    public int width;
    public int height;
}*/
[System.Serializable]
public class ImageData
{
  //  public int total;
 //   public string continuation;
    public List<Item> items;
}
[System.Serializable]
public class Url
{
    public string PREVIEW;
    public string ORIGINAL;
    public string BIG;
}
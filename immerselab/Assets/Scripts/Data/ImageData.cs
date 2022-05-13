using System.Collections.Generic;

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
    public List<Creator> creators;
    public List<string> owners;
    /*public List<object> royalties;;*/
    public Meta meta;
}
[System.Serializable]
public class Meta
{
   // public List<Attribute> attributes;
    public Image image;
}

[System.Serializable]
public class ImageData
{
    public List<Item> items;
}
[System.Serializable]
public class Url
{
    public string PREVIEW;
    public string ORIGINAL;
    public string BIG;
}
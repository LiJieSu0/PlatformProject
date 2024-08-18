using System;
using System.Reflection.Metadata;
using Godot;
using Godot.Collections;

[Flags]
public enum ItemType{ //TODO configure the types set
    Equipment=1,
    Consumable=2,
    KeyItem=4,
    Valuables=8
}
public partial class ItemModel:Resource{ //Database built in database loader
    public string ItemName;
    public int ItemNo;
    public float Odds;
    public string ItemTexturePath;
    public int BasicPrice;
    public string ItemDescription;
    public int ItemTypes;
    public int StackLimit;

    public ItemModel(int itemNo,string itemName,float odds,string itemTexturePath,int basicPrice,string itemDescription,int types,int stackLimit) { 
        this.ItemName=itemName;
        this.ItemNo=itemNo;
        this.Odds=odds;
        this.ItemTexturePath=itemTexturePath;
        this.BasicPrice=basicPrice;
        this.ItemDescription=itemDescription;
        this.ItemTypes=types;
        this.StackLimit=stackLimit;
    }
}
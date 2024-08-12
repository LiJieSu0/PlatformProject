using Godot;

public partial class ItemModel:Resource{
    public string ItemName;
    public int ItemNo;
    public float Odds;
    public string ItemTexturePath;
    public int BasicPrice;

    public ItemModel(int itemNo,string itemName,float odds,string itemTexturePath,int basicPrice) { 
        this.ItemName=itemName;
        this.ItemNo=itemNo;
        this.Odds=odds;
        this.ItemTexturePath=itemTexturePath;
        this.BasicPrice=basicPrice;
    }
}
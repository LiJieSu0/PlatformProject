using Godot;

public partial class ItemModel:Resource{
    public string ItemName;
    public int ItemNo;
    public float Odds;

    public ItemModel(int itemNo,string itemName,float odds) { 
        this.ItemName=itemName;
        this.ItemNo=itemNo;
        this.Odds=odds;
    }
}
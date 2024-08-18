
public class Item{

    public enum ItemType{
        Equipment,
        Consumable,
        KeyItem,
        Valuables
    }

    #region Variables
    public string _itemName;
    public int _itemNo;
    public ItemType[] _itemTypes;
    public string _itemDescription; //TODO load descriptions from json
    #endregion


}
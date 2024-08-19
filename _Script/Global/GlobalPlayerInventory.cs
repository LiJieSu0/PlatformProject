using Godot;
using Godot.Collections;
public partial class GlobalPlayerInventory:Node,ISaveable{
    public static GlobalPlayerInventory Instance { get; private set;}
    public delegate Variant SaveInventory();
    public event SaveInventory SaveInventoryEvent;
    public Variant LoadedInventoryData;
    public override void _Ready(){
        Instance=this;
    }
    public Variant SaveInventoryTrigger(){
        return SaveInventoryEvent.Invoke();
    }

    public string Save(){
        return Json.Stringify(new Dictionary<string,Variant>(){
            {this.Name, SaveInventoryTrigger()}
        });
    }

    public void Load(Variant variant){
        LoadedInventoryData=variant;
    }

    public string NewSave(){
        return Json.Stringify(new Dictionary<string,Variant>(){ //TODO make sure inventory is empty
            {this.Name, SaveInventoryTrigger()}
        });
    }
}
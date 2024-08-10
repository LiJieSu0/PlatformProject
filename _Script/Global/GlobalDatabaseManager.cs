using Godot;
using Godot.Collections;
public partial class GlobalDatabaseManager:Node{
    public static GlobalDatabaseManager Instance { get; private set; }
    private const string FILE_PATH="res://_Asset/Database/ItemList.json";

    public override void _Ready(){
		Instance=this;
        GD.Print("Load database here");
        LoadDatabase();
    }

    private void LoadDatabase(){
        if (!FileAccess.FileExists(FILE_PATH)){
            GD.Print("Database is no exist");
            return;
        }
        var json = new Json();
        using var dataLoader = FileAccess.Open(FILE_PATH, FileAccess.ModeFlags.Read); 
        var jsonData=Json.ParseString(dataLoader.GetAsText());
        var data=new Godot.Collections.Dictionary<string, Variant>((Dictionary)jsonData);
        var itemList=(Array)data["ItemList"];

        foreach(var item in itemList){
            var tmp=new Dictionary<string, Variant>((Dictionary)item);
            GD.Print("Item number" + tmp["ItemNo"]);
            GD.Print("Item Name" + tmp["ItemName"]);
            GD.Print("Item Odds" + tmp["Odds"]);


        }
    }

}


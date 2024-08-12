using System;
using Godot;
using Godot.Collections;
public partial class GlobalDatabaseManager:Node{
    public static GlobalDatabaseManager Instance { get; private set; }
    private const string FILE_PATH="res://_Asset/Database/ItemList.json";
    public Dictionary <int,Variant> ItemListDB=new Dictionary<int,Variant>();
    public override void _Ready(){
		Instance=this;
        LoadDatabase();
    }

    private void LoadDatabase(){
        if (!FileAccess.FileExists(FILE_PATH)){
            GD.Print("Database doesn't exist");
            return;
        }
        var json = new Json();
        using var dataLoader = FileAccess.Open(FILE_PATH, FileAccess.ModeFlags.Read); 
        var jsonData=Json.ParseString(dataLoader.GetAsText());
        var data=new Dictionary<string, Variant>((Dictionary)jsonData); //Load whole data from sheet
        var itemList=(Godot.Collections.Array)data["ItemList"];
        var enemyList=(Godot.Collections.Array)data["EnemyList"];

        LoadItemList(itemList);
        LoadEnemyList(enemyList);
    }



    private void LoadItemList(Godot.Collections.Array itemList){
        foreach(var item in itemList){
            var tmp=new Dictionary<string, Variant>((Dictionary)item);
            ItemModel tmpItemModel=new ItemModel((int)tmp["ItemNo"],
                                                    (string)tmp["ItemName"],
                                                    (float)tmp["Odds"],
                                                    (string)tmp["ItemTexturePath"],
                                                    (int)tmp["BasicPrice"]);
                                                    
            ItemListDB[(int)tmp["ItemNo"]]=tmpItemModel;
            ItemModel tmpModel=(ItemModel)ItemListDB[(int)tmp["ItemNo"]];
        }
    }

    private void LoadEnemyList(Godot.Collections.Array enemyList){
        foreach(var enemy in enemyList){
            var tmp=new Dictionary<string, Variant>((Dictionary)enemy);
            //TODO create enemy model
        }
    }

}


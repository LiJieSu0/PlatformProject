using Godot;
using Godot.Collections;
using System;
using System.Runtime.CompilerServices;

public partial class GlobalPlayerLoadInScene : Node{
	public static GlobalPlayerLoadInScene Instance;

  public int PlayerLevel;
  public int PlayerStrength;
  public int PlayerVitality;
  public int PlayerIntelligence;
  public int PlayerDexterity;
    public override void _Ready(){
		  Instance=this;
      LoadSavedStatus();
      GD.Print("Loaded: "+PlayerDexterity);
    }

    private void LoadSavedStatus(){
      if (!FileAccess.FileExists("user://savegame.save")){
        //No save exist create new save
        using var newSaveWriter = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
        var newSave=new Dictionary<string, Variant>(){
        {"PlayerLevel",1},
        {"PlayerStrength",10},
        {"PlayerVitality",10},
        {"PlayerIntelligence",10},
        {"PlayerDexterity",10},
        };
        newSaveWriter.StoreLine(Json.Stringify(newSave));
      }
      using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);
      var jsonString = saveGame.GetLine();
      var json = new Json();
      var parseResult = json.Parse(jsonString);
      var data = new Dictionary<string, Variant>((Dictionary)json.Data);
      FromDict(data);
    }
    public void SaveGame(){
      using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);

      var saveNodes = GetTree().GetNodesInGroup("Persist");
      // foreach (Node saveNode in saveNodes){
      //   if (string.IsNullOrEmpty(saveNode.SceneFilePath)){
      //     GD.Print($"persistent node '{saveNode.Name}' is not an instanced scene, skipped");
      //     continue;
      //   }

      //   if (!saveNode.HasMethod("Save")){
      //     GD.Print($"persistent node '{saveNode.Name}' is missing a Save() function, skipped");
      //     continue;
      //   }

      //   var nodeData = saveNode.Call("Save");
      //   var jsonString = Json.Stringify(nodeData);
      //   saveGame.StoreLine(jsonString);
		  //   }
      var data=new Dictionary<string, Variant>(){
        {"PlayerLevel",PlayerLevel},
        {"PlayerStrength",PlayerStrength},
        {"PlayerVitality",PlayerVitality},
        {"PlayerIntelligence",PlayerIntelligence},
        {"PlayerDexterity",PlayerDexterity},
      };

	}

    public void FromDict(Dictionary<string, Variant> data){
        PlayerLevel = (int)data["PlayerLevel"];
        PlayerStrength = (int)data["PlayerStrength"];
        PlayerVitality = (int)data["PlayerVitality"];
        PlayerIntelligence = (int)data["PlayerIntelligence"];
        PlayerDexterity=(int)data["PlayerDexterity"];
    }

}

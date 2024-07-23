using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class GlobalSaveManager : Node
{
	public static GlobalSaveManager Instance { get; private set;}
	public override void _Ready()
	{
		Instance=this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void WriteSave(){
		using var newSaveWriter = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
		var root=GetTree().Root;
		for(int i=0;i<root.GetChildCount();i++){
			if(root.GetChild(i) is ISaveable saveable){
				string data=saveable.Save();
 				newSaveWriter.StoreLine(data);
			}
		}
	}

	public void LoadSave(){ //TODO need to be tested
		var root=GetTree().Root;
		if (!FileAccess.FileExists("user://savegame.save")){
			//No save exist, create new save
			using var newSaveWriter = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
			for(int i=0;i<root.GetChildCount();i++){
				if(root.GetChild(i) is ISaveable saveable){
					string tmpData=saveable.NewSave();
					newSaveWriter.StoreLine(tmpData);
				}
			}
			newSaveWriter.Close();
		}
		
		using var saveLoader = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read); 
		while (saveLoader.GetPosition() < saveLoader.GetLength()){
			string jsonString = saveLoader.GetLine();
			var json = new Json();
			var parseResult = json.Parse(jsonString);
			var data = new Godot.Collections.Dictionary<string, Variant>((Dictionary)json.Data);
			
			foreach(KeyValuePair<string,Variant>kvp in data){
				var loadNode=root.GetNode(kvp.Key) as ISaveable;
				loadNode.Load(kvp.Value);
			}

		}
    }

	
}

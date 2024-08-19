using Godot;
using Godot.Collections;
using System;

public partial class GlobalSceneManager : Node,ISaveable
{
	public static GlobalSceneManager Instance { get; private set;}
	public PackedScene _currScene;
	public override void _Ready()
	{
		Instance=this;
	}

	public override void _Process(double delta)
	{
	}
	
	public void ChangeScene(string filePath){
		GetTree().ChangeSceneToFile(filePath);
	}
	public void ChangeScene(PackedScene packedScene){
		GetTree().ChangeSceneToPacked(packedScene);
	}

    public string Save()
    {
		string currentScene=GetTree().CurrentScene.Name;
		var data=new Dictionary<string,string>(){
			{this.Name,currentScene}
		};
		string jsonString = Json.Stringify(data);
		// return jsonString; //TODO load corrrect scene
		return "";
	}

    public void Load(Variant variant){
		return; //TODO fix load scene
		//TODO error handle
		string scenePath=ScenePathDict.SCENE_DICT[(string)variant];
		//TODO change to correct scene path;
		// PackedScene packedScene = (PackedScene)ResourceLoader.Load(scenePath);
        // if (packedScene != null){
        //     ChangeScene(packedScene);
        //     GD.Print("Scene loaded successfully: " + scenePath);
        // }else{
        //     GD.PrintErr("Error loading scene: " + scenePath);
        // }
    }

	public string NewSave(){
		var data=new Dictionary<string,string>(){
			{this.Name,"firstScene"} //TODO change to real first scene;
		};
		string jsonString = Json.Stringify(data);
		return jsonString;
	}

}

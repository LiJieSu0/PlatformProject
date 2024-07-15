using Godot;
using System;

public partial class GlobalSceneManager : Node
{
	public static GlobalSceneManager Instance { get; private set;}
	public override void _Ready()
	{
		Instance=this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void ChangeScene(string filePath){
		GetTree().ChangeSceneToFile(filePath);
	}
	public void ChangeScene(PackedScene packedScene){
		GetTree().ChangeSceneToPacked(packedScene);
	}
}

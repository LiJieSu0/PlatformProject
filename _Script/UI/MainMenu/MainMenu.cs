using Godot;
using System;

public partial class MainMenu : Control
{
	AudioStreamPlayer2D btnSEPlayer;
	public override void _Ready()
	{
		btnSEPlayer=GetNode<AudioStreamPlayer2D>("BtnSE");
		GD.Print(btnSEPlayer==null);
	}

	public override void _Process(double delta)
	{
	}
	private void OnStartBtnPressed(){
		btnSEPlayer.Play();
	}
	private void OnQuitBtnPressed(){
		GetTree().Quit();
	}
	private void OnBtnSEFinsihed(){
		GlobalSceneManager.Instance.ChangeScene("res://_Scene/FirstStage/MainScene.tscn");
	}
}

using Godot;
using System;

public partial class PauseMenuScript : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(GlobalEventPublisher.IsPause==false){
			this.Hide();
		}
		else{
			this.Show();
		}
	}

	private void OnCloseBtnPressed(){
		this.Hide();
		GlobalEventPublisher.IsPause=false;
	}
}

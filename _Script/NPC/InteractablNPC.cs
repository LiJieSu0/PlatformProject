using Godot;
using System;

public partial class InteractablNPC : Area2D,INPC
{
	#region Node
	Control control;
	#endregion

	#region Variables
	private bool isPlayerNear=false;
	#endregion
	public override void _Ready()
	{
		control=GetNode<Control>("Control");

	}

	public override void _Process(double delta){	

	}
	private void OnAreaEnter(Area2D area){
		isPlayerNear=true;
		control.Show();
	}
	private void OnAreaExit(Area2D area){
		isPlayerNear=false;
		control.Hide();
	}

    public void InteractReaction()
    {
		GD.Print("Show dialogue");
    }

}

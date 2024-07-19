using Godot;
using System;

public partial class InventoryMenu : Control
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	private void OnCloseBtnPressed(){
		GD.Print("pressed");
		this.Hide();
	}
}

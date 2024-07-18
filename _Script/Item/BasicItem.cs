using Godot;
using System;

public partial class BasicItem : RigidBody2D
{
	//TODO load item resource
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	private void OnPlayerEnter(Node2D player){
		GD.Print("Player enter");//TODO player pick up function
	}
}

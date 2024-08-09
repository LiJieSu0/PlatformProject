using Godot;
using System;

public partial class BasicItem : RigidBody2D
{
	[Export] ItemRes itemRes;

	#region Variables
	public const float disappearTime=10f;
	#endregion


	#region Node
	Sprite2D sprite2D;
	Area2D pickUpArea;
	Timer disappearTimer;
	#endregion

	public override void _Ready(){
	}

	public override void _Process(double delta)
	{
	}

	public void InitializeNode(){
		sprite2D=GetNode<Sprite2D>("Sprite2D");
		pickUpArea=GetNode<Area2D>("PickUpArea");
		disappearTimer=GetNode<Timer>("DisappearTimer");
	}

	public void InitializeVariables(){
		disappearTimer.WaitTime=disappearTime;
	}

	public void InitialSignal(){
		
	}

}

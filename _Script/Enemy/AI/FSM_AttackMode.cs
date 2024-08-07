using Godot;
using System;

public partial class FSM_AttackMode : StateNode
{
	public override void Enter(){
		GD.Print("FollowEnter");

	}
	public override void Exit(){
		GD.Print("FollowExit");
		
	}
}

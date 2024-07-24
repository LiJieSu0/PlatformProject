using Godot;
using System;

public partial class FSM_Patrol : StateNode{
	public override void Enter(){
		GD.Print("PatrolEnter");
	}
	public override void Exit(){
		GD.Print("PatrolExit");
		
	}
}

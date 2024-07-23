using Godot;
using System;

public partial class Red : StateNode
{
	private float timer;

    public override void Enter(){
		GD.Print("Enter initial");
		timer=0;
    }

	public override void Exit(){
		GD.Print("Exit");
	}
    public override void _Ready(){
	}
    public override void ReadyInitial(){
		GD.Print("ReadyInitial");
    }
    public override void _Process(double delta){
		

	}

    public override void Update(float delta){
		timer+=delta;
		GD.Print("Red PhysicUpdateing");
		if(timer>2){
			GD.Print("Red time up");
			fsm.TransitionTo("Green");
		}
    }
}

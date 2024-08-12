using Godot;
using System;

public partial class StateNode : Node
{
	public StateMachine fsm;
	public virtual void Enter(){}
	public virtual void Exit(){}
	public virtual void ReadyInitial(){}
	public virtual void Update(){}
	public virtual void Update(float delta){}
	public virtual void PhysicsUpdate(){}

}

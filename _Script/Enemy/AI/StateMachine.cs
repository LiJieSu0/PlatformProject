using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export] public NodePath initialState;
	private Dictionary<string,StateNode> _stateDict;
	private StateNode _currState;
	public override void _Ready()
	{
		_stateDict=new Dictionary<string,StateNode>();
		foreach(Node node in GetChildren()){
			if(node is StateNode s){
				_stateDict[node.Name]=s;
				s.fsm=this;
				s.ReadyInitial();
				s.Exit();
			}
		}
		_currState=GetNode<StateNode>(initialState);
		_currState.Enter();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_currState.Update((float)delta);
	}

	public void TransitionTo(string key){
		if(!_stateDict.ContainsKey(key)||_currState==_stateDict[key])
			return;
		_currState.Exit();
		_currState=_stateDict[key];
		_currState.Enter();

	}
}

using Godot;
using System;

public partial class StatusManager : Node2D
{
	#region PlayerBasicStatus
	[Export]public float MaxHp;
	[Export]public float MaxMp;
	[Export]public float CurrHp;
	[Export]public float CurrMp;
	[Export]public float AttackPower;
	#endregion

	#region Skill
	
	#endregion
	public override void _Ready()
	{
		InitializeStatus();
	}

	public override void _Process(double delta)
	{
	}
	private void InitializeStatus(){
		MaxHp=100;
		MaxMp=100;
		CurrHp=MaxHp;
		CurrMp=MaxMp;
		AttackPower=10;
	}

}

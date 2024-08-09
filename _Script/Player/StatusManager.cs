using Godot;
using System;
using System.Collections.Generic;

public partial class StatusManager : Node2D
{
	#region PlayerBasicStatus
	[Export]public float MaxHp;
	[Export]public float MaxMp;
	[Export]public float CurrHp;
	[Export]public float CurrMp;
	[Export]public float AttackPower;
	public int CurrSkillIdx;
	public int CurrProjectileIdx;
	public int CurrExp;
	public int CurrMoney;

	#endregion

	#region Skill
	public List<SkillResource> SkillList=new List<SkillResource>();
	public List<PackedScene> ProjectileList=new List<PackedScene>();
	#endregion

	public override void _Ready()
	{
		InitializeStatus();
		//TODO Load skill from list 
		//TODO Load skill dynamically
		SkillList.Add((SkillResource)ResourceLoader.Load("res://_Resource/Skills/Single.tres"));
		SkillList.Add((SkillResource)ResourceLoader.Load("res://_Resource/Skills/Burst.tres"));
		SkillList.Add((SkillResource)ResourceLoader.Load("res://_Resource/Skills/Shot.tres"));
		ProjectileList.Add((PackedScene)ResourceLoader.Load("res://_Scene/Projectiles/BasicProjectile.tscn"));
		CurrSkillIdx=0;
		CurrProjectileIdx=0;
		CurrExp=0;//TODO Load exp and moeny from save
		CurrMoney=0;
		GlobalEventPublisher.Instance.EnemyDeadEvent+=UpdateExpAndMoney;
	}

	public override void _Process(double delta)
	{
	}
	private void InitializeStatus(){
		MaxHp=100;
		MaxMp=100;

		AttackPower=10;
	}
	private void UpdateExpAndMoney(string enemyName){
		//TODO load enemy exp sheet through enemy name

	}

}

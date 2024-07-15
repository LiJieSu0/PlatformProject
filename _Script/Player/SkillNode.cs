using Godot;
using System;

public partial class SkillNode : Node2D,ISkill
{
	[Export] SkillResource skillResource;
	[Export] PackedScene skillProjectile;

	private StatusManager statusManager;
	private float manaCost;
	private string skillName;
	private float skillCoolDownTime;
	private bool isCoolDown=false;
	private float time;
	public override void _Ready(){
		InitializeFromResource();
		statusManager=GetParent().GetParent().GetNode<StatusManager>("StatusManager");
		time=0;
	}

	public override void _PhysicsProcess(double delta){
		CoolDownTimer((float)delta);
	}

    public void CastSkill(){
		if(statusManager.CurrMp>=manaCost&&!isCoolDown){
			skillResource.CastRangedSkill(skillProjectile,this,3);
			statusManager.CurrMp-=manaCost;
			isCoolDown=true;	
		}
    }
	private void InitializeFromResource(){
		manaCost=skillResource.ManaCost;
		skillName=skillResource.SkillName;
		skillCoolDownTime=skillResource.SkillCoolDownTime;
	}

	private void CoolDownTimer(float delta){
		if(isCoolDown){
			time+=delta;
		}
		if(time>=this.skillCoolDownTime){
			isCoolDown=false;
			time=0;
		}
	}

}

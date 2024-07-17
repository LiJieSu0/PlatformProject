using Godot;
using System;

public partial class SkillNode : Node2D,ISkill
{
	[Export] PackedScene skillProjectile; //TODO load from somewhere else

	SkillResource skillResource;
	private StatusManager statusManager;
	private PlayerBody player;
	private bool isFaceRight;
	private float manaCost;
	private string skillName;
	private float skillCoolDownTime;
	private bool isCoolDown=false;
	private float time;
	public override void _Ready(){
		GlobalEventPublisher.Instance.SkillChangeEvent+=ChangeCurrSkill;
		statusManager=GetParent().GetParent().GetNode<StatusManager>("StatusManager");
		ChangeCurrSkill();
		player=(PlayerBody)GetParent();
		time=0;
	}

	public override void _PhysicsProcess(double delta){
		CoolDownTimer((float)delta);
		isFaceRight=player.isFaceRight;
	}

    public void CastSkill(){
		if(statusManager.CurrMp>=manaCost&&!isCoolDown){
			skillResource.CastSkil(skillProjectile,this,isFaceRight,3);
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


	public void ChangeCurrSkill(int idx=0){
		skillResource=statusManager.SkillList[statusManager.CurrSkillIdx];
		InitializeFromResource();
	}
	public void ChangeCurrProjectile(){

	}
}

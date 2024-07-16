using Godot;
using System;
using System.Collections.Generic;

public partial class HUDManager : CanvasLayer
{
	#region Node
	TextureProgressBar hpBar;
	TextureProgressBar mpBar;
	StatusManager statusManager;
	List<Panel> skillPanels=new List<Panel>();

	#endregion 

	public override void _Ready(){
		GlobalEventPublisher.Instance.SkillChangeEvent+=updateSkillActivate;
		hpBar=GetNode<TextureProgressBar>("PlayerStatus/HpBar");
		mpBar=GetNode<TextureProgressBar>("PlayerStatus/MpBar");
		statusManager=GetParent().GetNode<StatusManager>("StatusManager");
		hpBar.MaxValue=statusManager.MaxHp;
		mpBar.MaxValue=statusManager.MaxMp;
		for(int i=0;i<GetNode("SkillSet").GetChildCount();i++){
			skillPanels.Add((Panel)GetNode("SkillSet").GetChild(i).GetNode("Mask"));
		}
		updateSkillActivate(statusManager.CurrSkillIdx);

	}

	public override void _Process(double delta)
	{
		updateStatus();
	}

	private void updateStatus(){
		hpBar.Value=statusManager.CurrHp;
		mpBar.Value=statusManager.CurrMp;
	}

	private void updateSkillActivate(int idx){
		for (int i = 0; i < skillPanels.Count; i++){
			if (i ==idx){
				skillPanels[i].Hide();
			}else{
				skillPanels[i].Show();
			}
		}	
	}
}

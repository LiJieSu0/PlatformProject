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

	public override void _Ready()
	{
		hpBar=GetNode<TextureProgressBar>("PlayerStatus/HpBar");
		mpBar=GetNode<TextureProgressBar>("PlayerStatus/MpBar");
		statusManager=GetParent().GetNode<StatusManager>("StatusManager");
		hpBar.MaxValue=statusManager.MaxHp;
		mpBar.MaxValue=statusManager.MaxMp;
		for(int i=0;i<GetNode("SkillSet").GetChildCount();i++){
			skillPanels.Add((Panel)GetNode("SkillSet").GetChild(i).GetNode("Mask"));
		}
		for(int i=0;i<skillPanels.Count;i++){
			GD.Print(skillPanels[i].Name);
		}
	}

	public override void _Process(double delta)
	{
		updateStatus();
		updateSkillActivate();

	}

	private void updateStatus(){
		hpBar.Value=statusManager.CurrHp;
		mpBar.Value=statusManager.CurrMp;
	}

	private void updateSkillActivate(){
		for (int i = 0; i < skillPanels.Count; i++){
			if (i == statusManager.CurrSkillIdx){
				skillPanels[i].Hide();
			}else{
				skillPanels[i].Show();
			}
		}	
	}
}

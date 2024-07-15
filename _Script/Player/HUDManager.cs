using Godot;
using System;

public partial class HUDManager : CanvasLayer
{
	#region Node
	TextureProgressBar hpBar;
	TextureProgressBar mpBar;
	StatusManager statusManager;

	#endregion 

	public override void _Ready()
	{
		hpBar=GetNode<TextureProgressBar>("PlayerStatus/HpBar");
		mpBar=GetNode<TextureProgressBar>("PlayerStatus/MpBar");
		statusManager=GetParent().GetNode<StatusManager>("StatusManager");
		hpBar.MaxValue=statusManager.MaxHp;
		mpBar.MaxValue=statusManager.MaxMp;

	}

	public override void _Process(double delta)
	{
		updateStatus();
	}

	private void updateStatus(){
		hpBar.Value=statusManager.CurrHp;
		mpBar.Value=statusManager.CurrMp;
	}
}

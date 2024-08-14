using Godot;
using System;

public partial class UI_TabMenu : Control{

	#region Node
	private UI_TabStatsMenu _statsMenu;
	private UI_TabInfos _infos;
	#endregion
	public override void _Ready(){
	}

	private void InitializeNode(){
		_statsMenu=GetNode<UI_TabStatsMenu>("Panel/TabContainer/Stats/MarginContainer/HBoxContainer/StatsContainer");
		_infos=GetNode<UI_TabInfos>("Panel/TabContainer/Stats/MarginContainer/HBoxContainer/InfosContainer");
	}
	public void ShowTabMenu(){
		this.Show();
		_infos.UpdateLabel();
	}
}

using Godot;
using System;

public partial class UI_TabMenu : Control{

	#region Node
	private UI_TabStats _statsMenu;
	private UI_TabInfos _infos;
	#endregion
	public override void _Ready(){
		GlobalEventPublisher.Instance.ShowTabMenuEvent+=ToggleTabMenu;
	}



    private void InitializeNode(){
		_statsMenu=GetNode<UI_TabStats>("Panel/TabContainer/Stats/MarginContainer/HBoxContainer/StatsContainer");
		_infos=GetNode<UI_TabInfos>("Panel/TabContainer/Stats/MarginContainer/HBoxContainer/InfosContainer");
	}
	public void ShowTabMenu(){
		this.Show();
		_infos.UpdateLabel();
	}
	public void HideTabMenu(){
		this.Hide();
	}
	private void ToggleTabMenu(){
        if(!GlobalEventPublisher.Instance.isShowMenu){
			this.Show();
			GlobalEventPublisher.Instance.isShowMenu=true;
		}
		else{
			this.Hide();
			GlobalEventPublisher.Instance.isShowMenu=false;
		}
    }
}

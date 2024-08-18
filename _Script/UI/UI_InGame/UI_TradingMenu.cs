using Godot;
using System;

public partial class UI_TradingMenu : Control
{
	#region Node
	UIFrameWithCloseBtn _closeFrame;
	#endregion

	public override void _Ready()
	{
		InitializeNode();
		InitializeSignal();

	}

	public override void _Process(double delta)
	{
	}

	private void InitializeNode(){
		_closeFrame=GetNode<UIFrameWithCloseBtn>("Panel/UIFrameWithCloseBtn");
	}

	private void InitializeSignal(){
		_closeFrame.CloseButtonPressed+=()=>{
			this.Hide();
		};
		GlobalEventPublisher.Instance.ShowTradingMenuEvent+=ShowTradingMenu;
	}

	public void ShowTradingMenu(){
		this.Show();
	}


}

using Godot;
using System;

public partial class UI_TabInfos : Control{
	#region Node
	private Label _currExpLabel;
	private Label _currMoneyLabel;
	#endregion
	public override void _Ready(){
		InitializeNode();
		UpdateLabel();
	}

	private void InitializeNode(){
		_currExpLabel=GetNode<Label>("Exp/CurrExpLabel");
		_currMoneyLabel=GetNode<Label>("Money/CurrMoneyLabel");
	}

	public void UpdateLabel(){
		_currExpLabel.Text=GlobalPlayerStats.Instance.PlayerCurrAccumExp.ToString();
		_currMoneyLabel.Text=GlobalPlayerStats.Instance.PlayerMoney.ToString();
	}
	


}

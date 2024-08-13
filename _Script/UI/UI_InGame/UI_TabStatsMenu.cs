using Godot;
using System;

public partial class UI_TabStatsMenu : TabBar{

	#region Node
	private Label _levelLabel;
	private Label _strLabel;
	private Label _vitLabel;
	private	Label _intLabel;
	private Label _dexLabel;
	#endregion	
	
	#region Variables
	private GlobalPlayerStats _globalPlayerStats;
	public int _currLevel;
	public int _currStr;
	public int _currVit;
	public int _currInt;
	public int _currDex;
	public int _currUpgradePoints;
	#endregion
	public override void _Ready(){
		_globalPlayerStats=GlobalPlayerStats.Instance;
		InitializeVariables();
		InitializeNode();
	}

	private void InitializeNode(){
		_levelLabel=GetNode<Label>("MarginContainer/VBoxContainer/Level/CurrLevel");
		_strLabel=GetNode<Label>("MarginContainer/VBoxContainer/Strength/CurrStr");
		_vitLabel=GetNode<Label>("MarginContainer/VBoxContainer/Vitality/CurrVit");
		_intLabel=GetNode<Label>("MarginContainer/VBoxContainer/Intelligence/CurrInt");
		_dexLabel=GetNode<Label>("MarginContainer/VBoxContainer/Dexterity/CurrDex");

		_levelLabel.Text=_currLevel.ToString();
		_strLabel.Text=_currStr.ToString();
		_vitLabel.Text=_currVit.ToString();
		_intLabel.Text=_currInt.ToString();
		_dexLabel.Text=_currDex.ToString();

	}

	private void InitializeVariables(){
		_currLevel=_globalPlayerStats.PlayerLevel;
		_currStr=_globalPlayerStats.PlayerStrength;
		_currVit=_globalPlayerStats.PlayerVitality;
		_currInt=_globalPlayerStats.PlayerIntelligence;
		_currDex=_globalPlayerStats.PlayerDexterity;
		_currUpgradePoints=_globalPlayerStats.PlayerUpgradePoints;
	}

}

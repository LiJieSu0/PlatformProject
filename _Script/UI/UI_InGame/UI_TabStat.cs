using Godot;
using System;

public partial class UI_TabStatsMenu : Control{

	#region Node
	private Label _levelLabel;
	private Label _strLabel;
	private Label _vitLabel;
	private	Label _intLabel;
	private Label _dexLabel;
	private Label _lukLabel;

	private Label _upgradePtsLabel;
	private Label _currUpgradePtsLabel;

	private Button _strIncreaseBtn;
	private Button _strDecreaseBtn;
	private Button _vitIncreaseBtn;
	private Button _vitDecreaseBtn;
	private Button _intIncreaseBtn;
	private Button _intDecreaseBtn;
	private Button _dexIncreaseBtn;
	private Button _dexDecreaseBtn;
	private Button _lukIncreaseBtn;
	private Button _lukDecreaseBtn;

	
	private Button _assignConfirmedBtn;
	#endregion	
	
	#region Variables
	private GlobalPlayerStats _globalPlayerStats;
	public int _currLevel;
	public int _currStr;
	public int _currVit;
	public int _currInt;
	public int _currDex;
	public int _currLuk;
	public int _currUpgradePoints;
	private bool isAssignable=false;
	#endregion
	public override void _Ready(){
		_globalPlayerStats=GlobalPlayerStats.Instance;
		InitializeNode();
		InitializeVariables();
		InitializeSignal();

		UpdateLabel();
		CheckIsAssignable();

	}

	private void InitializeNode(){
		#region LabelNode
		_levelLabel=GetNode<Label>("Level/CurrLevel");
		_strLabel=GetNode<Label>("Strength/CurrStr");
		_vitLabel=GetNode<Label>("Vitality/CurrVit");
		_intLabel=GetNode<Label>("Intelligence/CurrInt");
		_dexLabel=GetNode<Label>("Dexterity/CurrDex");
		_lukLabel=GetNode<Label>("Luck/CurrLuk");
		_upgradePtsLabel=GetNode<Label>("Level/UpgradePtsLabel");
		_currUpgradePtsLabel=GetNode<Label>("Level/CurrUpgradePtsLabel");
		#endregion

		#region StatsButtonNode
		_strIncreaseBtn=GetNode<Button>("Strength/IncreaseBtn");
		_strDecreaseBtn=GetNode<Button>("Strength/DecreaseBtn");

		_vitIncreaseBtn=GetNode<Button>("Vitality/IncreaseBtn");
		_vitDecreaseBtn=GetNode<Button>("Vitality/DecreaseBtn");

		_intIncreaseBtn=GetNode<Button>("Intelligence/IncreaseBtn");
		_intDecreaseBtn=GetNode<Button>("Intelligence/DecreaseBtn");

		_dexIncreaseBtn=GetNode<Button>("Dexterity/IncreaseBtn");
		_dexDecreaseBtn=GetNode<Button>("Dexterity/DecreaseBtn");

		_lukIncreaseBtn=GetNode<Button>("Luck/IncreaseBtn");
		_lukDecreaseBtn=GetNode<Button>("Luck/DecreaseBtn");
		#endregion
		
		_assignConfirmedBtn=GetNode<Button>("AssignConfirmedBtn");
	}



	private void InitializeVariables(){
		_currLevel=_globalPlayerStats.PlayerLevel;
		_currStr=_globalPlayerStats.PlayerStrength;
		_currVit=_globalPlayerStats.PlayerVitality;
		_currInt=_globalPlayerStats.PlayerIntelligence;
		_currDex=_globalPlayerStats.PlayerDexterity;
		_currLuk=_globalPlayerStats.PlayerLuck;
		_currUpgradePoints=_globalPlayerStats.PlayerUpgradePoints;
	}


	private void InitializeSignal(){
		_strIncreaseBtn.Pressed += () => AdjustStat(ref _currStr, _globalPlayerStats.PlayerStrength, 1);
		_strDecreaseBtn.Pressed += () => AdjustStat(ref _currStr, _globalPlayerStats.PlayerStrength, -1);

		_vitIncreaseBtn.Pressed += () => AdjustStat(ref _currVit, _globalPlayerStats.PlayerVitality, 1);
		_vitDecreaseBtn.Pressed += () => AdjustStat(ref _currVit, _globalPlayerStats.PlayerVitality, -1);

		_intIncreaseBtn.Pressed += () => AdjustStat(ref _currInt, _globalPlayerStats.PlayerIntelligence, 1);
		_intDecreaseBtn.Pressed += () => AdjustStat(ref _currInt, _globalPlayerStats.PlayerIntelligence, -1);

		_dexIncreaseBtn.Pressed += () => AdjustStat(ref _currDex, _globalPlayerStats.PlayerDexterity, 1);
		_dexDecreaseBtn.Pressed += () => AdjustStat(ref _currDex, _globalPlayerStats.PlayerDexterity, -1);

		_lukIncreaseBtn.Pressed += () => AdjustStat(ref _currLuk, _globalPlayerStats.PlayerLuck, 1);
		_lukDecreaseBtn.Pressed += () => AdjustStat(ref _currLuk, _globalPlayerStats.PlayerLuck, -1);
		
		_assignConfirmedBtn.Pressed+=OnAssignConfirmedBtnPressed;
	}

	private void OnstrBtn(){
		GD.Print("in");
	}

	private void AdjustStat(ref int stat, int baseStat, int adjustment){
		GD.Print("In adjust "+_currUpgradePoints);
		if (adjustment > 0 && _currUpgradePoints <= 0)
			return;
		if (adjustment < 0 && stat == baseStat)
			return;

		stat += adjustment;
		_currUpgradePoints -= adjustment;
		UpdateLabel();
	}
	private void UpdateLabel(){
		_levelLabel.Text=_currLevel.ToString();
		_strLabel.Text=_currStr.ToString();
		_vitLabel.Text=_currVit.ToString();
		_intLabel.Text=_currInt.ToString();
		_dexLabel.Text=_currDex.ToString();
		_lukLabel.Text=_currLuk.ToString();
		_currUpgradePtsLabel.Text=_currUpgradePoints.ToString();
	}

	private void OnAssignConfirmedBtnPressed(){
		_globalPlayerStats.PlayerStrength=_currStr;
		_globalPlayerStats.PlayerVitality=_currVit;
		_globalPlayerStats.PlayerIntelligence=_currInt;
		_globalPlayerStats.PlayerDexterity=_currDex;
		_globalPlayerStats.PlayerLuck=_currLuk;
		_globalPlayerStats.PlayerUpgradePoints=_currUpgradePoints;
		GD.Print("CURR global player luck "+GlobalPlayerStats.Instance.PlayerLuck);
		CheckIsAssignable();
	}

	private void CheckIsAssignable(){
		if(_globalPlayerStats.PlayerUpgradePoints>0){
			isAssignable=true;
			ShowAssignBtn();
			_assignConfirmedBtn.Show();
			_upgradePtsLabel.Show();
			_currUpgradePtsLabel.Show();
			_currUpgradePtsLabel.Text=_currUpgradePoints.ToString();
		}
		else{
			isAssignable=false;
			HideAssignBtn();
			_assignConfirmedBtn.Hide();
			_upgradePtsLabel.Hide();
			_currUpgradePtsLabel.Hide();
		}
	}

	private void HideAssignBtn(){
		_strIncreaseBtn.Hide();
		_strDecreaseBtn.Hide();

		_vitIncreaseBtn.Hide();
		_vitDecreaseBtn.Hide();

		_intIncreaseBtn.Hide();
		_intDecreaseBtn.Hide();

		_dexIncreaseBtn.Hide();
		_dexDecreaseBtn.Hide();

		_lukIncreaseBtn.Hide();
		_lukDecreaseBtn.Hide();
	}
	private void ShowAssignBtn(){
		_strIncreaseBtn.Show();
		_strDecreaseBtn.Show();

		_vitIncreaseBtn.Show();
		_vitDecreaseBtn.Show();

		_intIncreaseBtn.Show();
		_intDecreaseBtn.Show();

		_dexIncreaseBtn.Show();
		_dexDecreaseBtn.Show();

		_lukIncreaseBtn.Show();
		_lukDecreaseBtn.Show();
	}


}

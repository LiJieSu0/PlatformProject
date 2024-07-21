using Godot;
using System;

public partial class UI_TabMenu : Control
{
	[Export] private Label CurrLvLabel;
	[Export] private Label CurrStrLabel;
	[Export] private Label CurrVitLabel;
	[Export] private Label CurrIntLabel;
	[Export] private Label CurrDexLabel;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}

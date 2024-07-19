using Godot;
using System;

public partial class UIFrameWithCloseBtn : Control
{
	[Export ]string MenuTitle;
	[Signal]
    public delegate void CloseButtonPressedEventHandler();
	public override void _Ready()
	{
		GetNode<Label>("Label").Text=MenuTitle;
	}

	public override void _Process(double delta)
	{
		
	}
    private void OnCloseBtnPressed(){
        EmitSignal(SignalName.CloseButtonPressed);
    }
}

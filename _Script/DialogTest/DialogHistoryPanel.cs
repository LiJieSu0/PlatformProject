using Godot;
using System;

public partial class DialogHistoryPanel : Panel
{
	private VBoxContainer _dialogHistoryContainer;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void InitializeNode(){
		_dialogHistoryContainer=GetNode<VBoxContainer>("ScrollContainer/DialogHistoryContainer");
	}
	public void AddHistoryDialog(string dialog,string charIconPath){
		var tmpHistoryDialog=GD.Load<HistoryDialog>("res://_Scene/DialogTest/HistoryDialog.tscn");
		tmpHistoryDialog.ReciveDialog(dialog);
		tmpHistoryDialog.ReciveCharacterIcon(charIconPath);
	}
	public void ClearHistoryDialog(){

	}


}

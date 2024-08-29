using Godot;
using System;

public partial class DialogHistoryPanel : Panel{

	private VBoxContainer _dialogHistoryContainer;	public override void _Ready(){
		InitializeNode();
		GlobalEventPublisher.Instance.DialogHistoryEvent+=AddHistoryDialog;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void InitializeNode(){
		_dialogHistoryContainer=GetNode<VBoxContainer>("ScrollContainer/DialogHistoryContainer");
	}
	public void AddHistoryDialog(string charIconPath,string dialog){
		var packedScene =GD.Load<PackedScene>("res://_Scene/DialogTest/HistoryDialog.tscn");
		var tmpHistoryDialog = packedScene.Instantiate<HistoryDialog>();
		tmpHistoryDialog.ReciveCharacterIcon(charIconPath);
		tmpHistoryDialog.ReciveDialog(dialog);
		_dialogHistoryContainer.AddChild(tmpHistoryDialog);
	}
	public void ClearHistoryDialog(){

	}
	

}

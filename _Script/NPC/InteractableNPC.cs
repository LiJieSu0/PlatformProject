using Godot;
using System;

public partial class InteractableNPC : Area2D,INPC
{
	#region Node
	Control _interactableMark;
	Sprite2D _dialogBallon;
	Label _dialogContentLabel;
	#endregion

	#region Variables
	private bool isPlayerNear=false;
	private int currDailogIdx;
	string[] lines={"first line","Second line"};
	#endregion
	public override void _Ready()
	{
		InitializeNode();
		InitializeSignal();

	}

	public override void _Process(double delta){	

	}
	private void OnAreaEnter(Area2D area){
		isPlayerNear=true;
		_interactableMark.Show();
	}
	private void OnAreaExit(Area2D area){
		isPlayerNear=false;
		_interactableMark.Hide();
	}

    public void InteractReaction(){
		string tmpKey="";
		GlobalDialogManager.Instance.StartDailogueTrigger(tmpKey);
		_dialogBallon.Show();
		StartTalking(tmpKey);
    }

    private void StartTalking(string key){
		currDailogIdx=0;
		_dialogContentLabel.Text=lines[currDailogIdx];
    }

	private void OnNextDialogue(){
		currDailogIdx++;
		if(currDailogIdx>=lines.Length){
			GlobalDialogManager.Instance.EndDialogueTrigger("");
			_dialogBallon.Hide();
			return;
		}
		_dialogContentLabel.Text=lines[currDailogIdx];
    }

    private void InitializeNode(){
		_interactableMark=GetNode<Control>("InteractableMark");
		_dialogBallon=GetNode<Sprite2D>("DialogBallon");
		_dialogContentLabel=GetNode<Label>("DialogBallon/DialogContentLabel");
		
	}
	private void InitializeSignal(){
		this.AreaEntered+=OnAreaEnter;
		this.AreaExited+=OnAreaExit;
		GlobalDialogManager.Instance.NextDialogueEvent+=OnNextDialogue;
	}


}

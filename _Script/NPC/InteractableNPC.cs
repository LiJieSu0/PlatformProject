using Godot;
using System;

public partial class InteractableNPC : Area2D,INPC
{
	#region Node
	Control _interactableMark;
	Control _optionsBtnContainer;
	Sprite2D _dialogBallon;
	Label _dialogContentLabel;
	#endregion

	#region Variables
	private bool isPlayerNear=false;
	private bool isWaitingForOptions=false;
	private bool isOptionShowing=false;
	private int currDailogIdx;
	string[] lines={"first line","Second line","Third line"};

	#endregion
	public override void _Ready(){
		InitializeNode();
		InitializeSignal();

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
		StartDialogue(tmpKey);
    }

    private void StartDialogue(string key){
		currDailogIdx=0;
		_dialogContentLabel.Text=lines[currDailogIdx];
    }

	private void OnNextDialogue(){
		if(isWaitingForOptions)
			return;
		currDailogIdx++;
		if(currDailogIdx>=lines.Length){
			GlobalDialogManager.Instance.EndDialogueTrigger("");
			_dialogBallon.Hide();
			return;
		}
		_dialogContentLabel.Text=lines[currDailogIdx];
		if(isOptionShowing||true){
			isWaitingForOptions=true;
			ShowingOptions(new string[]{"Fuck off","Come here"});
		}
    }

    private void ShowingOptions(string[] options){
		int optIdx=0;
		foreach(string option in options){
			Button tmp=new Button();
			tmp.Text=option;
			tmp.Pressed+=()=>{
				isWaitingForOptions=false;
				GD.Print("Options selected "+option);
				GlobalDialogManager.Instance.SelectOptionTrigger(optIdx);
				FreeAllOptions();
				OnNextDialogue();
			};
			_optionsBtnContainer.AddChild(tmp);
			optIdx++;
		}

    }


    private void InitializeNode(){
		_interactableMark=GetNode<Control>("InteractableMark");
		_dialogBallon=GetNode<Sprite2D>("DialogBallon");
		_dialogContentLabel=GetNode<Label>("DialogBallon/DialogContentLabel");
		_optionsBtnContainer=GetNode<VBoxContainer>("DialogBallon/OptionsBtnContainer");
	}
	private void InitializeSignal(){
		this.AreaEntered+=OnAreaEnter;
		this.AreaExited+=OnAreaExit;
		GlobalDialogManager.Instance.NextDialogueEvent+=OnNextDialogue;
	}

	private void FreeAllOptions(){
		foreach(Node child in _optionsBtnContainer.GetChildren()){
			child.QueueFree();
		}
	}

}

using Godot;
using Godot.Collections;
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
	DialogLoader _dialogLoader;
	private bool isPlayerNear=false;
	private bool isWaitingForOptions=false;
	private bool isOptionShowing=false;
	private int currDailogIdx=0;
	string[] _lines; 
	Dictionary<string, string[]> dialogueOptions;

	#endregion
	public override void _Ready(){
		InitializeNode();
		InitializeSignal();
		dialogueOptions = new Dictionary<string, string[]> //TODO load scripts through json
        {
            { "greeting", new string[] { "Hello!", "Hi there!", "Good day!" } },
            { "farewell", new string[] { "Goodbye!", "See you later!", "Take care!" } },
            { "thanks", new string[] { "Thank you!", "Much appreciated!", "Thanks a lot!" } }
        };

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
		string tmpKey="H1";
		CreateDialogLoader();
		GlobalDialogManager.Instance.StartDailogueTrigger(tmpKey); //TODO check is therea any other listen funcs
		_dialogBallon.Show();
		StartDialogue(tmpKey);
    }

    private void StartDialogue(string key){
		_dialogLoader.SetLinesKey(key);
		_lines=_dialogLoader.GetLines(); //TODO handle key error
		if(_lines.Length==0){
			GD.Print("No lines for this key");
			return;
		}
		currDailogIdx=0;
		_dialogContentLabel.Text=_lines[currDailogIdx];
		GD.Print("start new Dialog "+_dialogLoader.currKey) ;
    }

	private void OnNextDialogue(){ //TODO refactor change to options
		if(isWaitingForOptions)
			return;
		currDailogIdx++;
		//TODO refactor the conditional
		GD.Print(" curr IDX "+currDailogIdx);
		GD.Print("options "+_dialogLoader.GetOptions().Length);
		if(currDailogIdx>=_lines.Length&&_dialogLoader.GetOptions().Length==0){ //The last line and no options
			GlobalDialogManager.Instance.EndDialogueTrigger(""); //Dialogue ends here
			_dialogBallon.Hide();
			return;
		}
		_dialogContentLabel.Text=_lines[currDailogIdx];
		if(currDailogIdx==_lines.Length-1&&_dialogLoader.GetOptions().Length!=0){
			isWaitingForOptions=true;
			ShowingOptions(_dialogLoader.GetOptions());
		}
    }

    private void ShowingOptions(string[] options){
		int optIdx=0;
		foreach(string option in options){
			Button tmp=new Button();
			tmp.Text=option;
			int tmpIdx=optIdx;
			tmp.Pressed+=()=>{
				isWaitingForOptions=false;
				int selectedIdx=tmpIdx;
				GlobalDialogManager.Instance.SelectOptionTrigger(selectedIdx.ToString());
				StartDialogue(_dialogLoader.GetResults()[selectedIdx]); //TODO start a new dialogue stream through options
				FreeAllOptions();
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

	private void CreateDialogLoader(){
		_dialogLoader=new DialogLoader();
		_dialogLoader.LoadAllDialog();
	}

}

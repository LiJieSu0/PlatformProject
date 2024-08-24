// using Godot;
// using Godot.Collections;
// using System;
// using System.Collections;
// using System.Threading.Tasks;

// public partial class InteractableNPC : Area2D,INPC
// {
// 	[Export] string[] SpecialOptions;
// 	[Export] string[] SpecialResults;

// 	#region Node
// 	Control _interactableMark;
// 	Control _optionsBtnContainer;
// 	Sprite2D _dialogBallon;
// 	Label _charNameLabel;
// 	Label _dialogContentLabel;
// 	#endregion

// 	#region Variables
// 	DialogLoader _dialogLoader;
// 	private bool isPlayerNear=false;
// 	private bool isWaitingForOptions=false;
// 	private bool isOptionShowing=false;
// 	private int currDailogIdx=0;
// 	Array<string> _lines;
// 	Array<string> _charNames; 
// 	Dictionary<string, string[]> dialogueOptions;

// 	#endregion
	

// 	public override void _Ready(){
// 		InitializeNode();
// 		InitializeSignal();
// 		dialogueOptions = new Dictionary<string, string[]> //TODO load scripts through json
//         {
//             { "greeting", new string[] { "Hello!", "Hi there!", "Good day!" } },
//             { "farewell", new string[] { "Goodbye!", "See you later!", "Take care!" } },
//             { "thanks", new string[] { "Thank you!", "Much appreciated!", "Thanks a lot!" } }
//         };

// 	}

// 	private void OnAreaEnter(Area2D area){
// 		isPlayerNear=true;
// 		_interactableMark.Show();
// 	}
// 	private void OnAreaExit(Area2D area){
// 		isPlayerNear=false;
// 		_interactableMark.Hide();
// 	}

//     public void InteractReaction(){
// 		string tmpKey="H1";
// 		_dialogLoader=new DialogLoader(tmpKey);
// 		GlobalDialogManager.Instance.StartDailogueTrigger(tmpKey); //TODO check is therea any other listen funcs
// 		_dialogBallon.Show();
// 		StartDialogue(tmpKey);
//     }

//     private void StartDialogue(string key){
// 		_dialogLoader.SetLinesKey(key);
// 		_charNames=new Array<string>(_dialogLoader.GetCharNames());
// 		_lines=new Array<string>(_dialogLoader.GetLines()); //TODO handle key error
// 		if(_lines.Count==0){
// 			GD.Print("No lines for this key");
// 			return;
// 		}
// 		currDailogIdx=0;
// 		_charNameLabel.Text=_charNames[currDailogIdx];
// 		_dialogContentLabel.Text=_lines[currDailogIdx];
//     }

// 	private void OnNextDialogue(){
// 		if(isWaitingForOptions)
// 			return;
// 		currDailogIdx++;
// 		//TODO refactor the conditional
// 		if(currDailogIdx>=_lines.Count&&_dialogLoader.GetOptions().Length==0){ //The last line and no options
// 			GlobalDialogManager.Instance.EndDialogueTrigger(""); //Dialogue ends here
// 			_dialogBallon.Hide();
// 			return;
// 		}
// 		_charNameLabel.Text=_charNames[currDailogIdx];
// 		_dialogContentLabel.Text=_lines[currDailogIdx];
// 		if(currDailogIdx==_lines.Count-1&&_dialogLoader.GetOptions().Length!=0){
// 			isWaitingForOptions=true;
// 			Array<string> options=new Array<string>(_dialogLoader.GetOptions());
// 			Array<string> results=new Array<string>(_dialogLoader.GetResults());
// 			ShowingOptions(options,results);
// 		}
//     }

//     private void ShowingOptions(Array<string> options,Array<string> results){
// 		//TODO add special options and results here
// 		// CreateSpecialOptions("special order",ShowShop);
// 		options.Add("Show shop");
// 		int optIdx=0;
// 		foreach(string option in options){
// 			Button tmp=new Button();
// 			tmp.Text=option;
// 			int tmpIdx=optIdx;
// 			tmp.Pressed+=()=>{ //TODO refactor
// 				if(option=="Show shop"){
// 					ShowShop();
// 					return;
// 				}
// 				isWaitingForOptions=false;
// 				int selectedIdx=tmpIdx;
// 				GlobalDialogManager.Instance.SelectOptionTrigger(selectedIdx.ToString());
// 				StartDialogue(results[selectedIdx]);
// 				FreeAllOptions();
// 			};
// 			_optionsBtnContainer.AddChild(tmp);
// 			optIdx++;
// 		}
//     }

//     private void InitializeNode(){
// 		_interactableMark=GetNode<Control>("InteractableMark");
// 		_dialogBallon=GetNode<Sprite2D>("DialogBallon");
// 		_charNameLabel=GetNode<Label>("DialogBallon/CharNameLabel");
// 		_dialogContentLabel=GetNode<Label>("DialogBallon/DialogContentLabel");
// 		_optionsBtnContainer=GetNode<VBoxContainer>("DialogBallon/OptionsBtnContainer");
// 	}
// 	private void InitializeSignal(){
// 		this.AreaEntered+=OnAreaEnter;
// 		this.AreaExited+=OnAreaExit;
// 		GlobalDialogManager.Instance.NextDialogueEvent+=OnNextDialogue;
// 	}

// 	private void FreeAllOptions(){
// 		foreach(Node child in _optionsBtnContainer.GetChildren()){
// 			child.QueueFree();
// 		}
// 	}



// 	private void CreateSpecialOptions(string option,Action results){
// 		GD.Print("some special options "+option);
// 		results();
// 	}

// 	private void ShowShop(){
// 		GD.Print("Shop showing");
// 		//TODO create shop interface
// 		GlobalEventPublisher.Instance.ShowTradingMenuTrigger();
// 		GlobalDialogManager.Instance.EndDialogueTrigger("");
// 		_dialogBallon.Hide();
		
// 	}
// }

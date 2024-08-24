using Godot;
using Godot.Collections;

public enum DialogState{
		WaitForStart,
		SentenceShowing,
		WaitForNextSentence,
		WaitForSelect,
		SelectFinsihed,
		DialogEnd,
		AnimationPlaying,
}
public partial class DialogTest : CanvasLayer{

	[Export] ShaderMaterial NotSpeakingShader;
	private Dictionary<string,CharSprite> _charSpriteDict=new Dictionary<string,CharSprite>(); //TODO load this dynamically
	public DialogState _currDialogState;
	public DialogLoader _dialogLoader;
	public int _currDialogIdx;
	public Array<string> _lines;
	private Array<string> _charNames;
	public Array<Array<string>> _options;
	public Array<Array<string>> _results;
	private Array<string> _finalResults;


	public string _currCharName;
	public bool isSkippable=false;


	#region Node
	private Label _charNameLabel;
	private DialogContentLabel _dialogContentLabel;
	private CharSprite FirstChar;
	private CharSprite SecondChar;
	private Control _optionContainer;
	#endregion
	
	public override void _Ready(){
		_dialogLoader=new DialogLoader();
		_currDialogIdx=0;
		InitializeNode();
		SetLinesWithKey("B1");
		_currDialogState=DialogState.WaitForStart;
		StartDialog(_currDialogIdx);
		_dialogLoader.GetFinalResults();
		var tmp=new Variant[3]{"LeftPoint","RightPoint",false};
		FirstChar.LeftMoveToScene(tmp);
	}

    public override void _Process(double delta){

    }

	private void StartDialog(int idx){
		GD.Print(idx);
		_charNameLabel.Text=_charNames[idx];
		if(_charNames[idx]!=_currCharName){
			ChangeCharSpeaking(_charNames[idx]);
		}
		if(_lines[idx].StartsWith("@")){
			string funcName=_lines[idx].Substring(1);
			_charSpriteDict[_charNames[idx]].Call(funcName);
		}else{
			_dialogContentLabel.ReceiveLine(_lines[idx]);
		}
		//If it is the last sentence, option buttons are handled by DialogContentLabel
	}



    public void ShowOptions(){
		Array<string> currOptions=_options[_currDialogIdx];
		Array<string> currResults=_results[_currDialogIdx];

		for(int i=0;i<currOptions.Count;i++){
			AddOptionBtn(i,currOptions[i],currResults[i]);
		}
		_optionContainer.Show();
		_currDialogState=DialogState.WaitForSelect;
	}

	private void AddOptionBtn(int i,string option,string result){
			Button tmpBtn=new Button();
			tmpBtn.Text=option;
			int tmpIdx=i;
			tmpBtn.Pressed+=()=>{
				_currDialogState=DialogState.SelectFinsihed;
				GD.Print("option selected "+result);
				foreach(Node node in _optionContainer.GetChildren()){
					node.QueueFree();
				}
				_optionContainer.Hide();
				SetLinesWithKey(result);
				_currDialogIdx=0;
				StartDialog(_currDialogIdx);
			};
			_optionContainer.AddChild(tmpBtn);
	}
    public override void _UnhandledInput(InputEvent @event){
		if(@event is InputEventMouseButton mouseButton){
			if(mouseButton.ButtonIndex==MouseButton.Left&&mouseButton.IsReleased()&&_currDialogState==DialogState.WaitForNextSentence){
				_currDialogIdx++;
				StartDialog(_currDialogIdx);
			}
			if(mouseButton.ButtonIndex==MouseButton.Left&&mouseButton.IsReleased()&&_currDialogState==DialogState.SentenceShowing&&isSkippable){
				_dialogContentLabel.ClickToFinish();
			}
		}

    }

	private void SetLinesWithKey(string key){
		if(key!="")
			_dialogLoader.SetLinesKey(key);
		_charNames=_dialogLoader.GetCharNames();
		_lines=_dialogLoader.GetLines();
		_options=_dialogLoader.GetOptions();
		_results=_dialogLoader.GetResults();
	}

    private void ChangeCharSpeaking(string speakingCharName){
		_currCharName=speakingCharName;
		foreach(var (name,sprite) in _charSpriteDict){
			if(name==speakingCharName){
				sprite.Material=null;
			}
			else{
				sprite.Material=NotSpeakingShader;
			}
		}

    }
    private void InitializeNode(){
		FirstChar=GetNode<CharSprite>("CharManager/Sprite2D");
		SecondChar=GetNode<CharSprite>("CharManager/Sprite2D2");
		_charNameLabel=GetNode<Label>("Panel/CharNameLabel");
		_dialogContentLabel=GetNode<DialogContentLabel>("Panel/DialogContentLabel");
		_optionContainer=GetNode<Control>("OptionContainer");
		_charSpriteDict["無月"]=FirstChar;
		_charSpriteDict["遙香"]=SecondChar;
	}



}

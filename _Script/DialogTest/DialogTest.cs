using System.Threading.Tasks;
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
public partial class DialogTest : Node{

	[Export] ShaderMaterial NotSpeakingShader;
	[Export] float AUTO_PLAY_INTERVAL_TIME=0.5f;

	#region Variables
	private Dictionary<string,CharSprite> _charSpriteDict=new Dictionary<string,CharSprite>(); //TODO load this dynamically
	public DialogState _currDialogState;
	public DialogLoader _dialogLoader;
	public bool isAutoOn=false;
	public bool isSkippable=false;
	public int _currDialogIdx;
	public Array<string> _lines;
	private Array<string> _charNames;
	public Array<Array<string>> _options;
	public Array<Array<string>> _results;
	private Array<string> _finalResults;
	public string _currCharName;
	#endregion

	#region Node
	private Label _charNameLabel;
	private Button _autoToggleBtn;
	private DialogContentLabel _dialogContentLabel;
	private CharSprite FirstChar;
	private CharSprite SecondChar;
	private Control _optionContainer;
	public DialogTestCamera _camera2D;
    private Button _historyBtn;
    private Panel _dialogHistoryPanel;
    private ColorRect _curtainColorRect;
    private AudioStreamPlayer _bgmPlayer;
    #endregion




    public override void _Ready(){
		_dialogLoader=new DialogLoader();
		_currDialogIdx=0;
		InitializeNode();
		_autoToggleBtn.Pressed+=()=>{
			isAutoOn=!isAutoOn;
			if(_currDialogState==DialogState.WaitForNextSentence){
				AutoPlaying();
			}
		};
		_historyBtn.Pressed+=()=>{
			_dialogHistoryPanel.Visible=!_dialogHistoryPanel.Visible;
			if(_dialogHistoryPanel.Visible){
				isAutoOn=false;
			}
		};
		SetLinesWithKey("B1");
		_currDialogState=DialogState.WaitForStart;
		StartDialog(_currDialogIdx);
		_dialogLoader.GetFinalResults();
		var tmp=new Variant[3]{"LeftPoint","RightPoint",false};
		// FirstChar.FallDown();



	}

	public override void _Process(double delta){
	}

	public void StartDialog(int idx){
		GD.Print(idx);
		_charNameLabel.Text=_charNames[idx];
		if(_charNames[idx]!=_currCharName){
			ChangeCharSpeaking(_charNames[idx]);
		}
		if(_lines[idx].StartsWith("@")){
			string funcName=_lines[idx].Substring(1);
			// _currDialogIdx++;
			// StartDialog(_currDialogIdx);
			if(_options[idx].Count==0){
				_charSpriteDict[_charNames[idx]].Call(funcName); //Call the current sprite do animation without variables 
			}
			else{
				_charSpriteDict[_charNames[idx]].Call(funcName,_options[idx]); //Call the current sprite do animation with variables 
				//TODO convert variables in the function
			}
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
			if(mouseButton.ButtonIndex==MouseButton.Left&&
			mouseButton.IsReleased()&&
			_currDialogState==DialogState.WaitForNextSentence&&
			!isAutoOn&&
			!_dialogHistoryPanel.Visible){
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


	public void AutoPlaying(){
		Timer timer=new Timer();
		this.AddChild(timer);
		timer.WaitTime=AUTO_PLAY_INTERVAL_TIME;
		timer.Timeout+=()=>{
			_currDialogIdx++;
			StartDialog(_currDialogIdx);
			timer.QueueFree();
		};
		timer.Start();
	}


	private void InitializeNode(){
		FirstChar=GetNode<CharSprite>("CharManager/Sprite2D");
		SecondChar=GetNode<CharSprite>("CharManager/Sprite2D2");
		_charNameLabel=GetNode<Label>("DialogPanel/CharNameLabel");
		_dialogContentLabel=GetNode<DialogContentLabel>("DialogPanel/DialogContentLabel");
		_optionContainer=GetNode<Control>("DialogPanel/OptionContainer");
		_autoToggleBtn=GetNode<Button>("MenuBtns/AutoToggleBtn");
		_historyBtn=GetNode<Button>("MenuBtns/HistoryBtn");
		_camera2D=GetNode<DialogTestCamera>("Camera2D");
		_dialogHistoryPanel=GetNode<Panel>("DialogHistoryPanel");
		_curtainColorRect=GetNode<ColorRect>("Camera2D/CurtainColorRect");
		_charSpriteDict["無月"]=FirstChar;
		_charSpriteDict["遙香"]=SecondChar;
		_bgmPlayer=GetNode<AudioStreamPlayer>("BGMPlayer");
	}

	private void EaseInScene(){
		Color endColor=new Color(Colors.Black,0);
		Tween tween=CreateTween();
		tween.TweenProperty(_curtainColorRect,"color",endColor,1);
		tween.Finished+=()=>{
			tween.Kill();
		};
	}
	private void EaseOutScene(){
		Color endColor=new Color(Colors.Black,1);
		Tween tween=CreateTween();
		tween.TweenProperty(_curtainColorRect,"color",endColor,1);
		tween.Finished+=()=>{
			tween.Kill();
		};
	}

}

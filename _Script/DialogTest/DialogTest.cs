using Godot;
using Godot.Collections;
using System;
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

	public DialogState _currDialogState;
	public DialogLoader _dialogLoader;
	public int _currDialogIdx;
	public Array<string> _lines;
	private Array<string> _charNames;
	private string _currCharName;
	public bool isSkippable=false;


	#region Node
	private Label _charNameLabel;
	private DialogContentLabel _dialogContentLabel;
	private Sprite2D FirstChar;
	private Sprite2D SecondChar;
	private Control _optionContainer;
	#endregion
	
	public override void _Ready(){
		_dialogLoader=new DialogLoader("B1");
		_currDialogIdx=0;
		InitializeNode();
		SetLinesWithKey();
		_currDialogState=DialogState.WaitForStart;
		GD.Print(_charNames.ToString());
		StartDialog(_currDialogIdx);
	}

    public override void _Process(double delta){
		
    }

	private void StartDialog(int idx){
		_charNameLabel.Text=_charNames[idx];
		_dialogContentLabel.ReceiveLine(_lines[idx]);
		//If it is the last sentence, option buttons are handled by DialogContentLabel
	}

	public void ShowOptions(){
		GD.Print("Showing options");
		Array<string> options=new Array<string>(_dialogLoader.GetOptions()); //TODO test array is null or not
		Array<string> results=new Array<string>(_dialogLoader.GetResults());
		for(int i=0;i<options.Count;i++){
			AddOptionBtn(i,options[i],results[i]);
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

	private void SetLinesWithKey(string key=""){
		if(_dialogLoader.currKey==""||_dialogLoader.currKey==null){ //check is there any currkey when class created.
			GD.Print("There is no scene key to load");
			return;
		}
		if(key!="")
			_dialogLoader.currKey=key;
		_charNames=new Array<string>(_dialogLoader.GetCharNames());
		_lines=new Array<string>(_dialogLoader.GetLines());
	}


    private void InitializeNode(){
		FirstChar=GetNode<Sprite2D>("Sprite2D");
		SecondChar=GetNode<Sprite2D>("Sprite2D2");
		_charNameLabel=GetNode<Label>("Panel/CharNameLabel");
		_dialogContentLabel=GetNode<DialogContentLabel>("Panel/DialogContentLabel");
		_optionContainer=GetNode<Control>("OptionContainer");
	}

}

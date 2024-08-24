using Godot;
using System;

public partial class DialogContentLabel : Label
{
	[Export] float WORD_DELAY_TIME=0.05f;
	[Export] float SKIPPABLE_TIME=0.5f;
	DialogTest _dialogTest;
	Tween tween;

	string _currLine;
	public override void _Ready(){
		_dialogTest=GetParent().GetParent<DialogTest>();
	}

	public override void _Process(double delta){
	}

	public void ReceiveLine(string line){
		_currLine=line;
		this.Text=_currLine;
		tween=CreateTween();
		this.VisibleRatio=0;
		_dialogTest.isSkippable=false;
		_dialogTest._currDialogState=DialogState.SentenceShowing;
		CreateSkippableTimer();
		tween.TweenProperty(this,"visible_ratio",1,line.Length*WORD_DELAY_TIME);
		tween.Finished+=OnTweenFinished;

	}

	public void ClickToFinish(){
		if(tween!=null){
			tween.Pause();
			tween.Kill();
		}
		tween=CreateTween();
		tween.TweenProperty(this,"visible_ratio",1,0.1);
		tween.Finished+=OnTweenFinished;
	}
	
	private void OnTweenFinished(){
		_dialogTest._currDialogState=DialogState.WaitForNextSentence;
		OptionsCheck();
		LastLineCheck();
		tween.Kill();
	}

    private void OptionsCheck(){
		if(_dialogTest._currDialogState==DialogState.WaitForNextSentence&&_dialogTest._options[_dialogTest._currDialogIdx].Count!=0){
			_dialogTest._currDialogState=DialogState.WaitForSelect;
			_dialogTest.ShowOptions();
		}
	}
    private void LastLineCheck(){
		if(_dialogTest._currDialogIdx==_dialogTest._lines.Count-1){
			_dialogTest._currDialogState=DialogState.DialogEnd;
			GD.Print("End dialog here");
		}
    }
	private void CreateSkippableTimer(){
		Timer timer=new Timer();
		this.AddChild(timer);
		timer.WaitTime=SKIPPABLE_TIME;
		timer.Timeout+=()=>{
			_dialogTest.isSkippable=true;
			timer.QueueFree();
		};
		timer.Start();
	}

}

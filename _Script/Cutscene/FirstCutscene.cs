using Godot;
using System;
using DialogueManagerRuntime;
public partial class FirstCutscene : Node2D
{
	AnimationPlayer animationPlayer;
	private double _currAnimationPos=0;
	private string _currAnimation="";
	[Export]public string[] dialogueTitleArr;
	[Export] public Resource dialogueRes;
	private int _currDialogueIdx=0;
	public override void _Ready(){
		animationPlayer=GetNode<AnimationPlayer>("AnimationPlayer");
	}	

	public override void _Process(double delta)
	{
		
	}

	public void Dosomething(){
		GD.Print("Do something here");
	}

	public void StartNextDialogue(){
		_currAnimation=animationPlayer.CurrentAnimation;
		_currAnimationPos=animationPlayer.CurrentAnimationPosition+0.1;
		animationPlayer.Pause();
		DialogueManager.ShowExampleDialogueBalloon(dialogueRes,dialogueTitleArr[_currDialogueIdx]);
	}
	public void StartAnimation(){
		_currDialogueIdx+=1;
		animationPlayer.Play(_currAnimation);
		animationPlayer.Seek(_currAnimationPos);
	}
}

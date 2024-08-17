using Godot;
using System;

public partial class GlobalDialogManager : Node
{
	public static GlobalDialogManager Instance { get; private set; }
	public delegate void StartDailogue(string key);
	public event StartDailogue StartDailogueEvent;
	public delegate void NextDialogue();
	public event NextDialogue NextDialogueEvent;
	public delegate void EndDialogue(string key);
	public event EndDialogue EndDialogueEvent;


	public override void _Ready(){
		Instance=this;
	}
    public void StartDailogueTrigger(string key){
        StartDailogueEvent?.Invoke(key);
    }

	public void NextDialogueTrigger(){
		NextDialogueEvent?.Invoke();
	}

	public void EndDialogueTrigger(string key){
		EndDialogueEvent?.Invoke(key);
	}

}

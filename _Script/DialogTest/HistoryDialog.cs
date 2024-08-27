using Godot;
using System;

public partial class HistoryDialog : NinePatchRect
{
	private Label _historyDialogLabel;
	private Sprite2D _charIconSprite;
	public override void _Ready()
	{
		InitializeNode();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void ReciveDialog(string dialog){
		_historyDialogLabel.Text=dialog;
		
    }

    public void ReciveCharacterIcon(string charIconPath){
		_charIconSprite.Texture=GD.Load<Texture2D>(charIconPath);
		_charIconSprite.Scale=UtilsFunc.DesiredSpriteScale(_charIconSprite.Texture.GetSize(),new Vector2(100,100));
    }	

	private void InitializeNode(){
		_historyDialogLabel=GetNode<Label>("HistoryDialogLabel");
		_charIconSprite=GetNode<Sprite2D>("CharIconSprite");
	}
}

using Godot;

public partial class CharSprite : Sprite2D{
	private DialogTest _dialogtest;
	public Node MovingPoints;

	public override void _Ready()
	{
		MovingPoints=GetParent().GetParent().GetNode("MovingPoints");
		_dialogtest=GetParent().GetParent<DialogTest>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Shake(){
		Vector2 leftPos=new Vector2(this.Position.X-50,this.Position.Y);
		Vector2 rightPos=new Vector2(this.Position.X+50,this.Position.Y);
		Vector2 originalPos=this.Position;
		Tween tween=CreateTween();
		tween.SetLoops(3);
		_dialogtest._currDialogState=DialogState.AnimationPlaying;
		tween.TweenProperty(this,"position",leftPos,0.1);
		tween.TweenProperty(this,"position",rightPos,0.1);
		tween.Finished+=()=>{
            ResetCharPosition(originalPos, tween);
        };
	}



    public void Jump(){
		Vector2 jumpPos=new Vector2(this.Position.X,this.Position.Y-20);
		Vector2 originalPos=this.Position;
		Tween tween=CreateTween();
		_dialogtest._currDialogState=DialogState.AnimationPlaying;
		tween.SetLoops(2);
		tween.TweenProperty(this,"position",jumpPos,0.2);
		tween.TweenProperty(this,"position",originalPos,0.2);
		tween.Finished+=()=>{
            ResetCharPosition(originalPos, tween);
		};
	}
    private void ResetCharPosition(Vector2 originalPos, Tween tween){
        this.Position = originalPos;
        _dialogtest._currDialogState = DialogState.WaitForNextSentence;
        tween.Kill();
    }
	public void LeftMoveToScene(params Variant[] variables){
		string startPointName=(string)variables[0];
		string endPointName=(string)variables[1];
		bool backToOriginalPos=(bool)variables[2];
		Vector2 orignalPos=this.Position;
		Vector2 startPos=MovingPoints.GetNode<Node2D>(startPointName).Position;
		Vector2 endPos=MovingPoints.GetNode<Node2D>(endPointName).Position;
		Tween tween=CreateTween();
		this.Position=startPos;
		tween.TweenProperty(this,"position",endPos,2);
		tween.Finished+=()=>{
			GD.Print("Moving finished");
			tween.Kill();
		};
	}
}

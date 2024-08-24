using Godot;

public partial class CharSprite : Sprite2D
{
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Shake(){
		Vector2 leftPos=new Vector2(this.Position.X-50,this.Position.Y);
		Vector2 rightPos=new Vector2(this.Position.X+50,this.Position.Y);
		Vector2 OriginalPos=this.Position;
		Tween tween=CreateTween();
		tween.SetLoops(3);
		tween.TweenProperty(this,"position",leftPos,0.1);
		tween.TweenProperty(this,"position",rightPos,0.1);
		tween.Finished+=()=>{
			GD.Print("Shake finished");
			this.Position=OriginalPos;
			tween.Kill();
		};
	}

	public void Jump(){
		Vector2 jumpPos=new Vector2(this.Position.X,this.Position.Y-20);
		Vector2 OriginalPos=this.Position;
		Tween tween=CreateTween();
		tween.SetLoops(2);
		tween.TweenProperty(this,"position",jumpPos,0.2);
		tween.TweenProperty(this,"position",OriginalPos,0.2);
		tween.Finished+=()=>{
			GD.Print("Shake finished");
			this.Position=OriginalPos;
			tween.Kill();
		};
	}

}

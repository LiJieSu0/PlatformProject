using Godot;
using System;

public partial class DialogTestCamera : Camera2D
{
	[Export] float ShakeMagnitude=20f;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void ShakeCamera(){
		var originalPos = this.Position;
		Tween tween=CreateTween();
		tween.SetLoops(5);
		tween.TweenProperty(this,"position",new Vector2(this.Position.X+GD.RandRange(-1,1)*ShakeMagnitude,
														this.Position.Y+GD.RandRange(-1,1)*ShakeMagnitude),0.05f);
		tween.TweenProperty(this,"position",new Vector2(this.Position.X+GD.RandRange(-1,1)*ShakeMagnitude,
														this.Position.Y+GD.RandRange(-1,1)*ShakeMagnitude),0.05f);
		tween.TweenProperty(this,"position",new Vector2(this.Position.X+GD.RandRange(-1,1)*ShakeMagnitude,
														this.Position.Y+GD.RandRange(-1,1)*ShakeMagnitude),0.05f);
		tween.TweenProperty(this,"position",new Vector2(this.Position.X+GD.RandRange(-1,1)*ShakeMagnitude,
														this.Position.Y+GD.RandRange(-1,1)*ShakeMagnitude),0.05f);																												
		tween.Finished+=()=>{
			GD.Print("Shake finished");
			this.Position=originalPos;
			tween.Kill();
		};

	}
}

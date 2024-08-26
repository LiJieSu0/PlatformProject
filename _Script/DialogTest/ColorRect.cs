using Godot;
using System;

public partial class ColorRect : Godot.ColorRect
{
	// Called when the node enters the scene tree for the first time.
	Color endColor=new Color(Colors.Black,0);
	public override void _Ready()
	{
		Tween tween=CreateTween();
		tween.TweenProperty(this,"color",endColor,1);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

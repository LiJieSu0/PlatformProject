using Godot;
using System;

public partial class PatrolEnemy : PathFollow2D
{
	private bool isGoleft=true;
	public override void _Ready()
	{
		this.ProgressRatio=0;
	}

	public override void _Process(double delta)
	{
		this.ProgressRatio+=(float)delta*0.01f;
	}
}

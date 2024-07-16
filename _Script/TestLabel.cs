using Godot;
using Godot.Collections;
using System;

public partial class TestLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Dictionary<string, Variant> Save(){
		return new Dictionary<string, Variant>(){
			{"Label",this.Text}
		};
	}
}

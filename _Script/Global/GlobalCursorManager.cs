using Godot;
using System;

public partial class GlobalCursorManager : Node
{

	public static GlobalCursorManager Instance { get; private set; }
	private Texture2D  CrossHairCursor = (Texture2D)GD.Load("res://_Asset/UI/crosshair.png");
	private Texture2D  MenuCursor = (Texture2D)GD.Load("res://_Asset/UI/menuCursor.png");

	
	
	public override void _Ready()
	{
		Instance=this;
		Input.SetCustomMouseCursor(CrossHairCursor,Input.CursorShape.Arrow,new Vector2(16,16));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ChangeCursor(){
		Input.SetCustomMouseCursor(MenuCursor,Input.CursorShape.Arrow);
	}
}

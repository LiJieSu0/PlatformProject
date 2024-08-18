using Godot;
using System;

public partial class UI_ItemInSlot : TextureRect{

	public ItemModel _currItem=null;
	public int _itemAmount=0;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

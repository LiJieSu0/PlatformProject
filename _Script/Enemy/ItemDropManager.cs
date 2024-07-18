using Godot;
using System;
using System.Collections.Generic;

public partial class ItemDropManager : Node2D
{
	[Export] private int MoneyDrop;
	[Export] private int ExpDrop;
	// [Export] public ItemRes[] ItemResArr;
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
	}

	// public void ItemDropInstantiate(){ //TODO write reource to create item loading
	// 	//TODO player receive money and exp
	// 	for(int i = 0;i<ItemResArr.Length;i++){
	// 		string itemPath=ItemResArr[i].PackedItemScenePath;
	// 		PackedScene itemScene = (PackedScene)ResourceLoader.Load(itemPath);
	// 		Node2D item=(Node2D)itemScene.Instantiate();
	// 		GetTree().Root.AddChild(item);
	// 		item.GlobalPosition=this.GlobalPosition;
	// 	}
	// }

}

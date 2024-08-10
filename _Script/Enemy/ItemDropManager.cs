using Godot;
using System;
using System.Collections.Generic;

public partial class ItemDropManager : Node2D{
	[Export] public PackedScene[] ItemResArr; //TODO change to string path
	public int[] _drops;
	public override void _Ready(){
		GlobalEventPublisher.Instance.EnemyDeadEvent+=ItemDropInstantiate;
	}

    public void ItemDropInstantiate(string enemyName){ 
		//TODO load drop item list through sheet
		GD.Print("Enemy name drop :"+enemyName);
		for(int i = 0;i<ItemResArr.Length;i++){
			// string itemPath=ItemResArr[i].PackedItemScenePath;
			// PackedScene itemScene = (PackedScene)ResourceLoader.Load(itemPath);
			PackedScene itemScene=ItemResArr[i];
			Node2D item=(Node2D)itemScene.Instantiate();
			GetTree().CurrentScene.CallDeferred("add_child",item);
			item.GlobalPosition=this.GlobalPosition;
		}
	}

}

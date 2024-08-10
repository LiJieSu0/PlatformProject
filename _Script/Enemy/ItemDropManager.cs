using Godot;
using System;
using System.Collections.Generic;

public partial class ItemDropManager : Node2D{

	private const string PACKED_SCENE_PATH="res://_Scene/Item/BasicDropItem.tscn";
	public int[] _drops;
	public override void _Ready(){
		GlobalEventPublisher.Instance.EnemyDeadEvent+=NormalItemDropInstantiate;
		GlobalEventPublisher.Instance.EnemyDeadEvent+=SpecialItemDropInstantiate;

	}

    public void NormalItemDropInstantiate(string enemyName){ 
		//TODO load drop item list through sheet
		for(int i = 0;i<_drops.Length;i++){
			//TODO genereate odds

			PackedScene itemScene = (PackedScene)ResourceLoader.Load(PACKED_SCENE_PATH);
			Node2D item=(Node2D)itemScene.Instantiate();
			GetTree().CurrentScene.GetNode("ItemManager").CallDeferred("add_child",item);
			item.GlobalPosition=this.GlobalPosition;
			BasicDropItem tmp=(BasicDropItem)item;
			tmp.LoadDropItemTexture("texture path");
			var itemResource = ResourceLoader.Load<DropItemRes>("path"); //load from database
			

		}
	}

	public void SpecialItemDropInstantiate(string enemyName){
		return;
		//TODO drop some quest item
	}
}

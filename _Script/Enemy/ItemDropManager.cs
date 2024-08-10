using Godot;
using Godot.Collections;
using System;

public partial class ItemDropManager : Node2D{

	private Dictionary<int,Variant> _itemDb;
	private const string PACKED_SCENE_PATH="res://_Scene/Item/BasicDropItem.tscn";
	public int[] _drops;
	Random random = new Random();
	public override void _Ready(){
		GlobalEventPublisher.Instance.EnemyDeadEvent+=NormalItemDropInstantiate;
		GlobalEventPublisher.Instance.EnemyDeadEvent+=SpecialItemDropInstantiate;
		_itemDb=GlobalDatabaseManager.Instance.ItemListDB;
	}

    public void NormalItemDropInstantiate(string enemyName){ 
		GD.Print("Called");
		for(int i = 0;i<_drops.Length;i++){
			ItemModel currItem=(ItemModel)_itemDb[_drops[i]];
			GD.Print("Item num "+currItem.ItemTexturePath);
			float randomNum= (float)(random.NextDouble()*(1.0-0.01+0.01)); //Drop rate calculate here.
			if(randomNum>currItem.Odds){
				continue;
			}
			PackedScene itemScene = (PackedScene)ResourceLoader.Load(PACKED_SCENE_PATH);
			Node2D item=(Node2D)itemScene.Instantiate();
			GetTree().CurrentScene.GetNode("ItemManager").CallDeferred("add_child",item);
			item.GlobalPosition=this.GlobalPosition;
			BasicDropItem droppedItem=(BasicDropItem)item;
			droppedItem.LoadDropItemTexture(currItem.ItemTexturePath);
		}
	}

	public void SpecialItemDropInstantiate(string enemyName){
		return;
		//TODO drop some quest item
	}
}

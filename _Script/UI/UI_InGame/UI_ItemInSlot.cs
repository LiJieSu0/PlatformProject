using Godot;
using System;

public partial class UI_ItemInSlot : TextureRect{

	public ItemModel _currItem=null;
	public int _itemNo;
	public int _itemAmount=0;
	private Label _itemAmountLabel;
	public override void _Ready(){
		_itemAmountLabel=GetNode<Label>("ItemTextureRect/ItemAmountLabel");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void MoveItemToSlot(UI_ItemInSlot prevSlot){
		//TODO check prev slot
		_currItem=prevSlot._currItem;
		_itemNo=_currItem.ItemNo;
		_itemAmount=prevSlot._itemAmount;
		prevSlot._currItem=null;
		prevSlot._itemAmount=0;
		UpdateAmountLabel();
	}

	public void AddItemToSlot(ItemModel item,int amount=1){
		_currItem=item;
		_itemNo=item.ItemNo;
		_itemAmount+=amount;
		GetChild<TextureRect>(0).Texture=GD.Load<Texture2D>(item.ItemTexturePath);
		UpdateAmountLabel();
	}

	public void UpdateAmountLabel(){
		if(_currItem!=null)
			_itemAmountLabel.Text=_itemAmount.ToString();
	}
	public void ClearAmountLabel(){
		_itemAmountLabel.Text="";
	}

}

using Godot;
using System;

public partial class UI_ItemInSlot : TextureRect{

	public ItemModel _currItem=null;
	public int _itemAmount=0;
	private Label _itemAmountLabel;
	public override void _Ready(){
		_itemAmountLabel=GetNode<Label>("ItemTextureRect/ItemAmountLabel");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void UpdateAmountLabel(){
		if(_currItem!=null)
			_itemAmountLabel.Text=_itemAmount.ToString();
	}
	public void ClearAmountLabel(){
		_itemAmountLabel.Text="";
	}

}

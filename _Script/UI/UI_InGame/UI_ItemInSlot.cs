using Godot;
using System;

public partial class UI_ItemInSlot : TextureRect{

	public ItemModel _currItem=null;
	public int _itemNo;
	public int _itemAmount=0;
	private bool isMouseEntered=false;
	private Label _itemAmountLabel;
	public override void _Ready(){
		_itemAmountLabel=GetNode<Label>("ItemTextureRect/ItemAmountLabel");
		InitializeSignal();
		// GetChild<TextureRect>(0).MouseFilter=MouseFilterEnum.Ignore;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		ShowSlotMenu();
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


    public override string ToString(){
        return $"{_itemNo},{_itemAmount}";
    }

	private void ShowSlotMenu(){
		if(Input.IsActionJustPressed("ui_mouse_right")&&isMouseEntered){
			GD.Print("right click "+this.Name);
			GetNode<Panel>("SecondMenu").Show();
			//TODO add second menu
		}
	}
	public override void _UnhandledInput(InputEvent @event)
{
    if (@event is InputEventMouseButton mouseEvent){
		GD.Print("unhandled");
        if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
        {
            GD.Print("Right-click detected in the unhandled input");
        }
    }
}
	private void InitializeSignal(){
		this.MouseEntered+=OnMouseEntered;
		this.MouseExited+=OnMouseExited;
	}

    private void OnMouseExited(){
		isMouseEntered=false;
    }


    private void OnMouseEntered(){
		isMouseEntered=true;
    }

}

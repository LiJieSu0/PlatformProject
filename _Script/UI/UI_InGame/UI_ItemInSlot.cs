using Godot;
using System;

public partial class UI_ItemInSlot : TextureRect{

	public ItemModel _currItem=null;
	public int _itemNo;
	public int _itemAmount=0;
	private bool isMouseEntered=false;
	private bool isMouseInSecondLevel=false;
	private Label _itemAmountLabel;
	private Panel _secondLevelMenu;
	public override void _Ready(){
		_itemAmountLabel=GetNode<Label>("ItemTextureRect/ItemAmountLabel");
		_secondLevelMenu=GetNode<Panel>("SecondMenu");
		InitializeSignal();
		// GetChild<TextureRect>(0).MouseFilter=MouseFilterEnum.Ignore;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		ShowSlotMenu();
		HideSecondLevelMenu();
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
		if(Input.IsActionJustPressed("ui_mouse_right")&&isMouseEntered&&_currItem!=null){
			GD.Print("right click "+this.Name);
			GetNode<Panel>("SecondMenu").Show();
			//TODO add second menu button
		}
	}

	private void InitializeSignal(){
		this.MouseEntered+=OnMouseEntered;
		this.MouseExited+=OnMouseExited;
		_secondLevelMenu.MouseEntered+=()=>{
			isMouseInSecondLevel=true;
		};
		_secondLevelMenu.MouseExited+=()=>{
			isMouseInSecondLevel=false;
		};
	}

	private void HideSecondLevelMenu(){
		if(!isMouseInSecondLevel&&_secondLevelMenu.Visible&&Input.IsActionJustPressed("ui_mouse_left")){
			_secondLevelMenu.Hide();
		}

	}

    private void OnMouseExited(){
		isMouseEntered=false;
    }
    private void OnMouseEntered(){
		isMouseEntered=true;
    }

	

}

using System;
using System.ComponentModel;
using Godot;
using Godot.Collections;


public partial class UI_InventoryManager : GridContainer{

	#region Node
	private TextureRect _currTextureRect;
	private UI_ItemInSlot _currSlot=null;
	private UI_ItemInSlot _originalSlot;
	#endregion

	#region Variables
	private Dictionary <int,Variant> _itemDB;

	private bool isItemHolding=false;

	#endregion

	public override void _Ready(){
		InitializeNode();
		InitializeVariables();
		InitializeSignal();
		AddItem(1);

	}

    private void InitializeVariables(){
		_itemDB=GlobalDatabaseManager.Instance.ItemListDB;

    }

    public override void _Process(double delta){
		MovingItem(_currTextureRect);
	}

    private void InitializeNode(){ //TODO add item info to every slot
		foreach(UI_ItemInSlot n in GetChildren()){
			n.MouseEntered+=()=>{
				Callable callable=Callable.From(()=>OnMouseEntered(n));
				callable.Call();
			};
			n.MouseExited+=()=>{
				Callable callable=Callable.From(()=>OnMouseExited(n));
				callable.Call();
			};
			//TODO load inventory state from save manager

			n.GetChild<TextureRect>(0).GuiInput+=(InputEvent @event)=>{
				Callable callable=Callable.From(()=>OnMouseHold(@event,n.GetChild<TextureRect>(0))); //Get the signal emitter
				callable.Call();
			};
		}

	}

	#region SignalFuncs
    private void OnMouseHold(InputEvent @event,TextureRect itemTextureRect){
		if(@event is InputEventMouseButton mouseEvent &&mouseEvent.ButtonIndex==MouseButton.Left&&mouseEvent.Pressed){
			_originalSlot=itemTextureRect.GetParent<UI_ItemInSlot>();
			var tmp=itemTextureRect.Duplicate();
			GetTree().Root.AddChild(tmp);
			_currTextureRect=(TextureRect)tmp;
			itemTextureRect.Texture=null;
			isItemHolding=true;
		}

    }

	private void OnMouseEntered(UI_ItemInSlot slot){
		_currSlot=slot;
	}
	private void OnMouseExited(UI_ItemInSlot slot){
		_currSlot=null;
	}

	#endregion
	
	private void InitializeSignal(){

	}


    private void MovingItem(TextureRect item){
		Vector2 offset=new Vector2(-10,-10);
		if(item==null)
			return;
		item.MouseFilter=MouseFilterEnum.Ignore;
		item.GlobalPosition=GetGlobalMousePosition()+offset;
		
		
		if(Input.IsActionJustReleased("ui_mouse_left")){
			if(_currSlot==null){
				_originalSlot.GetChild<TextureRect>(0).Texture=item.Texture; //TODO move item data
			}
			else{
				_currSlot.GetChild<TextureRect>(0).Texture=item.Texture;
			}
			_currTextureRect=null;
			item.QueueFree();
		}
    }

	public void AddItem(int itemNo){
		ItemModel item=(ItemModel)_itemDB[itemNo];
		UI_ItemInSlot firstEmptySlot=null;
		foreach(UI_ItemInSlot slot in GetChildren()){
			if(slot._currItem==null&&firstEmptySlot==null){
				firstEmptySlot=slot;
				continue;
			}
			if(slot._currItem==item){
				slot._itemAmount++;
			}
		}
		if(firstEmptySlot==null){
			GD.Print("Inventory is full");
		}
		else{
			firstEmptySlot._currItem=item;
			firstEmptySlot._itemAmount=1;
			firstEmptySlot.GetChild<TextureRect>(0).Texture=GD.Load<Texture2D>(item.ItemTexturePath);
		}

	}

}

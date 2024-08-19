using System;
using System.ComponentModel;
using Godot;
using Godot.Collections;


public partial class UI_InventoryManager : GridContainer{

	#region Node
	private TextureRect _currTextureRect;
	private UI_ItemInSlot _currSlot=null;
	private UI_ItemInSlot _originalSlot=null;
	#endregion

	#region Variables
	private Dictionary <int,Variant> _itemDB;

	private bool isItemHolding=false;

	#endregion

	public override void _Ready(){
		InitializeNode();
		InitializeVariables();
		InitializeSignal();
		AddItem(2);
		AddItem(2);
		AddItem(1);
		AddItem(1);

	}

    private void InitializeVariables(){
		_itemDB=GlobalDatabaseManager.Instance.ItemListDB;

    }

    public override void _Process(double delta){
		MovingItem(_currTextureRect);
	}

    private void InitializeNode(){ 
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
				Callable callable=Callable.From(()=>OnMouseHold(@event,n.GetChild<TextureRect>(0))); //Get the signal emitter here
				callable.Call();
			};
		}

	}

	#region SignalFuncs
    private void OnMouseHold(InputEvent @event,TextureRect itemTextureRect){
		if(@event is InputEventMouseButton mouseEvent &&mouseEvent.ButtonIndex==MouseButton.Left&&mouseEvent.Pressed){
			_originalSlot=itemTextureRect.GetParent<UI_ItemInSlot>();
			var tmp=itemTextureRect.Duplicate();
			_originalSlot.ClearAmountLabel();
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
		GlobalEventPublisher.Instance.ItemPickEvent+=AddItem;
	}


    private void MovingItem(TextureRect item){
		Vector2 offset=new Vector2(-10,-10);
		if(item==null)
			return;
		item.MouseFilter=MouseFilterEnum.Ignore;
		item.GlobalPosition=GetGlobalMousePosition()+offset;
		
		
		if(Input.IsActionJustReleased("ui_mouse_left")){
			if(_currSlot==null){
				_originalSlot.GetChild<TextureRect>(0).Texture=item.Texture; 
				_originalSlot.UpdateAmountLabel();
			}
			else{ //Mouse hover to other slot
				if(_originalSlot._currItem==_currSlot._currItem||_currSlot._currItem==null){
					_currSlot.GetChild<TextureRect>(0).Texture=item.Texture; //TODO check is combinable or not
					_currSlot.MoveItem(_originalSlot);
				}
				else{
					_originalSlot.GetChild<TextureRect>(0).Texture=item.Texture; //Different Item go back to original slot
					_originalSlot.UpdateAmountLabel();
				}
			}
			_currTextureRect=null;
			item.QueueFree();
		}
    }

	public bool AddItem(int itemNo){
		ItemModel item=(ItemModel)_itemDB[itemNo];
		UI_ItemInSlot firstEmptySlot=null;
		foreach(UI_ItemInSlot slot in GetChildren()){
			if(slot._currItem==null&&firstEmptySlot==null){
				firstEmptySlot=slot;
				continue;
			}
			if(slot._currItem==item&&slot._itemAmount<item.StackLimit){
				slot._itemAmount++;
				slot.UpdateAmountLabel();
				return true;
			}

		}
		if(firstEmptySlot==null){
			GD.Print("Inventory is full");
			return false;
		}
		else{
			firstEmptySlot._currItem=item;
			firstEmptySlot._itemAmount=1;
			firstEmptySlot.GetChild<TextureRect>(0).Texture=GD.Load<Texture2D>(item.ItemTexturePath);
			firstEmptySlot.UpdateAmountLabel();
			return true;
		}
	}

}

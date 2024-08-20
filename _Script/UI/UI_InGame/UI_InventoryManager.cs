
using System;
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

	public override void _Ready(){ //TODO add slots dynamically
		InitializeNode();
		InitializeVariables();
		InitializeSignal();
		LoadInventoryFromSave();
		GlobalSaveManager.Instance.WriteSave(); //TEST function
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
		GlobalEventPublisher.Instance.ItemPickEvent+=AddItemToInventory;
		GlobalPlayerInventory.Instance.SaveInventoryEvent+=SaveInventory;
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
					_currSlot.MoveItemToSlot(_originalSlot);
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

	public bool AddItemToInventory(int itemNo){
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
			firstEmptySlot.AddItemToSlot(item);
			return true;
		}
	}

    public Variant SaveInventory(){
		var saveData=new Dictionary<string, string>();
		foreach(UI_ItemInSlot slot in GetChildren()){
			if(slot._currItem!=null){
				saveData[slot.Name]=slot.ToString();
				GD.Print(slot.ToString());
			}else{
				saveData[slot.Name]=null;
			}
		}
		return saveData;
    }
    private void LoadInventoryFromSave(){
		Variant loadedData=GlobalPlayerInventory.Instance.LoadedInventoryData;
		if(loadedData.VariantType==Variant.Type.Nil){
			GD.Print("Inventory save data broken");
			return;
		}
		var data=new Dictionary<string, string>((Dictionary)loadedData);
		for(int i=0;i<GetChildCount()-1;i++){
			UI_ItemInSlot currSlot=GetChild<UI_ItemInSlot>(i);
			if(data.ContainsKey(currSlot.Name)){
				var parts = data[currSlot.Name].Split(',');
				if (parts.Length != 2)
            		throw new FormatException("Input string is not in the correct format");
				int loadedItemNo=int.Parse(parts[0]);
				int loadedItemAmount=int.Parse(parts[1]);
				ItemModel item=(ItemModel)_itemDB[loadedItemNo];
				currSlot.AddItemToSlot(item,loadedItemAmount);

			}
		}
    }
}

using System;
using System.ComponentModel;
using Godot;
using Godot.Collections;


public partial class UI_InventoryManager : GridContainer{

	#region Node
	public  Dictionary<Control,ItemModel> _slotItemDict;

	#endregion

	#region Variables
	private Dictionary <int,Variant> _itemDB;
	private TextureRect _currTextureRect;
	private Control _currSlot=null;
	private bool isItemHolding=false;
	private TextureRect _originalSlot;

	#endregion

	public override void _Ready(){
		InitializeNode();
		InitializeSignal();
		InitializeVariables();
		AddItem(1);

	}

    private void InitializeVariables(){
		_itemDB=GlobalDatabaseManager.Instance.ItemListDB;
    }

    public override void _Process(double delta){
		MovingItem(_currTextureRect);
	}

    private void InitializeNode(){ //TODO add item info to every slot
		_slotItemDict=new Dictionary<Control,ItemModel>();
		foreach(Control n in GetChildren()){
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
				Callable callable=Callable.From(()=>OnMouseClick(@event,n.GetChild<TextureRect>(0))); //Get the signal emitter
				callable.Call();
			};
		}

	}

	#region SignalFuncs
    private void OnMouseClick(InputEvent @event,TextureRect itemTextureRect){
		if(@event is InputEventMouseButton mouseEvent &&mouseEvent.ButtonIndex==MouseButton.Left&&mouseEvent.Pressed){
			_originalSlot=itemTextureRect;
			var tmp=itemTextureRect.Duplicate();
			GetTree().Root.AddChild(tmp);
			_currTextureRect=(TextureRect)tmp;
			itemTextureRect.Texture=null;
			isItemHolding=true;
		}

    }

	private void OnMouseEntered(Control node){
		_currSlot=node;
	}
	private void OnMouseExited(Control node){
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
				_originalSlot.Texture=item.Texture;
			}
			else{
				_currSlot.GetChild<TextureRect>(0).Texture=item.Texture;
			}
			_currTextureRect=null;
			item.QueueFree();
		}
    }

	public void AddItem(int itemNo){
		foreach(Control slot in GetChildren()){
			ItemModel item=(ItemModel)_itemDB[itemNo];
			if(!_slotItemDict.ContainsKey(slot)){ //TODO check if the inventory is null
				_slotItemDict[slot]=item;
				slot.GetChild<TextureRect>(0).Texture=GD.Load<Texture2D>(item.ItemTexturePath);
				break;
			}
		}
	}

}

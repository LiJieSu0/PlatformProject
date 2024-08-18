using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class UI_InventoryManager : GridContainer
{

	#region Node
	public  Dictionary<Control,TextureRect> _slotItemDict;

	#endregion

	#region Variables
	private TextureRect _currTextureRect;
	private Control _currSlot=null;
	private bool isItemHolding=false;
	private TextureRect _originalSlot;

	#endregion

	public override void _Ready(){
		InitializeNode();
		InitializeSignal();

	}

	public override void _Process(double delta){
		MovingItem(_currTextureRect);
		if(_currSlot!=null)
			GD.Print(_currSlot.Name);
	}



    private void InitializeNode(){ //TODO add item info to every slot
		_slotItemDict=new Dictionary<Control,TextureRect>();
		foreach(Control n in GetChildren()){
			n.MouseEntered+=()=>{
				Callable callable=Callable.From(()=>OnMouseEntered(n));
				callable.Call();
			};
			n.MouseExited+=()=>{
				Callable callable=Callable.From(()=>OnMouseExited(n));
				callable.Call();
			};
			_slotItemDict[n]=n.GetChild<TextureRect>(0);
			n.GetChild<TextureRect>(0).GuiInput+=(InputEvent @event)=>{
				Callable callable=Callable.From(()=>OnMouseClick(@event,n.GetChild<TextureRect>(0))); //Get the signal emitter
				callable.Call();
			};
		}

	}

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


}

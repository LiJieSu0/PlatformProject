using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class UI_InventoryManager : GridContainer
{

	#region Node
	public  Dictionary<Node,TextureRect> _slotItemDict;

	#endregion

	#region Variables
	private TextureRect _currTextureRect;
	private bool isItemHolding=false;
	private TextureRect _originalSlot;

	#endregion

	public override void _Ready(){
		InitializeNode();
		InitializeSignal();

	}

	public override void _Process(double delta){
		MovingItem(_currTextureRect);
	}



    private void InitializeNode(){
		_slotItemDict=new Dictionary<Node,TextureRect>();
		foreach(Node n in GetChildren()){
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
	private void InitializeSignal(){

	}


    private void MovingItem(TextureRect item){
		Vector2 offset=new Vector2(-10,-10);
		if(item==null)
			return;
		item.GlobalPosition=GetGlobalMousePosition()+offset;
		
		if(Input.IsActionJustReleased("ui_mouse_left")){
			_originalSlot.Texture=item.Texture;
			item.QueueFree();
			_currTextureRect=null;
		}
    }


}

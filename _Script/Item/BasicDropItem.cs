using Godot;
using System;

public partial class BasicDropItem : RigidBody2D{
	#region Variables
	public const float disappearTime=10f;
	public int _itemNo;
	public string _itemName;
	public bool isPlayerInPickUpArea=false;
	private Vector2 _itemSpriteSize=new Vector2(10,10); //Fixed Item size 
	#endregion


	#region Node
	Sprite2D sprite2D;
	Area2D pickUpArea;
	Timer disappearTimer;
	#endregion

	public override void _Ready(){
		InitializeNode();
		InitializeVariables();
		InitialSignal();
	}

	public override void _Process(double delta){
	}
	

	public void InitializeNode(){
		sprite2D=GetNode<Sprite2D>("Sprite2D");
		pickUpArea=GetNode<Area2D>("PickUpArea");
		disappearTimer=GetNode<Timer>("DisappearTimer");
	}

	public void InitializeVariables(){
		disappearTimer.WaitTime=disappearTime;
	}

	public void InitialSignal(){
		pickUpArea.BodyEntered+=OnPlayerInPickUpArea;
		pickUpArea.BodyExited+=OnPlayerExitPickUpArea;
	}

	public void LoadDropItemTexture(string texturePath){
		sprite2D.Texture=(Texture2D)ResourceLoader.Load<Texture2D>(texturePath);
		sprite2D.Scale=UtilsFunc.DesiredSpriteScale(sprite2D.Texture.GetSize(),_itemSpriteSize);
	}

    private void OnPlayerInPickUpArea(Node2D body){
		isPlayerInPickUpArea=true;
    }
	private void OnPlayerExitPickUpArea(Node2D body){
		isPlayerInPickUpArea=false;
	}


}

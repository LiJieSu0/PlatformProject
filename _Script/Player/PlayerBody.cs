using System;
using Godot;
public partial class PlayerBody : CharacterBody2D{

	[Export] ShaderMaterial dashShader;

	#region MovementVairables
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	#endregion

	#region Nodes
	public StatusManager statusManager;
	private AnimationTree _animationTree;
	private AnimationNodeStateMachinePlayback _animationState;
	private Sprite2D _sprite2D;
	private SkillNode _skillNode;
	private Area2D _attackArea;
	private Area2D _interactableArea;

	#endregion

	#region Variables
	private bool isTalking=false;
	public bool isFaceRight=true;
	private bool isAttack=false;
	private bool isDashCD=false;
	private bool isDashing=false;
	private float dashingTime=0.5f;
	private float dashCD=2;
	private INPC npcInRange=null; //TODO change to list
	#endregion
    
	#region Resource
	#endregion
	//TODO add effect from the stats
	public override void _Ready(){
		InitializeNode();
		InitializeSignal();
    }

    public override void _PhysicsProcess(double delta){
		PauseFunction();
		if(Engine.TimeScale==0)
			return;
		PlayerMovement((float)delta);
		RecoverMana((float)delta);
		TestFunction();
		InteractWithEnivronment();
		TalkToNpc();
		ChangeCurrSkill();
	}

	#region InputFuncs
	private void PlayerMovement(float delta){
		if(isTalking)
			return; 
		if(Input.IsActionJustPressed("ui_dash")&&!isDashing)
			Dash();
		AttackFunction();
		if(isAttack)
			return;
		Vector2 velocity = Velocity;
		if (!IsOnFloor()){
			velocity.Y += gravity * delta;
			_animationState.Travel("JumpFall");
		}

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()){
			velocity.Y = JumpVelocity;
			_animationState.Travel("JumpStart");
		}
		if(Input.IsActionPressed("move_left")||Input.IsActionPressed("move_right")){
			_animationState.Travel("Run");
		}
		if(Input.IsActionJustReleased("move_left")||Input.IsActionJustReleased("move_right")&&IsOnFloor()){
			_animationState.Travel("Idle");
		}

		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero){
			velocity.X = direction.X * Speed;
		}
		else{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		Vector2 faceDir=(GetGlobalMousePosition()-this.GlobalPosition).Normalized();
		if((faceDir.X>0&&!isFaceRight)||(faceDir.X<0&&isFaceRight)){ 
			isFaceRight=!isFaceRight;
			_sprite2D.Scale=new Vector2(_sprite2D.Scale.X*-1,_sprite2D.Scale.Y);
		}
		MoveAndSlide();
	}

	private void AttackFunction(){
		if(Input.IsActionJustPressed("ui_cast")){
			_skillNode.CastSkill();
		}
		if(Input.IsActionJustPressed("ui_attack")&&IsOnFloor()&&statusManager.CurrMp>=10&&!isAttack){
			isAttack=true;
			statusManager.CurrMp-=10;
			_animationState.Travel("Attack");
		}
		if(isAttack){
			return;
		}
	}

	private void InteractWithEnivronment(){
		if(Input.IsActionJustPressed("ui_interact")){ //TODO interact with enviornment
		}
	}

	private void TalkToNpc(){
		if(Input.IsActionJustPressed("ui_talk")&&npcInRange!=null&&!isTalking){
			npcInRange.InteractReaction();
			isTalking=true;
		}
		if(Input.IsActionJustPressed("ui_accept")&&isTalking){
			GlobalDialogManager.Instance.NextDialogueTrigger();
		}

	}
		private void ChangeCurrSkill(){ 
		for (int i = 0; i < 3; i++){
			if (Input.IsActionJustPressed($"ui_skill_{i + 1}")){
				statusManager.CurrSkillIdx = i;
				GlobalEventPublisher.Instance.SkillChangeTrigger(i);
				break;
			}
		}
	}

	private void PauseFunction(){
		if(Input.IsActionJustPressed("ui_pause")){
			GlobalEventPublisher.Instance.ShowTabMenuTrigger();
		}
		Engine.TimeScale=GlobalEventPublisher.Instance.isShowMenu?0:1;
	}
	
	private void Dash(){ //TODO improve dash function
		isDashing=true;
		Timer dashTimer = new Timer();
        AddChild(dashTimer);
        dashTimer.WaitTime = 0.5;
        dashTimer.OneShot = true;
		dashTimer.Timeout+=()=>{
			isDashing=false;
			dashTimer.QueueFree();
		};
		dashTimer.Start();
		Timer shaderTimer = new Timer();
		AddChild(shaderTimer);
		shaderTimer.WaitTime = 0.01;
		shaderTimer.Autostart = true;
		shaderTimer.Timeout += () =>
		{
			if (isDashing){
				DashShaderCreate();
			}
			else
			{
				shaderTimer.Stop();
				shaderTimer.QueueFree();
			}
		};
		shaderTimer.Start();
		if(isFaceRight){
			Velocity=new Vector2(1,0)*2000;
		}
		else{
			Velocity=new Vector2(-1,0)*2000;
		}
	}

	private void DashShaderCreate(){
		Sprite2D duplicate=(Sprite2D)_sprite2D.Duplicate(15);
		duplicate.Material=dashShader;
		duplicate.GlobalPosition=this.GlobalPosition+new Vector2(0,-0.01f);
		GetTree().Root.AddChild(duplicate);
		Timer disappearTimer=new Timer();
		AddChild(disappearTimer);
		disappearTimer.WaitTime=0.05;
		disappearTimer.Timeout+=()=>{
			duplicate.QueueFree();
			disappearTimer.QueueFree();
		};
		disappearTimer.Start();
	}


	#endregion




	#region StatusUpdate
	public void ReceiveDamage(float damage){
		statusManager.CurrHp-=damage;
		if(statusManager.CurrHp<=0){
			PlayerDead();
		}
	}
	public void DecreaseMana(int mana){
		statusManager.CurrMp-=mana;
	}

	private void RecoverMana(float time){
		if(statusManager.CurrMp>statusManager.MaxMp)
			return;
		statusManager.CurrMp+=100*time; //TODO read recover mana per second from status
	}

	private void RecoverHp(float time){
		if(statusManager.CurrHp>statusManager.MaxHp)
			return;
		statusManager.CurrHp+=100*time; //TODO read recover mana per second from status
	}
	#endregion

	private void InitializeNode(){ 
		_sprite2D=GetNode<Sprite2D>("Sprite2D");
		_animationTree=GetNode<AnimationTree>("AnimationTree");
		_animationState = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		_skillNode=GetNode<SkillNode>("SkillNode");
		_attackArea=GetNode<Area2D>("Sprite2D/AttackArea");
		_interactableArea=GetNode<Area2D>("Sprite2D/InteractableArea");
		statusManager=GetParent().GetNode<StatusManager>("StatusManager");
	}

	private void InitializeSignal(){
		_animationTree.AnimationFinished+=OnAnimationFinsihed;
		_attackArea.BodyEntered+=OnAttackHit;
		_interactableArea.AreaEntered+=OnInteractableAreaEnter;
		_interactableArea.AreaExited+=OnInteractableAreaExit;
		GlobalDialogManager.Instance.EndDialogueEvent+=OnDialogueEnd;
	}

    #region SignalFuncs
    private void OnAttackHit(Node2D body){
        if(body is BasicEnemy e){
			e.ReceiveDamage((int)statusManager.AttackPower);
		}
		else{
			GD.Print("Attack function attack on not enemy");
		}
    }


    private void OnAnimationFinsihed(StringName animName){
    if(animName=="Attack"){
			isAttack=false;
			_animationState.Travel("Idle");
		}
    }

	
	private void OnInteractableAreaEnter(Area2D area){
		if(area is INPC)
			npcInRange=area as INPC;
	}
	private void OnInteractableAreaExit(Area2D area){
		npcInRange=null;
	}
	private void OnDialogueEnd(string key){
		isTalking=false;
	}

	#endregion

	public void TestFunction(){
		if(Input.IsActionJustPressed("receiveDamage")){
			ReceiveDamage(10);
		}
		if(Input.IsActionJustPressed("reduceMana")){
			DecreaseMana(10);
		}
	}

	private void PlayerDead(){
		GD.Print("Play dead animation");
		// TODO change to disable all player controller
		GD.Print("Play dead UI and reload");
	}



}

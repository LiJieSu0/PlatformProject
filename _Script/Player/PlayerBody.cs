using Godot;
using DialogueManagerRuntime;
public partial class PlayerBody : CharacterBody2D{

	#region Movement
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	#endregion

	#region Nodes
	private StatusManager statusManager;
	private AnimationTree _animationTree;
	private AnimationNodeStateMachinePlayback _animationState;
	private Sprite2D sprite2D;
	private SkillNode skillNode;
	#endregion

	#region Variables
	public bool isFaceRight=true;
	private bool isAttack=false;
	private Area2D areaInteractableRange=null; //TODO change to list
	#endregion
    
	#region Resource
	[Export] public Resource dialogueResource;
	#endregion

	public override void _Ready()
    {
		sprite2D=GetNode<Sprite2D>("Sprite2D");
		statusManager=GetParent().GetNode<StatusManager>("StatusManager");
		_animationTree=GetNode<AnimationTree>("AnimationTree");
		_animationState = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		skillNode=GetNode<SkillNode	>("SkillNode");
    }

    public override void _PhysicsProcess(double delta){
		PauseFunction();
		if(Engine.TimeScale==0)
			return;
		PlayerMovement((float)delta);
		RecoverMana((float)delta);
		TestFunction();
		InteractWithEnivronment();
		ChangeCurrSkill();
	}
	private void PlayerMovement(float delta){ //TODO fix attack while running animation bug when running and press attack cannot attack anymore
		AttackFunction();
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
			sprite2D.Scale=new Vector2(sprite2D.Scale.X*-1,sprite2D.Scale.Y);
		}
		MoveAndSlide();
	}
	private void OnAnimationFinsihed(string animName){
		if(animName=="Attack"){
			isAttack=false;
			_animationState.Travel("Idle");
		}
	}

	private void OnInteractableAreaEnter(Area2D area){
		if(area is INPC)
		areaInteractableRange=area;
	}
	private void OnInteractableAreaExit(Area2D area){
		areaInteractableRange=null;
	}

	public void ReceiveDamage(float damage){
		statusManager.CurrHp-=damage;
		if(statusManager.CurrHp<=0){
			PlayerDead();
		}
	}
	public void DecreaseMana(int mana){
		statusManager.CurrMp-=mana;
	}

	private void AttackFunction(){
		if(Input.IsActionJustPressed("ui_cast")){
			skillNode.CastSkill();
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

	private void RecoverMana(float time){
		if(statusManager.CurrMp>statusManager.MaxMp)
			return;
		statusManager.CurrMp+=100*time; //TODO read recover mana per second from status
	}

	private void OnAttackBody(Node2D node){
		if(node is BasicEnemy e){
			e.ReceiveDamage((int)statusManager.AttackPower);
		}
		else{
			GD.Print("Attack function attack on not enemy");
		}
	}
	public void TestFunction(){
		if(Input.IsActionJustPressed("receiveDamage")){
			ReceiveDamage(10);
		}
		if(Input.IsActionJustPressed("reduceMana")){
			DecreaseMana(10);
		}
	}

	private void InteractWithEnivronment(){
		if(Input.IsActionJustPressed("ui_interact")&&areaInteractableRange!=null){
			INPC npc=(INPC)areaInteractableRange;
			npc.InteractReaction();
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
			GlobalEventPublisher.IsPause=!GlobalEventPublisher.IsPause;
		}
		Engine.TimeScale=GlobalEventPublisher.IsPause?0:1;
	}

	private void PlayerDead(){
		GD.Print("Play dead animation");
		GlobalEventPublisher.IsPause=true;// TODO change to disable all player controller
		GD.Print("Play dead UI and reload");
	}

	public void GetExpAndMoney(int exp,int money){

	}

}

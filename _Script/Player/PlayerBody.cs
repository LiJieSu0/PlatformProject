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
	private AnimationTree animationTree;
	private AnimationNodeStateMachinePlayback stateMachine;
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
		animationTree=GetNode<AnimationTree>("AnimationTree");
		stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		skillNode=GetNode<SkillNode	>("SkillNode");
    }

    public override void _PhysicsProcess(double delta)
	{
		PauseFunction();
		if(Engine.TimeScale==0)
			return;
		PlayerMovement((float)delta);
		RecoverMana((float)delta);
		TestFunction();
		InteractWithEnivronment();
		ChangeCurrSkill();

	}
	private void PlayerMovement(float delta){
		AttackFunction();
		Vector2 velocity = Velocity;
		if (!IsOnFloor()){
			velocity.Y += gravity * delta;
			stateMachine.Travel("JumpFall");
		}

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()){
			velocity.Y = JumpVelocity;
			stateMachine.Travel("JumpStart");
		}
		if(Input.IsActionPressed("ui_left")||Input.IsActionPressed("ui_right")){
			stateMachine.Travel("Run");
		}
		if(Input.IsActionJustReleased("ui_left")||Input.IsActionJustReleased("ui_right")&&IsOnFloor()){
			stateMachine.Travel("Idle");
		}

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero){
			velocity.X = direction.X * Speed;
		}
		else{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		Vector2 faceDir=(GetGlobalMousePosition()-this.GlobalPosition).Normalized();
		if((faceDir.X>0&&!isFaceRight)||(faceDir.X<0&&isFaceRight)){ //TODO change face direction with mouse cursor
			isFaceRight=!isFaceRight;
			sprite2D.Scale=new Vector2(sprite2D.Scale.X*-1,sprite2D.Scale.Y);
		}
		MoveAndSlide();
	}
	private void OnAnimationFinsihed(string animName){
		if(animName=="Attack"){
			isAttack=false;
			stateMachine.Travel("Idle");
		}
	}

	private void OnInteractableAreaEnter(Area2D area){
		if(area is INPC)
		areaInteractableRange=area;
	}
	private void OnInteractableAreaExit(Area2D area){
		areaInteractableRange=null;
	}

	public void ReceiveDamage(int damage){
		statusManager.CurrHp-=damage;
	}
	public void DecreaseMana(int mana){
		statusManager.CurrMp-=mana;
	}

	private void AttackFunction(){
		if(Input.IsActionJustPressed("ui_cast")){
			skillNode.CastSkill();
		}
		if(Input.IsActionJustPressed("ui_attack")&&IsOnFloor()&&statusManager.CurrMp>=10&&!isAttack){
			//TODO attack animation will cause isAttack cannot transback to true statement, bug need to be fixed
			isAttack=true;
			statusManager.CurrMp-=10;
			stateMachine.Travel("Attack");
		}
		if(isAttack){
			return;
		}
	}

	private void RecoverMana(float time){
		if(statusManager.CurrMp>statusManager.MaxMp)
			return;
		statusManager.CurrMp+=1*time; //TODO read recover mana per second from status
	}

	private void OnAttackBody(Node2D node){
		if(node is IEnemy e){
			e.ReceiveDamage((int)statusManager.AttackPower);
		}else{
			GD.Print(node.Name+" is not an enemy");
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

}

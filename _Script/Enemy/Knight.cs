using Godot;


public partial class Knight : BasicEnemy
{
	//TODO animation redo

    public override void _Ready()
    {
		base.InitializeNode();
		base.InitializeStatus();
		base.InitialSignal();
		base.RngMove();
    }

    public override void _PhysicsProcess(double delta)
	{
		FSM_Action();
		MoveAndSlide();
	}


    private void FSM_Action(){
		switch(_currFSMState){
			//TODO refactor FSM state
			case FSMStates.Idle:
				_animationState.Travel("Idle");
				break;
			case FSMStates.Patrol:
				base.Move();
		        _animationState.Travel("Run");
				break;
			case FSMStates.Chase:
				base.ChaseMove();
                _animationState.Travel("Run");
				break;
			case FSMStates.Attack:
				base.StopMoving();
				if(!isAttackCD){
					_animationState.Travel("Attack");
				}
				else{
					_animationState.Travel("Idle");
				}
				break;
			default:
				break;
		}
	}

	public override void OnAnimationFinished(StringName aniName){
		if(aniName=="Attack"){
			_attackTimer.Start();
			isAttackCD=true;
		}
	}

    public override void Attack(){
        base.Attack();
    }

}

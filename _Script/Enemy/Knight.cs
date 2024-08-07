using Godot;


public partial class Knight : BasicEnemy
{


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
		AnimationState();
	}


    private void FSM_Action(){
		switch(_fsm._currState.Name){
			//TODO refactor FSM state
			case FSMStates.PATROL_MODE:
				base.Move();

				break;
			case FSMStates.FOLLOW_MODE:
				base.FollowMove();
				break;
			case FSMStates.ATTACK_MODE:
				//TODO add stop velocity
				break;
			default:
				break;
		}
	}
    public void AnimationState(){
        switch(_fsm._currState.Name){
			case FSMStates.FOLLOW_MODE:
		        _animationState.Travel("Run");
				break;
            case FSMStates.PATROL_MODE:
                _animationState.Travel("Run");
                break;
			case FSMStates.ATTACK_MODE:
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

    public override void Attack()
    {
        base.Attack();
    }

}

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
			case FSMStates.PATROL_MODE:
				base.Move();

				break;
			case FSMStates.FOLLOW_MODE:
				base.FollowMove();
				break;
			default:
				break;
		}
	}
    public void AnimationState(){
        switch(_fsm._currState.Name){
            case FSMStates.PATROL_MODE:
                _animationState.Travel("Run");
                break;
            default:
                break;
        }
    }
}

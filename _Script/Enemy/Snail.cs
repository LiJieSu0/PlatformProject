using Godot;


public partial class Snail : BasicEnemy
{


    public override void _Ready()
    {
		base.InitializeNode();
		base.InitializeStatus();
		base.RngMove();
    }

    public override void _PhysicsProcess(double delta)
	{
		FSM_Action();
		MoveAndSlide();
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



	private void OnPlayerDetect(Node2D body){
        _target=body;
		_fsm.TransitionTo(FSMStates.FOLLOW_MODE);
	}
	private void OnPlayerExit(Node2D body){
		_target=null;
		_fsm.TransitionTo(FSMStates.PATROL_MODE);
	}


}

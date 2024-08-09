using Godot;


public partial class Snail : BasicEnemy
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
	}

	private void FSM_Action(){
		switch(_currFSMState){
			case FSMStates.Patrol:
				base.Move();
				break;
			case FSMStates.Chase:
				base.ChaseMove();
				break;
			default:
				break;
		}
	}

	

}

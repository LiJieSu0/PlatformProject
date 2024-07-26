
using Godot;
using System;

public partial class BasicEnemy :CharacterBody2D{
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    [Export] EnemyRes enemyRes;
	[Export]public float Speed = 100.0f;


    #region Status
    public string EnemyName;
    public int MaxHp;
    public int CurrHp;
    public int BasicDamage;
    #endregion
	
    #region Node
    public TextureProgressBar _hpBar;
	public ItemDropManager _itemDropManager;
    public StateMachine _fsm;
    public Sprite2D _sprite;
    public RayCast2D _leftRayCast;
    public Node2D _target;

    public Area2D _hitBox;
    public Area2D _detectArea;
    private AnimationTree _animationTree;
    private AnimationNodeStateMachinePlayback _animationState;
    #endregion

    #region Variables
    public bool isFaceLeft=true;
    public Vector2 _moveSpeed;
    RandomNumberGenerator rng;
    #endregion

    public virtual void PatrolMove(float delta){}
    public virtual void FollowModeMove(float delta){}
    public virtual void Attack(){}
    public virtual void CollisionDamage(){}

    public void InitializeNode(){
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _animationState = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		_itemDropManager=GetNode<ItemDropManager>("ItemDropManager");
        _fsm=GetNode<StateMachine>("FSM");
        _sprite=GetNode<Sprite2D>("Sprite2D");
        _leftRayCast=GetNode<RayCast2D>("Sprite2D/LeftRayCast");
        _hpBar=GetNode<TextureProgressBar>("MobHpBar");
        _detectArea=GetNode<Area2D>("DetectArea");
        _hitBox=GetNode<Area2D>("Hitbox");
        _hpBar.MaxValue=MaxHp;
        _hpBar.Value=CurrHp;
    }

    public void InitialSignal(){
        _detectArea.BodyEntered+=OnPlayerDetect;
        _detectArea.BodyExited+=OnPlayerExit;
        _hitBox.BodyEntered+=OnPlayerCollide;
    }

    public void InitializeStatus(){
        this.EnemyName=enemyRes.EnemyName;
        this.BasicDamage=enemyRes.BasicDamage;
        this.MaxHp=enemyRes.MaxHp;
        CurrHp=MaxHp;
        _hpBar.MaxValue=MaxHp;
        _hpBar.Value=CurrHp;
        _hpBar.Hide();
        _moveSpeed=new Vector2(-Speed,0);
        rng=new RandomNumberGenerator();
        rng.Randomize();
    }
    public void ReceiveDamage(int damage){
		_hpBar.Show();
		this.CurrHp-=damage;
        _hpBar.Value=CurrHp;
		if(CurrHp<=0){
			_itemDropManager.CallDeferred("ItemDropInstantiate");
			QueueFree();
		}
    }

    public void RngMove(){
        int rngValue=rng.RandiRange(1,100);
        if(rngValue>51){
            return;
        }
        else{
            Flip();
        }
    }

    public void Move(){
		if(isFaceLeft){
            Velocity=_moveSpeed;
        }else{
            Velocity=new Vector2(-1*_moveSpeed.X,0);
        }
        if(!_leftRayCast.IsColliding()){
            Flip();
        }
	}
    public void Flip(){
        this._sprite.Scale=new Vector2(_sprite.Scale.X*-1,_sprite.Scale.Y);
        isFaceLeft=!isFaceLeft;
    }

    public void FollowMove(){
        if(_target==null)
            return;
        float dir=(_target.GlobalPosition-this.GlobalPosition).Normalized().X;
        if((dir<0 && !isFaceLeft)||(dir>0&&isFaceLeft)&&_leftRayCast.IsColliding()){ 
            Flip();
        }
        Move();

    }
    private void OnPlayerCollide(Node2D body){
		if(body is PlayerBody player){
			player.ReceiveDamage(this.BasicDamage);
		}
	}

	private void OnPlayerDetect(Node2D body){
            _target = body;
            _fsm.TransitionTo(FSMStates.FOLLOW_MODE);
	}

    private void OnPlayerExit(Node2D body){
            _target = null;
            _fsm.TransitionTo(FSMStates.PATROL_MODE);
	}


}
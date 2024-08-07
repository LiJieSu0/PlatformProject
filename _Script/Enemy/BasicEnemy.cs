
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

    public Area2D _damageArea;
    public Area2D _detectArea;
    public Area2D _attackRangeArea;
    public AnimationTree _animationTree;
    public AnimationNodeStateMachinePlayback _animationState;
    public Timer _attackTimer;

    #endregion

    #region Variables
    public bool isFaceLeft=true;
    public bool isAttackCD=false;
    public Vector2 _moveSpeed;

    public float _attackCDTime=2.0f;
    RandomNumberGenerator rng;
    #endregion

    public virtual void PatrolMove(float delta){}
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
        _damageArea=GetNode<Area2D>("DamageArea");
        _hpBar.MaxValue=MaxHp;
        _hpBar.Value=CurrHp;
        try{
            _attackRangeArea=GetNode<Area2D>("Sprite2D/AttackRange");
            _attackTimer=GetNode<Timer>("AttackTimer");
        }catch(Exception){
            _attackRangeArea=null;
            _attackTimer=null;
        }   
    }

    public void InitialSignal(){
        _detectArea.BodyEntered+=OnPlayerDetect;
        _detectArea.BodyExited+=OnPlayerExit;
        _damageArea.BodyEntered+=OnPlayerDamaged;
        _animationTree.AnimationFinished+=OnAnimationFinished;
        if(_attackRangeArea!=null){
            _attackRangeArea.BodyEntered+=OnPlayerInAttackRange;
            _attackRangeArea.BodyExited+=OnPlayerLeaveAttackRange;
        }
        if(_attackTimer!=null){
            _attackTimer.WaitTime=_attackCDTime;
            _attackTimer.Timeout+=OnAttackCDTimeout;
        }

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
    public virtual void OnPlayerDamaged(Node2D body){
		if(body is PlayerBody player){
			player.ReceiveDamage(this.BasicDamage);
		}
	}

	public void OnPlayerDetect(Node2D body){
            _target = body;
            _fsm.TransitionTo(FSMStates.FOLLOW_MODE);
	}

    public void OnPlayerExit(Node2D body){
            _target = null;
            _fsm.TransitionTo(FSMStates.PATROL_MODE);
	}

    public virtual void OnPlayerInAttackRange(Node2D body){
        GD.Print("in attack range Attack mode");
        _fsm.TransitionTo(FSMStates.ATTACK_MODE);
    }

    public virtual void OnPlayerLeaveAttackRange(Node2D body)
    {
        _fsm.TransitionTo(FSMStates.PATROL_MODE);
    }

    public virtual void OnAttackCDTimeout(){
        isAttackCD=false;
    }
    public virtual void OnAnimationFinished(StringName animName){
        return;
    }
}
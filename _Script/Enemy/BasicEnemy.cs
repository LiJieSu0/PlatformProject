
using Godot;

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
    #endregion

    #region Variables
    public bool isFaceLeft=true;
    public Vector2 _moveSpeed;
    #endregion

    public virtual void PatrolMove(float delta){}
    public virtual void FollowModeMove(float delta){}
    public virtual void Attack(){}
    public virtual void DealDamage(){}

    public void InitializeNode(){
        _hpBar=GetNode<TextureProgressBar>("MobHpBar");
		_itemDropManager=GetNode<ItemDropManager>("ItemDropManager");
        _fsm=GetNode<StateMachine>("FSM");
        _sprite=GetNode<Sprite2D>("Sprite2D");
        _leftRayCast=GetNode<RayCast2D>("Sprite2D/LeftRayCast");
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
    }
    public void ReceiveDamage(int damage){
		_hpBar.Show();
		this.CurrHp-=damage;
		if(CurrHp<=0){
			_itemDropManager.CallDeferred("ItemDropInstantiate");
			QueueFree();
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
}
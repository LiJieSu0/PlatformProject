
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
    #endregion

    public virtual void PatrolMove(float delta){}
    public virtual void FollowModeMove(float delta){}
    public virtual void Attack(){}
    public virtual void DealDamage(){}

    public void InitializeNode(){
        _hpBar=GetNode<TextureProgressBar>("MobHpBar");
		_itemDropManager=GetNode<ItemDropManager>("ItemDropManager");
        _fsm=GetNode<StateMachine>("FSM");
    }

    public void InitializeStatus(){
        this.EnemyName=enemyRes.EnemyName;
        this.BasicDamage=enemyRes.BasicDamage;
        this.MaxHp=enemyRes.MaxHp;
        CurrHp=MaxHp;
        _hpBar.MaxValue=MaxHp;
        _hpBar.Value=CurrHp;
        _hpBar.Hide();
    }
    public void ReceiveDamage(int damage){
		_hpBar.Show();
		this.CurrHp-=damage;
		if(CurrHp<=0){
			_itemDropManager.CallDeferred("ItemDropInstantiate");
			QueueFree();
		}
    }

}
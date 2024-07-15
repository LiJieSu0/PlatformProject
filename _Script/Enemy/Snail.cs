using Godot;
using System;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

public partial class Snail : CharacterBody2D,IEnemy
{
	[Export]public float Speed = 100.0f;

	//TODO add resrouce to custom monster

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	#region Interface Variables
    public float MaxHp {get; set;}
    public float MaxMp {get; set;}
    public float CurrHP  {get; set;}
    public float CurrMp {get; set;}
	public float BasicDamage  {get; set;}
	public string MobName  {get; set;}
	#endregion

	#region Variables
	float time;
	#endregion

    public override void _Ready()
    {
		time=0;
		InitializeStatus();
    }
    public override void _PhysicsProcess(double delta)
	{
		time+=(float)delta;
		Vector2 velocity=new Vector2(-1,0);
		if(time<0.5){
			velocity.X =velocity.X* Speed;
			Velocity = velocity;
		}else if(time>0.5&&time<1){
			Velocity=new Vector2(0,0);
		}else{
			time=0;
		}
		MoveAndSlide();
	}


    public void ReceiveDamage(float damage){
		this.CurrHP-=damage;
		if(CurrHP<=0){
			QueueFree();
		}
    }

    public void DealDamage(float damage)
    {
    }

	private void InitializeStatus(){
		MaxHp=20;
		CurrHP=this.MaxHp;
		MaxMp=0;
		CurrMp=MaxMp;
		BasicDamage=10;
		MobName="Snail";
	}


}

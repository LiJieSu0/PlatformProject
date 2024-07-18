using Godot;
using System;
using System.ComponentModel;
using System.Drawing;

public partial class ProjectileScript : Area2D
{
	[Export] ProjectileRes projectileRes;

	Sprite2D sprite;
	CollisionShape2D collision;

	private float basicSpeed;
	private float basicDamage;
	private string projectileName;
	private Vector2 initialVelocity=new Vector2(1, 0);

	public override void _Ready()
	{
		sprite=GetNode<Sprite2D>("Sprite2D");
		sprite.Texture=ResourceLoader.Load<Texture2D>(projectileRes.ProjectileSpritePath);
		InitializeFromResource();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Position += initialVelocity*basicSpeed*(float)delta;
	}

	private void InitializeFromResource(){
		projectileName=projectileRes.ProjectileName;
		basicSpeed=projectileRes.BasicSpeed;
		basicDamage=projectileRes.BasicDamage;
	}

	private void OnProjectileHit(Node node){
		projectileRes.HitEffect();
		if(node is IEnemy enemy){
			enemy.ReceiveDamage(this.basicDamage);
		}
		QueueFree();
	}

	public void ChangeVelocity(Vector2 dir){
		initialVelocity=dir;
		float angleRadians=Mathf.Atan2(dir.Y,dir.X);
		float angleDegree=Mathf.RadToDeg(angleRadians);
		this.RotationDegrees=angleDegree;
	}

}

using Godot;
using System;

[GlobalClass]
public partial class SkillResource : Resource
{
    public enum SkillType{
        Melee,
        Ranged,
        SelfBuff,
    }

    public enum RangedShootMode{
        None,
        Single,
        Burst,
        Shot,
    }


    [Export] public string SkillName;
    [Export] public Vector2 SkillGenerateRange;
    [Export] public float ManaCost;
    [Export] public float SkillCoolDownTime;
    [Export] public SkillType skillType;
    [Export] public RangedShootMode rangedShootMode;

    public void CastSkil(PackedScene scene){ //TODO Melee skill
        //TODO design melee skill
    }

    public void CastSkil(PackedScene scene,float value){ //TODO selfbuff
        //TODO design self buff skill

    }

    public async void CastSkil(PackedScene scene, Node2D shooter, Vector2 dir,int projectileCount=1,float initAngle=0){
        switch (this.rangedShootMode){
            case RangedShootMode.Single:{
                var currProjectile=GenerateProjectile(scene,shooter);
                currProjectile.ChangeVelocity(dir);
                break;
                }
            case RangedShootMode.Burst:
                for(int i=0;i<projectileCount;i++){
                    var currProjectile=GenerateProjectile(scene, shooter);
                    currProjectile.ChangeVelocity(dir);
                    await ToSignal(shooter.GetTree().CreateTimer(0.2f),SceneTreeTimer.SignalName.Timeout);
                }
                break;
            case RangedShootMode.Shot:
                for(int i=0;i<projectileCount;i++){
                    var tmpAngle=-30.0f;
                    tmpAngle+=i*30;
                    float angleReductionRadians = Mathf.DegToRad(tmpAngle);
                    float currentAngleRadians = dir.Angle();
                    float newAngleRadians = currentAngleRadians + angleReductionRadians;
                    Vector2 newVector = new Vector2(Mathf.Cos(newAngleRadians), Mathf.Sin(newAngleRadians));
                    var currProjectile=GenerateProjectile(scene,shooter);
                    currProjectile.ChangeVelocity(newVector);
                }
                break;
            default:
                GD.Print("No default ranged shooting mode");
                break;
        }
    }

    private ProjectileScript GenerateProjectile(PackedScene scene, Node2D shooter){
        ProjectileScript projectile= (ProjectileScript)scene.Instantiate();
        shooter.GetParent().GetParent().AddChild(projectile);
        projectile.GlobalPosition=shooter.GlobalPosition;
        return projectile;
    }


}

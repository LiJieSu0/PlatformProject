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

    public void CastMeleeSkil(PackedScene scene){
        //TODO design melee skill
    }

    public void CastSelfBuffSkill(PackedScene scene){
        //TODO design self buff skill

    }

    public async void CastRangedSkill(PackedScene scene, Node2D shooter,int projectileCount=1,float initAngle=0){
        switch (this.rangedShootMode){
            case RangedShootMode.Single:
                GenerateProjectile(scene,shooter);
                break;
            case RangedShootMode.Burst:
                for(int i=0;i<projectileCount;i++){
                    GD.Print("projectil generate");
                    GenerateProjectile(scene,shooter);
                    await ToSignal(shooter.GetTree().CreateTimer(0.2f),SceneTreeTimer.SignalName.Timeout);
                }
                break;
            case RangedShootMode.Shot:
                for(int i=0;i<projectileCount;i++){
                var tmpAngle=-30.0f; //TODO calculate the cover angle off the shooting
                    tmpAngle+=i*30;
                    GD.Print(tmpAngle);
                    var currProjectile=GenerateProjectile(scene,shooter);
                    currProjectile.ChangeVelocity(tmpAngle);
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

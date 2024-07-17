using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

[GlobalClass]
public partial class ProjectileRes : Resource
{
    [Export] public string ProjectileName="";
    [Export] public float BasicSpeed;
    [Export] public float BasicDamage;
    [Export] public string ProjectileSpritePath="";
    
    public void ProjectilePath(Vector2 vector){
        vector=vector*BasicSpeed;
    }

    public void HitEffect(){
    }

    public Vector2 DegreesToVector(float degrees){
        return new Vector2(Mathf.Cos(Mathf.DegToRad(degrees)), Mathf.Sin(Mathf.DegToRad(degrees)));
    }

}

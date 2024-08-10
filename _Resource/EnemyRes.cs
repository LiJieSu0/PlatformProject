using Godot;
using System;


[GlobalClass]
public partial class EnemyRes : Resource{
    [Export] public string EnemyName="";
    [Export] public int MaxHp;
    [Export] public int BasicDamage;
    [Export] public int[] Drops; //Base on ItemNo in database to decide drop items and odds

}

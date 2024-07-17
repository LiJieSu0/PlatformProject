
using System.Net.NetworkInformation;
using Godot;

public partial class GlobalEventPublisher:Node{
    public static GlobalEventPublisher Instance { get; private set; }
    public static bool IsPause=false;
    public delegate void SkillChange(int idx);
    public event SkillChange SkillChangeEvent;
    public override void _Ready(){
		Instance=this;
    }

    public void SkillChangeTrigger(int idx){
        SkillChangeEvent?.Invoke(idx);
    }
}
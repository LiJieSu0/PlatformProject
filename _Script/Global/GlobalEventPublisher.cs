
using Godot;

public partial class GlobalEventPublisher:Node{
    public static GlobalEventPublisher Instance { get; private set; }
    public delegate void SkillChange(int idx);
    public event SkillChange SkillChangeEvent;
    public override void _Ready(){
		Instance=this;
    }

    public void SkillChangeTrigger(int idx){
        SkillChangeEvent?.Invoke(idx);
    }
}
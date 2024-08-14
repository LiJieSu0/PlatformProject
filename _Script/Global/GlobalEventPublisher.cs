
using System.Net.NetworkInformation;
using Godot;

public partial class GlobalEventPublisher:Node{
    public static GlobalEventPublisher Instance { get; private set; }
    public static bool IsPause=false;
    public bool isShowMenu=false;
    public delegate void ShowTabMenu();
    public event ShowTabMenu ShowTabMenuEvent;
    public delegate void SkillChange(int idx);
    public event SkillChange SkillChangeEvent;

    public delegate void EnemyDead(string enemyName);
    public event EnemyDead EnemyDeadEvent;
    public override void _Ready(){
		Instance=this;
    }

    public void SkillChangeTrigger(int idx){
        SkillChangeEvent?.Invoke(idx);
    }

    public void EnemyDeadTrigger(string enemyName){
        EnemyDeadEvent?.Invoke(enemyName);
    }

    public void ShowTabMenuTrigger(){
        ShowTabMenuEvent?.Invoke();
    }

}
using Godot;

public partial class GlobalPlayerInventory:Node{
    public static GlobalPlayerInventory Instance { get; private set;}

    public override void _Ready(){
        Instance=this;
    }
    
}
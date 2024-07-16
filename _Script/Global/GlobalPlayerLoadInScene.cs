using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class GlobalPlayerLoadInScene : Node
{

	public static GlobalPlayerLoadInScene Instance;
    public override void _Ready()
    {
		Instance=this;
    }

    private void LoadSaveStatus(){
      
    }
}

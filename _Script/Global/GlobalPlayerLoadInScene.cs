using Godot;
using Godot.Collections;
using System;
using System.Runtime.CompilerServices;

public partial class GlobalPlayerLoadInScene : Node
{
	public static GlobalPlayerLoadInScene Instance;
    public override void _Ready()
    {
		Instance=this;
    }

    private void LoadSavedStatus(){
      if (!FileAccess.FileExists("user://savegame.save")){
          return;
      }
      using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);
      
    }

}

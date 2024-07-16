using Godot;
using Godot.Collections;

public partial class TestSaveLoad : Control
{
private string savePath = "user://save_game.json";

    public void SaveGame(){
		using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);

		var saveNodes = GetTree().GetNodesInGroup("Persist");
		foreach (Node saveNode in saveNodes)
		{
			if (string.IsNullOrEmpty(saveNode.SceneFilePath))
			{
				GD.Print($"persistent node '{saveNode.Name}' is not an instanced scene, skipped");
				continue;
			}

			if (!saveNode.HasMethod("Save")){
				GD.Print($"persistent node '{saveNode.Name}' is missing a Save() function, skipped");
				continue;
			}

			var nodeData = saveNode.Call("Save");
			var jsonString = Json.Stringify(nodeData);
			saveGame.StoreLine(jsonString);
		}
	}

   public void LoadGame()
{
    if (!FileAccess.FileExists("user://savegame.save"))
    {
        return;
    }

    // We need to revert the game state so we're not cloning objects during loading.
    // This will vary wildly depending on the needs of a project, so take care with
    // this step.
    // For our example, we will accomplish this by deleting saveable objects.
    var saveNodes = GetTree().GetNodesInGroup("Persist");
    foreach (Node saveNode in saveNodes)
    {
        saveNode.QueueFree();
    }

    // Load the file line by line and process that dictionary to restore the object
    // it represents.
    using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);

    while (saveGame.GetPosition() < saveGame.GetLength())
    {
        var jsonString = saveGame.GetLine();

        // Creates the helper class to interact with JSON
        var json = new Json();
        var parseResult = json.Parse(jsonString);
        if (parseResult != Error.Ok)
        {
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
            continue;
        }

        // Get the data from the JSON object
        var nodeData = new Dictionary<string, Variant>((Dictionary)json.Data);

        // Firstly, we need to create the object and add it to the tree and set its position.
        var newObjectScene = GD.Load<PackedScene>(nodeData["Filename"].ToString());
        var newObject = newObjectScene.Instantiate<Node>();
        GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
        newObject.Set(Node2D.PropertyName.Position, new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]));

        // Now we set the remaining variables.
        foreach (var (key, value) in nodeData)
        {
            if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY")
            {
                continue;
            }
            newObject.Set(key, value);
        }
    }
}

	public Dictionary<string, Variant> Save(){ //Return a dictionry
		return new Godot.Collections.Dictionary<string, Variant>()
		{
			{ "Filename", SceneFilePath },
			{ "Parent", GetParent().GetPath() },
			{ "PosX", Position.X }, // Vector2 is not supported by JSON
			{ "PosY", Position.Y },
			{ "Attack", 10 },
			{ "Defense", 10 },
			{ "CurrentHealth", 52 },
			{ "MaxHealth", 100 },
			{ "Damage", 3.2 },
			{ "Regen", 6 },
			{ "Experience", 909 },
			{ "Tnl", 100 },
			{ "Level", 20 },
			{ "AttackGrowth", 1 },
			{ "DefenseGrowth", 2 },
			{ "HealthGrowth", 10 },
			{ "IsAlive", true },
		};
	}

	private void OnSaveBtnPressed(){
		SaveGame();
	}

}

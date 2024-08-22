using Godot;
using System;
using GdUnit4;
using GdUnit4.Asserts;
[TestSuite]
public partial class CharacterBodyTests : Node{
	
	[TestCase]
	public void PlayerBodyInitializeNodeTest(){
		var runner=ISceneRunner.Load("res://_Scene/Player/Player.tscn");
		// GD.Print(runner.GetNode("Sprite2D"));


		// Assertions.AssertThat(player.GetNode("Sprite2D")).IsNotNull();
		// Assertions.AssertThat(player.GetNode("AnimationTree")).IsNotNull();
		// Assertions.AssertThat(player.GetNode("AnimationTree").Get("parameters/playback")).IsNotNull();
		// Assertions.AssertThat(player.GetNode("SkillNode")).IsNotNull();
		// Assertions.AssertThat(player.GetNode("Sprite2D/AttackArea")).IsNotNull();
		// Assertions.AssertThat(player.GetNode("Sprite2D/InteractableArea")).IsNotNull();
		// Assertions.AssertThat(player.GetNode("StatusManager")).IsNotNull();
	}

}

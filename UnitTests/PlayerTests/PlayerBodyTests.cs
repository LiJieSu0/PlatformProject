using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System.Threading.Tasks;
[TestSuite]
public partial class PlayerBodyTests : Node{

	[TestCase]
	public void PlayerBodyInitializeNodeTest(){
        ISceneRunner _sceneRunner = ISceneRunner.Load("res://_Scene/Player/Player.tscn");
		var player=_sceneRunner.FindChild("CharacterBody2D") as Node;
		AssertThat(player.GetNode("AnimationTree")).IsNotNull();
		AssertThat(player.GetNode("AnimationTree").Get("parameters/playback")).IsNotNull();
		AssertThat(player.GetNode("SkillNode")).IsNotNull();
		AssertThat(player.GetNode("Sprite2D/AttackArea")).IsNotNull();
		AssertThat(player.GetNode("Sprite2D/InteractableArea")).IsNotNull();
		AssertThat(player.GetParent().GetNode("StatusManager")).IsNotNull();
		AssertThat(player.GetParent().GetNode("HUD")).IsNotNull();
	}

	[TestCase]
	public async Task PlayerBodyRightMovementTest(){
        ISceneRunner _sceneRunner = ISceneRunner.Load("res://_Scene/Player/Player.tscn");
		var player=_sceneRunner.FindChild("CharacterBody2D") as PlayerBody;
		_sceneRunner.SimulateKeyPress(Key.D); 
		await _sceneRunner.AwaitMillis(1000);
		_sceneRunner.SimulateKeyRelease(Key.D);
		AssertFloat(player.Position.X).IsGreater(0);
	}
	[TestCase]
	public async Task PlayerBodyLeftMovementTest(){
        ISceneRunner _sceneRunner = ISceneRunner.Load("res://_Scene/Player/Player.tscn");
		var player=_sceneRunner.FindChild("CharacterBody2D") as PlayerBody;
		player.Position=new Vector2(0,player.Position.Y);
		_sceneRunner.SimulateKeyPress(Key.A); 
		await _sceneRunner.AwaitMillis(1000);
		AssertFloat(player.Position.X).IsLess(0);
	}

	[TestCase]
	public void PlayerBodyReceiveDamageTest(){
		ISceneRunner _sceneRunner = ISceneRunner.Load("res://_Scene/Player/Player.tscn");
		var player=_sceneRunner.FindChild("CharacterBody2D") as PlayerBody;
		player.ReceiveDamage(10f);
		GD.Print(player.statusManager.CurrHp);
		AssertFloat(player.statusManager.CurrHp).IsGreater(0); //status manager didn't load player status
	}
}

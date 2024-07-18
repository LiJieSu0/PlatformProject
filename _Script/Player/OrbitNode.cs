using Godot;
using System;

public partial class OrbitNode : Node2D
{
    private Node2D player;
    private Node2D projectileSpawnPoint;
    [Export]private float orbitDistance = 20f;
    private float orbitAngle = 0f;
	public override void _Ready()
	{
		player =(Node2D)GetParent().GetParent();
        projectileSpawnPoint =(Node2D)GetParent();
	}
	public override void _Process(double delta){
        Vector2 mousePosition = GetGlobalMousePosition();
        Vector2 directionToMouse = (mousePosition - player.GlobalPosition).Normalized();
        orbitAngle = Mathf.Atan2(directionToMouse.Y, directionToMouse.X);
        Vector2 orbitPosition = player.GlobalPosition + new Vector2(Mathf.Cos(orbitAngle), Mathf.Sin(orbitAngle)) * orbitDistance;
        projectileSpawnPoint.GlobalPosition = orbitPosition;
	}
}

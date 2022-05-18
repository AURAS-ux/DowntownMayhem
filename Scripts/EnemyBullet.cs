using Godot;
using System;

public class EnemyBullet : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Translate(new Vector2(0, 150*delta));
    }

    public void HitPlayer(Player player)
    {
        if (player.Name == "Player")
        {
            player.isHit = true;
        }
    }

    public void HitBullet(Bullet bullet)
    {
        bullet.QueueFree();
        this.QueueFree();
    }
}

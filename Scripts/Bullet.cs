using Godot;
using System;

public class Bullet : Area2D
{
    [Export] int bulletSpeedMultiplyer = 20;
    public override void _Ready()
    {

    }

    public override void _PhysicsProcess(float delta)
    {
        this.Position += new Vector2(0,-bulletSpeedMultiplyer*delta);
    }
    public void BulletLeftScreen()
    {
        QueueFree();
    }
    public void BulletHit(Enemy hit)
    {
        hit.QueueFree();
        this.QueueFree();
        hit.SetHitStatus(true);
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

using Godot;
using System.Collections.Generic;

public class Enemy : KinematicBody2D
{
    [Export]int _speed=20;
    int direction;
    Vector2 currentPos;
    bool isHit;
    bool allowShooting;

    private PackedScene enemybullet;
    public override void _Ready()
    {
        direction = 1;
        currentPos = this.Position;
        isHit = false;
        enemybullet = GD.Load<PackedScene>("res://Prefabs/EnemyBullet.tscn");
        allowShooting = false;
    }
    

    public void ClearEnemyBullets()
    {
        for(int i =1;i< GetParent().GetChildren().Count;i++)
        {
            this.GetParent().GetChild(i).QueueFree();
        }
    }
    void EnemyShoot()
    {
        EnemyBullet bullet = enemybullet.Instance<EnemyBullet>();
        this.GetParent().AddChild(bullet);
        bullet.Position = new Vector2(this.GlobalPosition.x, this.GlobalPosition.y + 20.5f);
    }
    
    public void SetSpeed(int speed)
    {
        _speed = speed; 
    }
    public void SetHitStatus(bool status)
    {
        if (isHit != status)
            isHit = status;
        else GD.Print("Object status is already"+status);
    }

    public bool GetHitStatus() { return isHit; }
    public int GetSpeed()
    {
        return this._speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveEnemy(delta);
        //GD.Print(this.GetParent().GetChildren());
    }

    private void MoveEnemy(float delta)
    {
        if (IsOnWall())
        {
            direction *= -1;
            currentPos.y += 200;
            this.MoveAndSlide(new Vector2(0, currentPos.y), Vector2.Up);
        }

        this.MoveAndSlide(new Vector2(-100 * _speed * delta * direction, 0), Vector2.Up);
    }

  

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (allowShooting)
        {
            EnemyShoot();
            allowShooting = false;
        }

    }

    public void _on_Timer_timeout()
    {
        allowShooting = true;
    }
}

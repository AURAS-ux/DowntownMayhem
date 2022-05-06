using Godot;
using System;

public class Enemy : KinematicBody2D
{
    [Export]int _speed=20;
    int direction;
    Vector2 currentPos;
    bool isHit;
    public override void _Ready()
    {
        direction = 1;
        currentPos = this.Position;
        isHit = false;
    }

    public void SetHitStatus(bool status)
    {
        if (isHit != status)
            isHit = status;
        else GD.Print("Object status is already"+status);
    }

    public bool GetHitStatus() { return isHit; }
    public void PrintHit() { GD.Print(this.isHit); }

    public override void _PhysicsProcess(float delta)
    {
        MoveEnemy(delta);
    }

    private void MoveEnemy(float delta)
    {
        if (IsOnWall())
        {
            direction *= -1;
            currentPos.y += 1000;
            this.MoveAndSlide(new Vector2(0, currentPos.y), Vector2.Up);
        }

        this.MoveAndSlide(new Vector2(-100 * _speed * delta * direction, 0), Vector2.Up);
    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

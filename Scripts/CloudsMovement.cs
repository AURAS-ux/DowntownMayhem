using Godot;
using System;
using System.Collections.Generic;
public class CloudsMovement : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Random random;
    List<int> directions;
    int direction;
    // Called when the
    // node enters the scene tree for the first time.
    public override void _Ready()
    {
        random = new Random();
        directions = new List<int>() { -1, 1 };
        direction =  GetRandomDirection();
    }

    private int GetRandomDirection()
    {
        return directions[random.Next(directions.Count)];
    }

    private int GetRandomSpeed()
    {
        return random.Next(2, 100);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        this.Translate(new Vector2(GetRandomSpeed() * delta*direction, 0));
        if(this.Position.x > 1100f)
        {
            this.Position = new Vector2(0, this.Position.y);
        }else if(this.Position.x < -75)
        {
            this.Position = new Vector2(1100, this.Position.y);
        }
    }
}

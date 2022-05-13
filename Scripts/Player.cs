using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] int _moveSpeed = 20;
    private Vector2 _velocity;
    private PackedScene _bullet;
    private Position2D _positionShooting;
    private int _reloadTime;
    
    public override void _Ready()
    {
        _bullet = GD.Load<PackedScene>("res://Prefabs/Bullet.tscn");
        _positionShooting = GetNode<Position2D>("/root/MainLevel/Player/ShootPos");
        _reloadTime = 0;
    }

    public override void _PhysicsProcess(float delta)
    { 
        _velocity = MoveAndSlide(ProcessInput(delta));
    }


    private Vector2 ProcessInput(float delta)
    {
        _velocity = Vector2.Zero;
        if (Input.IsActionPressed("left"))
            _velocity += new Vector2(-10 * _moveSpeed * delta, 0);
        if (Input.IsActionPressed("right"))
            _velocity += new Vector2(10 * _moveSpeed * delta, 0);
        if (Input.IsActionPressed("Shoot") && _reloadTime > 60)
        {
            Shoot();
            _reloadTime = 0;
        }
        else _reloadTime++;
        //if (Input.IsActionPressed("up"))
        //    _velocity += new Vector2(0, -10 * _moveSpeed * delta);
        //if (Input.IsActionPressed("down"))
        //    _velocity += new Vector2(0, 10 * _moveSpeed * delta);
        return _velocity;
    }
    private void Shoot()
    {
        Bullet bullet = _bullet.Instance<Bullet>();
        Owner.GetParent().AddChild(bullet);
        bullet.Transform = _positionShooting.GlobalTransform;
        bullet.Scale = new Vector2(0.85f, 2);
        
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

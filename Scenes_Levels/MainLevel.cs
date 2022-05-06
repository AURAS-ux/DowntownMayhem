using Godot;
using System;
using System.Collections.Generic;

public class MainLevel : Node2D
{
    private Position2D _enemySpawnPos;
    private PackedScene _enemy;
    public Timer _timer;
    [Export] int enemyCount = 6;
    private int count;
    private List<Enemy> _enemies;
    private int score;
    public RichTextLabel scoreBoard;
    public override void _Ready()
    {
        _initVars();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        CheckForInput();
        ConfigSpawnTime();
        SetScore();
        scoreBoard.Text =$"Score:{score}";
    }

    private void SetScore()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].GetHitStatus() == true)
            {
                _enemies.RemoveAt(i);
                score += 100;
            }
        }
    }

    private void _initVars()
    {
        count = 0;
        _enemySpawnPos = GetNode<Position2D>("/root/MainLevel/EnemieSpawn");
        _enemy = GD.Load<PackedScene>("res://Prefabs/Enemy.tscn");
        _timer = GetNode<Timer>("/root/MainLevel/EnemieSpawn/Timer");
        _enemies = new List<Enemy>();
        score = 0;
        scoreBoard = GetNode<RichTextLabel>("/root/MainLevel/Background/Label");
    }

    public void IncreaseScore()
    {
        score += 200;
    }

    private void SpawnEnemies()
    { 
        var enemy = _enemy.Instance();
        _enemySpawnPos.AddChild(enemy);
        _enemies.Add((Enemy)enemy);
    }

    public void _on_Timer_timeout()
    {
        SpawnEnemies();
    }
    
    private void CheckForInput()
    {
        if (Input.IsKeyPressed((int)KeyList.Escape))
        {
            this.GetTree().Quit();
        }
    }
    private void ConfigSpawnTime()
    {
        count = _enemies.Count;
        if(count<enemyCount-1)
        {
            _timer.OneShot = false;
        }
        else
        {
            _timer.OneShot = true ;
        }
        //GD.Print(count);
    }
}

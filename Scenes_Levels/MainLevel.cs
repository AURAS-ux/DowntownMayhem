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
    private int _score;
    public RichTextLabel _scoreBoard;
    public PackedScene _youLostScene;
    private Random _randomPos;

    public void LeftTree()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.QueueFree();
            enemy.ClearEnemyBullets();
        }
    }
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
        _scoreBoard.Text =$"Score:{_score}";
        SetDifficulty();
    }
    public void CheckLost(Enemy collision)
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.QueueFree();
        }
        GetTree().ChangeSceneTo(_youLostScene);
    }

    private void SetScore()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
           // GD.Print($"Enemy{i} with children {_enemies[i].GetChildren()}");
            if (_enemies[i].GetHitStatus() == true)
            {
                _enemies.RemoveAt(i);
                IncreaseScore();
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
        _score = 0;
        _scoreBoard = GetNode<RichTextLabel>("/root/MainLevel/Background/Label");
        _youLostScene = GD.Load<PackedScene>("res://Scenes_Levels/YouLostScreen.tscn");
        _randomPos = new Random();
    }

    public void IncreaseScore()
    {
        _score += 200;
    }

    private void SetDifficulty()
    {
        if(_score >= 500)
        {
            for (int i = 0; i < _enemies.Count; i++)
                _enemies[i].SetSpeed(100) ;
        }
    }
    private void SpawnEnemies()
    {
        Enemy enemy = _enemy.Instance<Enemy>();
        this.GetTree().Root.AddChild(enemy);
        enemy.Position = new Vector2(_randomPos
            .Next(0, (int)_enemySpawnPos.Position.x),
            _enemySpawnPos.Position.y);
        _enemies.Add(enemy);
        
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
        if (count <= enemyCount-1)
        {
            _timer.Paused = false;
        }
        else
        {
            _timer.Paused = true;
        }
    }
}

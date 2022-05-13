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
    public PackedScene youLostScene;
    private Random randomPos;
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
        SetDifficulty();
        CheckLost();
    }

    private void CheckLost()
    {
        foreach(Enemy enemy in _enemies)
        {
            if (enemy.ReachedEnd())
            {
                GetTree().ChangeSceneTo(youLostScene);
            }
        }
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
        score = 0;
        scoreBoard = GetNode<RichTextLabel>("/root/MainLevel/Background/Label");
        youLostScene = GD.Load<PackedScene>("res://Scenes_Levels/YouLostScreen.tscn");
        randomPos = new Random();
    }

    public void IncreaseScore()
    {
        score += 200;
    }

    private void SetDifficulty()
    {
        if(score >= 500)
        {
            for (int i = 0; i < _enemies.Count; i++)
                _enemies[i].SetSpeed(500);
        }
    }
    private void SpawnEnemies()
    { 
        Enemy enemy = _enemy.Instance<Enemy>();
        this.GetTree().Root.AddChild(enemy);
        enemy.Position = new Vector2(randomPos
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

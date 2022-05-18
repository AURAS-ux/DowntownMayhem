using Godot;
using System;
public class YouLostScreen : Control
{
    public override void _Ready()
    {

    }
    public void RetryButtonPressed()
    {
        GetTree().ChangeScene("res://Scenes_Levels/MainLevel.tscn");
    }

    public void QuitButtonPressed()
    {
        GetTree().Quit();
    }
}

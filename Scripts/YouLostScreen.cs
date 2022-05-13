using Godot;
using System;

public class YouLostScreen : Control
{
   
    public void RetryButtonPressed()
    {
        GetTree().ChangeScene("res://Scenes_Levels/MainLevel.tscn");
    }

    public void QuitButtonPressed()
    {
        GetTree().Quit();
    }
}

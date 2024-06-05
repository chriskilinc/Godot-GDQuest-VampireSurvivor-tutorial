using Godot;
using System;

public partial class game : Node2D
{
    PathFollow2D pathFollow;
    PackedScene slime;
    CanvasLayer _gameOver;
    public override void _Ready()
    {
        pathFollow = GetNode<PathFollow2D>("%PathFollow2D");
        slime = GD.Load<PackedScene>("res://slime.tscn");
        _gameOver = GetNode<CanvasLayer>("%GameOver");
    }

    public void SpawnMob()
    {
        var newSlime = slime.Instantiate<slime>();
        pathFollow.ProgressRatio = GD.Randf();
        newSlime.GlobalPosition = pathFollow.GlobalPosition;
        AddChild(newSlime);
    }

    public void OnTimerTimeout()
    {
        SpawnMob();
    }

    public void OnPlayerHealthDepleted()
    {
        _gameOver.Visible = true;
        GetTree().Paused = true;
    }
}

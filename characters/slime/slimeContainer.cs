using Godot;
using System;

public partial class slimeContainer : Node2D
{
    AnimationPlayer AnimationPlayer;
    public override void _Ready()
    {
         AnimationPlayer= GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void PlayWalk()
    {
        AnimationPlayer.Play("walk");
    }

    public void PlayHurt()
    {
        AnimationPlayer.Play("hurt");
        AnimationPlayer.Queue("walk");
    }
}

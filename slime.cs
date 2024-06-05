using Godot;
using System;

public partial class slime : CharacterBody2D
{
    player player;
    slimeContainer slimeContainer;
    PackedScene smokeExplosion = GD.Load<PackedScene>("res://smoke_explosion/smoke_explosion.tscn");

    float health = 3;


    public override void _Ready()
    {
        player = GetNode<player>("/root/Game/Player");
        slimeContainer = GetNode<slimeContainer>("%SlimeContainer");
        if (slimeContainer.HasMethod("PlayWalk"))
        {
            slimeContainer.PlayWalk();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var direction = GlobalPosition.DirectionTo(player.GlobalPosition);
        Velocity = direction * 150;
        MoveAndSlide();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (slimeContainer.HasMethod("PlayHurt")) // If the slime object has a TakeDamage method
        {
            slimeContainer.PlayHurt(); // Call the TakeDamage method on the slime object
        }

        if (health <= 0)
        {
            var smoke = smokeExplosion.Instantiate() as Node2D;
            GetParent().AddChild(smoke);
            smoke.GlobalPosition = GlobalPosition;  // Set the smoke's position to the slime's position when it dies
            QueueFree();
        }
    }

}

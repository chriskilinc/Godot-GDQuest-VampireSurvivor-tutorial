using Godot;
using System;

public partial class player : CharacterBody2D
{
    double health = 100.0f;
    float movementSpeed = 600;

    Area2D _hurtBox;
    ProgressBar _healthBar;

    [Signal]
    public delegate void HealthDepletedEventHandler();

    public override void _Ready()
    {
        _hurtBox = GetNode<Area2D>("%HurtBox");
        _healthBar = GetNode<ProgressBar>("%HealthBar");
        _healthBar.MaxValue = (float)health;

    }

    public override void _PhysicsProcess(double delta)
    {
        var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = direction * movementSpeed;
        MoveAndSlide();

        if (direction != Vector2.Zero)
        {
            GetNode("HappyBoo").Call("play_walk_animation");
        }
        else
        {
            GetNode("HappyBoo").Call("play_idle_animation");
        }

        var damageRate = 10.0;
        var overlapping_mobs = _hurtBox.GetOverlappingBodies();

        if (overlapping_mobs.Count > 0)
        {
            health -= damageRate * delta;
            _healthBar.Value = (float)health;

            if (health <= 0)
            {
                EmitSignal(SignalName.HealthDepleted);
                GD.Print("You died!");
            }
        }

    }
}

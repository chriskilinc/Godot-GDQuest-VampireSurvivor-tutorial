using Godot;
using System;

public partial class Bullet : Area2D
{
    float bulletSpeed = 500;
    float travelDistance = 0;
    float maxRange = 1200;
    public override void _Ready()
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector2.Right.Rotated(Rotation); // Get current direction of where the bullet is pointing
        Position += direction * bulletSpeed * (float)delta; // Move the bullet in the direction it is pointing
        travelDistance += bulletSpeed * (float)delta; // Keep track of how far the bullet has traveled

        if (travelDistance >= maxRange) // If the bullet has traveled too far
        {
            QueueFree(); // Destroy the bullet
        }
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is slime) // If the bullet hits a slime
        {
            var slime = body as slime; // Cast the body to a slime object
            if (slime.HasMethod("TakeDamage")) // If the slime object has a TakeDamage method
            {
                slime.TakeDamage(1); // Call the TakeDamage method on the slime object
            }
            QueueFree(); // Destroy the bullet
        }
    }
}

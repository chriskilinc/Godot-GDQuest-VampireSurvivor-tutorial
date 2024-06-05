using Godot;
using System;
using System.Linq;

public partial class gun : Area2D
{
    PackedScene bullet = GD.Load<PackedScene>("res://bullet.tscn");
    Marker2D shootingPoint;
    Node2D target;

    // float shotsFired = 0;

    public override void _Ready()
    {
        shootingPoint = GetNode<Marker2D>("%ShootingPoint");
        // GD.Print(shootingPoint);
    }

    public override void _PhysicsProcess(double delta)
    {
        // target enemies in range and closest to the gun
        var enemiesInRange = GetOverlappingBodies();
        if (enemiesInRange.Count > 0)
        {

            // Shoot at the closest enemy for 3 shots, then switch to the next closest enemy 
            // if (shotsFired == 3)
            // {
            //     var orderedEnemies = enemiesInRange.OrderBy(enemy => GlobalPosition.DistanceSquaredTo(enemy.GlobalPosition)).ToArray();
            //     target = orderedEnemies[0];
            //     shotsFired = 0;
            // }
            // else
            // {
            //     target = enemiesInRange[0];
            // }

            var target = enemiesInRange[0];
            LookAt(target.GlobalPosition);
        }
    }

    public void OnTimerTimeout()
    {
        // Only shoot enemies in range
        var enemiesInRange = GetOverlappingBodies();
        if (enemiesInRange.Count > 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // shotsFired++;
        var newBullet = bullet.Instantiate() as Bullet;
        newBullet.GlobalPosition = shootingPoint.GlobalPosition;
        newBullet.GlobalRotation = GlobalRotation;
        shootingPoint.AddChild(newBullet);
    }
}

using UnityEditor;
using UnityEngine;

public class Minigun : Powerups
{
    float oldCooldown;
    public override void EndPowerup(TankShooting tank)
    {
        tank.shootCooldown = oldCooldown;
        Debug.Log("Powerup ended");
    }
    public override void StartPowerup(TankShooting tank)
    {
        oldCooldown = tank.shootCooldown;
        tank.shootCooldown = 0.1f;
    }
}

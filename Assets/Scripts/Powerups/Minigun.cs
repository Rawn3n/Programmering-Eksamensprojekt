using UnityEditor;
using UnityEngine;

public class Minigun : Powerups
{
    float oldCooldown;
    public override void EndPowerup(TankShooting tank)
    {
        isPowerupActive = false;
        tank.shootCooldown = oldCooldown;
    }
    public override void StartPowerup(TankShooting tank)
    {
        oldCooldown = tank.shootCooldown;
        tank.shootCooldown = 0.1f;
    }
    
}

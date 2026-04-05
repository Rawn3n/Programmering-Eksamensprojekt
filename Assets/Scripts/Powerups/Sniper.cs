using UnityEngine;

public class Sniper : Powerups
{
    [SerializeField] private float scaleFactor = 5f;
    public override void StartPowerup(TankShooting tank)
    {
        tank.bulletScale *= scaleFactor;
    }
    public override void EndPowerup(TankShooting tank)
    {
        tank.bulletScale /= scaleFactor;
    }

}

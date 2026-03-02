using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TankShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    float fireCooldown;
    public float shootCooldown = 5f;

    float lastShotTime;

    public event Action<float> OnTankShoot;

    public void OnShoot(InputAction.CallbackContext context)
    {
        fireCooldown = shootCooldown;
        if (!context.performed) return;

        if (Time.time < lastShotTime + fireCooldown) return;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<Bullet>().SetShooter(gameObject);

        lastShotTime = Time.time;

        //udsender event
        OnTankShoot?.Invoke(fireCooldown);
    }

    public void ActivatePowerup(Powerups powerup, float duration)
    {
        powerup.StartPowerup(this);
        StartCoroutine(PowerupTimer(powerup, duration));
    }

    private IEnumerator PowerupTimer(Powerups powerup, float duration)
    {
        yield return new WaitForSeconds(duration);
        powerup.EndPowerup(this);
    }
}

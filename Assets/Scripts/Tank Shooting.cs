using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] float fireCooldown = 0.5f;

    float lastShotTime;

    public event Action<float> OnTankShoot;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Time.time < lastShotTime + fireCooldown) return;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<Bullet>().SetShooter(gameObject);

        lastShotTime = Time.time;

        //udsender event
        OnTankShoot?.Invoke(fireCooldown);
    }
}

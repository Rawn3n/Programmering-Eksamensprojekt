using UnityEngine;
using UnityEngine.InputSystem;

public class TankShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] float fireCooldown = 0.5f;

    float lastShotTime;

    public void OnShoot(InputValue value)
    {
        if (!value.isPressed) return;
        if (Time.time < lastShotTime + fireCooldown) return;

        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        lastShotTime = Time.time;
    }
}

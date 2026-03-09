using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;


public abstract class Powerups : MonoBehaviour
{
    [SerializeField] protected float duration = 10f;
    [SerializeField] private GameObject currentTank;
    [SerializeField] protected Sprite powerupSprite;

    public abstract void EndPowerup(TankShooting tank);
    public abstract void StartPowerup(TankShooting tank);

    
    public Sprite GetSprite()
    {
        return powerupSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            TankShooting tank = collision.gameObject.GetComponent<TankShooting>();
            tank.ActivatePowerup(this, duration);
            Destroy(gameObject);
        }
    }
}

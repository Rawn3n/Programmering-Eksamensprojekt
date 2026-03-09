using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthSystem : MonoBehaviour, IDamageAble
{
    [SerializeField] float startHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = startHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}

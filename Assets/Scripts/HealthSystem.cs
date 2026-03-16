using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthSystem : MonoBehaviour, IDamageAble
{
    [SerializeField] float startHealth = 100f;
    private float currentHealth;
    [SerializeField] private GameObject deathParticlesPrefab;
    [SerializeField] public bool canTakeDamage = true;

    void Start()
    {
        currentHealth = startHealth;
    }
    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
        {
            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        GameManager.Instance.PlayerDied();
        GameManager.Instance.playerList.Remove(gameObject);

        Destroy(gameObject);
    }
}

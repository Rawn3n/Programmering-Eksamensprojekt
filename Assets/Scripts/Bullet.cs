using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float lifetime = 3f;
    [SerializeField] LayerMask bounceLayers;
    GameObject shooter;
    TankShooting shooterScript;

    public int Damage = 1;

    Rigidbody2D rb;
    Vector2 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == shooter) //sæt timer her hvis vi vil have at den skal kunne ramme tanken efter et stykke tid
            return;

        if (((1 << collision.gameObject.layer) & bounceLayers) != 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflected = Vector2.Reflect(lastVelocity.normalized, normal);
            rb.linearVelocity = reflected * speed;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // TakeDamage(Damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetShooter(GameObject shooterObj)
    {
        shooter = shooterObj;
        shooterScript = shooter.GetComponent<TankShooting>();
    }

}


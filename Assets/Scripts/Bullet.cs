using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float lifetime = 3f;
    [SerializeField] LayerMask bounceLayers;
    GameObject shooter;
    TankShooting shooterScript;

    public float damage = 100;
    private float delayTime = 0.1f;
    private bool canDamageShooter = false;

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
        Invoke("AllowDamageToShooter", delayTime);
    }

    void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == shooter && !canDamageShooter)
        {
            return;
        }

        if (((1 << collision.gameObject.layer) & bounceLayers) != 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflected = Vector2.Reflect(lastVelocity.normalized, normal);
            rb.linearVelocity = reflected * speed;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamageAble>()?.TakeDamage(damage);
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

    private void AllowDamageToShooter()
    {
        canDamageShooter = true;
    }
}
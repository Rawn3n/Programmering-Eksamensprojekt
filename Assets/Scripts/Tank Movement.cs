using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform shootPoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // kaldt af send message fra Input System
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnShoot(InputValue value)
    {
        if (!value.isPressed) return;
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }

    void FixedUpdate()
    {
        Vector2 movement = (Vector2)transform.up * moveInput.y + (Vector2)transform.right * moveInput.x;

        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}

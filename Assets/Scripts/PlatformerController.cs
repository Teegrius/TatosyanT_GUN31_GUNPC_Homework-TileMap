using UnityEngine;

public class SimplePlatformerJump : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 7f;
    public float jumpForce = 13f; // Увеличил силу прыжка

    [Header("Ground Check")]
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Если не назначили слой, используем Default
        if (groundLayer.value == 0) groundLayer = LayerMask.GetMask("Default");
    }

    void Update()
    {
        // ПРОСТАЯ проверка земли через луч вниз
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );
        isGrounded = hit.collider != null;

        // Движение влево/вправо
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * walkSpeed, rb.velocity.y);

        // ПРЫЖОК по Space или W или UpArrow
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("JUMP! Grounded: " + isGrounded);
        }

        // Визуализация в редакторе
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance,
                     isGrounded ? Color.green : Color.red);
    }
}
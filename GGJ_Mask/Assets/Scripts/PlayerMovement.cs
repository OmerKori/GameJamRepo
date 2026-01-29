using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float deceleration = 60f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;

    private Rigidbody2D rb;
    private bool isGrounded;

    private float inputX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Read input in Update (best practice)
        inputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move(inputX);
    }

    private void Move(float x)
    {
        float targetVelX = x * maxSpeed;

        // If player is pressing a direction, accelerate; otherwise decelerate to 0.
        float smooth = Mathf.Abs(x) > 0.01f ? acceleration : deceleration;

        float newVelX = Mathf.MoveTowards(rb.linearVelocity.x, targetVelX, smooth * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(newVelX, rb.linearVelocity.y);
    }

    private void Jump()
    {
        // Optional: remove downward velocity so jump is consistent
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = false;
    }
}

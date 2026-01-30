using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float Speed = 8f;
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float decceleration = 30f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;

    [SerializeField] private float ladderSpeed = 1;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isLaddered;

    private float inputX;
    private bool SpacePressed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Read input in Update (best practice)
        inputX = Input.GetAxisRaw("Horizontal");
        SpacePressed = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(inputX, 0) * Speed * acceleration);
        if (SpacePressed)
        {
            print(isGrounded);
            if (isLaddered)
                rb.AddForce(new Vector2(0, 1) * ladderSpeed);
            else if (isGrounded)
                rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (transform.position.y>collision.transform.position.y)
                isGrounded = true;
        }
            
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            isLaddered = true;
        
           
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = false;
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            isLaddered = false;
    }
}

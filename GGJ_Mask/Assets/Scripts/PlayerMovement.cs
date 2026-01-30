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
        if(isGrounded)
            rb.AddForce(acceleration * Speed * new Vector2(inputX, 0));
        else
            rb.AddForce(acceleration * Speed * 2/3 * new Vector2(inputX, 0));
     
        if (isLaddered)
        {
            if (SpacePressed)
                rb.linearVelocityY = ladderSpeed;
            else 
                rb.linearVelocityY = 0;
        }
        else if(isGrounded && SpacePressed)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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

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

    private Animator playerAnimator;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isLaddered;

    private float inputX;
    private bool WPressed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        WPressed = Input.GetKey(KeyCode.W);
    }

    private void FixedUpdate()
    {
        if (inputX != 0)
            transform.rotation = Quaternion.Euler(0, inputX * 180f, 0);
        
        //Fix Flipping 

        if (isGrounded)
        {
            rb.AddForce(acceleration * Speed * new Vector2(inputX, 0));
            playerAnimator.SetTrigger("Walk");
        }
        else
        {
            rb.AddForce(acceleration * Speed * 2 / 3 * new Vector2(inputX, 0));
            playerAnimator.SetTrigger("Walk");
        }

        //When Not Moving: playerAnimator.SetTrigger("Idle");

        if (isLaddered)
        {
            if (WPressed)
                rb.linearVelocityY = ladderSpeed;
            else
                rb.linearVelocityY = 0;
        }
        else if (isGrounded && WPressed)
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

 

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            isLaddered = true;
       
        if (collision.gameObject.layer == LayerMask.NameToLayer("EndDoor"))
        {
            GameManager.LoadNextLevel();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            isLaddered = false;
    }
}

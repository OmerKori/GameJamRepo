using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float Speed = 8f;
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float decceleration = 30f;
    [SerializeField] private float risingGravityScale = 1.3f;
    [SerializeField] private float fallingGravityScale = 1.9f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;

    [SerializeField] private float ladderSpeed = 1;

    private Animator playerAnimator;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isLaddered;
    private bool isRight;
    private bool isIdle = true;

    private float inputX;
    private bool WPressed;
    private bool UpPressed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        WPressed = Input.GetKey(KeyCode.W);
        UpPressed = Input.GetKey(KeyCode.UpArrow);
    }

    private void FixedUpdate()
    {
        rb.gravityScale = rb.linearVelocityY > 0 ? risingGravityScale : fallingGravityScale;

        if (transform.position.y < -10)
        {
            FindObjectOfType<LevelManager>().ResetLevel();
        }

        if (inputX < 0 && !isRight)
        {
            isRight = true;
            flipSprite();
        }
        else if (inputX > 0 && isRight)
        {
            isRight = false;
            flipSprite();
        }

        if (isGrounded)
        {
            rb.AddForce(acceleration * Speed * new Vector2(inputX, 0));
        }
        else
        {
            rb.AddForce(acceleration * Speed * 2 / 3 * new Vector2(inputX, 0));
        }

        //When Not Moving: playerAnimator.SetTrigger("Idle");
        if (rb.linearVelocity == Vector2.zero && !isIdle)
        {
            isIdle = true;
            playerAnimator.SetTrigger("Idle");
        }
        else if (rb.linearVelocity != Vector2.zero && isIdle)
        {
            isIdle = false;
            playerAnimator.SetTrigger("Walk");
        }

        if (isLaddered)
        {
            if (WPressed || UpPressed)
                rb.linearVelocityY = ladderSpeed;
            else
                rb.linearVelocityY = 0;
        }
        else if (isGrounded && WPressed || isGrounded && UpPressed)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (transform.position.y > collision.transform.position.y +transform.localScale.y / 2)
                isGrounded = true;
        }
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Mask"))
        {
            if(collision.collider.transform.Find("JumpBelow")!=null)
                if (collision.collider.transform.Find("JumpBelow").position.y < transform.position.y)
                {
                    isGrounded = true;
                }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = false;

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Mask"))
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
        if (collision.gameObject.name == "AllowJump")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            isLaddered = false;
        if (collision.gameObject.name == "AllowJump")
        {
            isGrounded = false;
        }
    }

    private void flipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}

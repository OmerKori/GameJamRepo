using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpForce = 7.0f;
    [SerializeField] float ladderClimbSpeed = 3.0f;
    
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    
    public bool isGrounded = true;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
       
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(moveHorizontal * speed, jumpForce));
            isGrounded = false;
        }
        else
            rb.AddForce(new Vector2(moveHorizontal * speed, 0));

        if (moveHorizontal > 0)
            spriteRenderer.flipX = false;
        else if (moveHorizontal < 0)
            spriteRenderer.flipX = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

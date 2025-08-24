using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; //player body

    public float moveSpeed;
    
    public bool isGrounded;
    
    public float horizontalMove; //float for input in which direction player goes
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMove * moveSpeed, rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x; 
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //let player jump if not on ground
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GROUNDED");
            isGrounded = true;
        }
    }
}

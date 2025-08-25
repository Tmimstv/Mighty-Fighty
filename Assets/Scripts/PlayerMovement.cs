using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb; //player body
    public float moveSpeed;
    public bool isGrounded;
    public float horizontalMove; //float for input in which direction player goes
    
    //get the animators properties?
    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMove * moveSpeed, rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x; 
        animator.SetFloat("Speed", horizontalMove);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //let player jump if not on ground
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * 10f, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GROUNDED");
            isGrounded = true;
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Light");
        }
    }
}

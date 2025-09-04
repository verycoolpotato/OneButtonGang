using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float JumpHeight;

    [SerializeField] LayerMask GroundedLayers;
    
    private int _dir = 1;
    private int _jumps = 2;

    private void Start()
    {
       
    }

    private void FixedUpdate()
    {
        AutoRun(_dir);
    }

    private void Update()
    {
      

        
    }

    //Automatic Movement
    private void AutoRun(int direction)
    {
        Rb.linearVelocityX = MovementSpeed * _dir;
    }

    //Call to reverse the players movement direction
    public void FlipDirection()
    {
        bool direction = true;
        direction = !direction;

        _dir = direction ? 1 : -1;
    }

    //Jump
    private void Jump()
    {

        GroundedCheck();
        if (_jumps > 0)
        {
            Rb.linearVelocity = Vector2.zero;
            Rb.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);

            _jumps--;
        }
       
    }

    private void GroundedCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 0.5f, GroundedLayers);
       bool isGrounded = hit.collider != null;
       

       if(isGrounded)
        {
            _jumps = 2;
        }
    }

    

    public void OnHold(InputAction.CallbackContext context)
    {
        float heldTime;

        if (context.performed) // button pressed
        {
            
        }
        else if (context.canceled) // button released
        {
          
        }

        heldTime = 0; 
    }

    private void CheckInputType(float time)
    {
        if(time < 0.2f)
        {
            Jump();
        }
    }

}

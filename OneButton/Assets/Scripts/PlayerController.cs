using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float JumpHeight;

   

    public enum Directions { Left = -1, Right = 1 };

    [Tooltip("The direction of movement")]
    public Directions Direction = Directions.Right;

    [Tooltip("What layers are detected as Ground?")]
    [SerializeField] LayerMask GroundedLayers;

  

    private int _jumps = 2;

    //For input timings
    private float _heldTime = 0;
    private bool _startHold = false;

   

    private void FixedUpdate()
    {
        AutoRun((int)Direction);
    }

    private void Update()
    {
        

       //Times input
        if (_startHold)
        {
            _heldTime += Time.deltaTime;
        }
    }

    //Automatic Movement
    private void AutoRun(int direction)
    {
        Rb.linearVelocityX = MovementSpeed * (int)Direction;
    }

    //Call to reverse the players movement direction
    public void FlipDirection()
    {
        bool direction = true;
        direction = !direction;

        Direction = direction ? Directions.Right : Directions.Left;
    }

    //Jump
    private void Jump()
    {
        if (Grounded())
            _jumps = 2;

        if (_jumps > 0)
        {
            Rb.linearVelocity = Vector2.zero;
            Rb.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);

            _jumps--;
        }
    }

    //check if on ground, returns whether grounded or not
    private bool Grounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 0.5f, GroundedLayers);
       bool isGrounded = hit.collider != null;

       return isGrounded;
    }

    
    //Called when spacebar is pressed, checks how long it is held for
    public void OnHold(InputAction.CallbackContext context)
    { 
        //NOTE, WE CAN ADD AN ADDITIONAL HOLD BEHAVIOUR WHILE IN THE AIR

        //Improves jump responsiveness while in air
        if (!Grounded()) 
        {
            Jump();
        }
        else if (context.performed) //pressed
        {
            _startHold = true;
        }
        else if (context.canceled) //released
        {
            CheckInputType(_heldTime);
            _startHold = false;
            _heldTime = 0;
        }
    }

    //called when space is released, determines what action was taken
    private void CheckInputType(float time)
    {
        if(time < 0.2f)
        {
            Jump();
        }
        else if( time > 1)
        {
            Debug.Log("swing");
        }
    }
}

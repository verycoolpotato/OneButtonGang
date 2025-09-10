using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : PlayerDestruction
{
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float JumpHeight;

    [Tooltip("Launches diagonally")]
    [SerializeField] private float SwingKnockback;

    [Tooltip("This Animator")]
    [SerializeField] Animator PlayerAnimator;
    public enum Directions { Left = -1, Right = 1 };

    [Tooltip("The direction of movement")]
    public Directions Direction = Directions.Right;

    [Tooltip("What layers are detected as Ground?")]
    [SerializeField] LayerMask GroundedLayers;

    [Tooltip("What layers are Breakble?")]
    [SerializeField] LayerMask BreakableLayers;

    [SerializeField] private float SlamRadius;

    private int _jumps = 2;

    //For input timings
    private float _heldTime = 0;
    private bool _startHold = false;

    
    private bool _canMove = true;
    private float _defaultGravity;
    private float _defaultMoveSpeed;

   
    private void FixedUpdate()
    {
        AutoRun((int)Direction);
    }
    private void Awake()
    {
        _defaultMoveSpeed = MovementSpeed;
        _defaultGravity = Rb.gravityScale;
    }
    private void Update()
    {
        //Times input
        if (_startHold)
        {
            _heldTime += Time.deltaTime;
        }
        if (_heldTime > 0.1f)
        {
            
            float rampTime = 0.7f;
            float minSpeed = 3f;

            MovementSpeed = _defaultMoveSpeed - (_defaultMoveSpeed - minSpeed) * Mathf.Clamp01(_heldTime / rampTime);

        }
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Grounded())
        {
            PlayerAnimator.SetBool("Jump", false);
        }
    }

    //Automatic Movement
    private void AutoRun(int direction)
    {
        if (_canMove)
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
        if (_jumps > 0)
        {
            PlayerAnimator.SetBool("Jump", true);
            Rb.linearVelocity = Vector2.zero;
            Rb.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);

            _jumps--;
        }
    }

    //check if on ground, returns whether grounded or not
    private bool Grounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.2f, Vector2.down, 0.75f, GroundedLayers);
        bool isGrounded = hit.collider != null;

        return isGrounded;
    }


    //Called when spacebar is pressed, checks how long it is held for
    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.performed) //pressed
        {
            _startHold = true;
            PlayerAnimator.SetBool("Charging", true);
        }
        else if (context.canceled) //released
        {
            PlayerAnimator.SetBool("Charging", false);
            MovementSpeed = _defaultMoveSpeed;
            CheckInputType(_heldTime);
            _startHold = false;
            _heldTime = 0;
        }
    }

    //called when space is released, determines what action was taken
    private void CheckInputType(float time)
    {
        if (Grounded())
            _jumps = 2;

        if (time < 0.1f)
        {
            if (_jumps > 0)
                Jump();
            else
                StartCoroutine(GroundSlam());
        }
        else if (time > 1 && Grounded())
        {
           GameObject target =  GetClosestObject(Vector2.right * (int)Direction, 0.5f);
            PlayerAnimator.SetTrigger("Swing");
            ApplyKnockback(target, (int)Direction,SwingKnockback);
        }
    }

    //Checks for gameobjects directly overlapping with this gameobject

    IEnumerator GroundSlam()
    {
        _canMove = false;

        // Stop motion and ensure gravity

        Rb.gravityScale = 0;
        Rb.linearVelocityY = 0;
        Rb.linearVelocityX *= 0.5f;
        yield return new WaitForSecondsRealtime(0.2f);

        Rb.gravityScale = _defaultGravity;

        Rb.AddForce(Vector2.down * 30, ForceMode2D.Impulse);

        

        // Wait until grounded
        yield return new WaitUntil(() => Grounded());

      

        //Knockback applied on landed
        GameObject[] targets = GetAllObjects(Vector2.down, SlamRadius);

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null) 
            {
                ApplyKnockback(targets[i], 0, 500);
                DealDamage(targets[i]);
                
            }
        }

        yield return new WaitForSeconds(0.5f);

        _canMove = true;
    }

    //Checks for target gameobject
    private GameObject GetClosestObject(Vector2 offset, float radius)
    {
        Vector2 circlePos = (Vector2)transform.position + offset;

        Collider2D[] hits = Physics2D.OverlapCircleAll(circlePos, radius, BreakableLayers);

        GameObject closestObject = null;
        float closestDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.SqrMagnitude((Vector2)hit.transform.position - circlePos);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestObject = hit.gameObject;
            }
        }

        return closestObject;
    }
    private GameObject[] GetAllObjects(Vector2 offset, float radius)
    {
        Vector2 circlePos = (Vector2)transform.position + offset;

        Collider2D[] hits = Physics2D.OverlapCircleAll(circlePos, radius, BreakableLayers);

        GameObject[] objects = new GameObject[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            objects[i] = hits[i].gameObject;
        }

        return objects;
    }
}

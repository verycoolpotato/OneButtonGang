using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private float MovementSpeed;

    private int _dir = 1;
    private void FixedUpdate()
    {
        AutoRun(_dir);
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
}

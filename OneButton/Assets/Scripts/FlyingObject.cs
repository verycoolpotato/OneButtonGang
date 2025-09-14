using UnityEngine;

public class FlyingObject : Destructible
{
    private float MoveSpeed;
  
    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(-0.1f * MoveSpeed, 0);

        if (transform.position.x < -19)
        {
            transform.position = new Vector2(110, transform.position.y);
        }
    }
    private void Start()
    {
        MoveSpeed = Random.Range(0.1f, 0.6f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
    }
}

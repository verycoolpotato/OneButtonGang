using UnityEngine;

public class ScreenScroll : MonoBehaviour
{
    [SerializeField] float ScrollSpeed;
    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(-0.1f * ScrollSpeed, 0);

        if(transform.position.x < -19)
        {
            transform.position = new Vector3 (19, transform.position.y, transform.position.z);
        }
    }
}

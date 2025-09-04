using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Attaches to the main camera, pretty self explanatory
    [SerializeField] GameObject Player;
    [SerializeField] Vector2 Offset;
    private void FixedUpdate()
    {
        transform.position = new Vector3
            (Player.transform.position.x + Offset.x,0 + Offset.y,-10);
    }
}

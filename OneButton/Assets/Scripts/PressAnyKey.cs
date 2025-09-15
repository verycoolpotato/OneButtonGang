using UnityEngine;
using UnityEngine.Events;
public class PressAnyKey : MonoBehaviour
{
    public UnityEvent onTriggered;
    private void Update()
    {
        if (Input.anyKeyDown == true)
        {
            onTriggered.Invoke();
        }
    }
}

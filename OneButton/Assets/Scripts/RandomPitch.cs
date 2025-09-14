using UnityEngine;
using UnityEngine.Audio;

public class RandomPitch : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float pitch;
   
    void Start()
    {
        pitch = Random.Range(0.6f, 1.4f);
        audioSource.pitch = pitch;
    }

    
}

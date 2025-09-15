using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int Health;
    [SerializeField] private int ScoreOnDestroyed;
    [SerializeField] private int ScoreOnDamaged;

    [Header("References")]
    [SerializeField] private SpriteRenderer Sprite;
    [SerializeField] private ParticleSystem DamageParticles; 
    [SerializeField] private GameObject SmokePrefab;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip DamagedAudio;
   

    [SerializeField] Rigidbody2D rb;

    private int _maxHealth;
    private float pitch;

    private void Start()
    {
        pitch = Random.Range(0.6f, 1.4f);
        audioSource.pitch = pitch;
        _maxHealth = Health;
       
    }

    
    public int health
    {
        get => Health;
        set
        {
            if (Health == value) return; 
            Health = value;
            Damaged(Health);
        }
    }

    private void Damaged(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            Break();
            return;
        }
        else if (currentHealth <= _maxHealth * 0.5f)
        {
            audioSource.PlayOneShot(DamagedAudio);
            ScoreManager.Instance.AddScore(ScoreOnDamaged);
        }
        StartCoroutine(LayerSwitcher());
      
        if (currentHealth > 0 && DamageParticles != null)
        {
            DamageParticles.Play();
        }
    }

    private IEnumerator LayerSwitcher()
    {
      
        gameObject.layer = 9;
        Sprite.sortingOrder = 3;

        yield return new WaitForSeconds(0.3f);

       
       
        gameObject.layer = 7;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.linearVelocity.magnitude > 10) 
        {
            health--;
        }
        if(collision.gameObject.layer == 9)
        {
            health--;
        }

    }

    private void Break()
    {
        StopAllCoroutines();
        ScoreManager.Instance.AddScore(ScoreOnDestroyed);

        

        if (SmokePrefab != null)
        {
            GameObject smokeGO = Instantiate(SmokePrefab, transform.position, Quaternion.identity);
           
           
        }

        Destroy(gameObject);
    }
}


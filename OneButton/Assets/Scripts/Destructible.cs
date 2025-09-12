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

    [SerializeField] Rigidbody2D rb;

    private int _maxHealth;
    

    private void Start()
    {
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
        // Set temporary layer and sprite order
        gameObject.layer = 9;
        Sprite.sortingOrder = 3;

        yield return new WaitForSeconds(0.3f);

        // After 0.3s, allow collisions
       
        gameObject.layer = 7;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.linearVelocity.magnitude > 5) 
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
           
            //Destroy(smokeGO, 4);
        }

        Destroy(gameObject);
    }
}


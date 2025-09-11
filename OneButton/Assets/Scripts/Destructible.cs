using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private int ScoreOnDestroyed;
    [SerializeField] private int ScoreOnDamaged;
    [SerializeField] private SpriteRenderer Sprite;
    private int _maxHealth;

    private void Start()
    {
        _maxHealth = Health;
    }
    //Run damaged logic on changed
    public int health
    {
        get => Health;
        set
        {
            Health = value;
            Damaged(Health);
            
        }
    }

    private void Damaged(int health)
    {
        
        if (health <= 0)
            Break();
        else if (health > 0 && health <= _maxHealth * 0.5f)
          ScoreManager.Instance.AddScore(ScoreOnDamaged);

        StartCoroutine(LayerSwitcher());

    }
    IEnumerator LayerSwitcher()
    {
        gameObject.layer = 9;
        Sprite.sortingOrder = 3;

        yield return new WaitForSeconds(0.3f);

        gameObject.layer = 7;
    }
    
    
    private void Break()
    {
        ScoreManager.Instance.AddScore(ScoreOnDestroyed);
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private int ScoreOnDestroyed;
    [SerializeField] private int ScoreOnDamaged;

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
    }

    
    
    private void Break()
    {
        Destroy(gameObject);
    }
}

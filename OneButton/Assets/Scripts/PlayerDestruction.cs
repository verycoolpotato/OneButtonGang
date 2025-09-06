using UnityEngine;

public class PlayerDestruction : MonoBehaviour
{






    public void ApplyKnockback(GameObject target, int directionX, float knockback)
    {
        if (target == null) return;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // Diagonal direction: equal x and y
        Vector2 diagonal = new Vector2(directionX, 1).normalized; // normalized ensures equal magnitude
        Vector2 force = diagonal * knockback;

        rb.AddForce(force, ForceMode2D.Impulse);
    }





    public void DealDamage(GameObject target)
    {
        Destructible destructible = target.GetComponent<Destructible>();
        if (destructible != null)

            destructible.health--;
    }

    //checks if this object can be damaged, objects can not be damaged repeatedly

    GameObject lastHit = null;
    public GameObject CanDamage(GameObject hit)
    {
        if (hit!= lastHit)
        {
            lastHit = hit;
           
            return lastHit;
        }
        else
        {
            return null;
        }
    }
}

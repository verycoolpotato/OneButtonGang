using UnityEngine;

public class PlayerDestruction : MonoBehaviour
{
    [SerializeField] PlayerController controller;

    [SerializeField] LayerMask BreakableLayers;

   
    private void Update()
    {
      overlapCheck();
      


        
    }


    private void overlapCheck()
    {
        
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.1f, BreakableLayers);

        if (hit != null)
        {
            GameObject objectToDamage = CanDamage(hit.transform.gameObject);

            if (objectToDamage != null)
            {
                Destructible destructible = objectToDamage.GetComponent<Destructible>();
                if (destructible != null)
                    destructible.health--;
    
                
            }
        }
    }


    //checks if this object can be damaged, objects can not be damaged repeatedly

    GameObject lastHit = null;
    private GameObject CanDamage(GameObject hit)
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

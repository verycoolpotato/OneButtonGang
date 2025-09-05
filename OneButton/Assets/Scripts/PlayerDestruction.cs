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
        GameObject ObjectToDamage = null;

         Collider2D hit = Physics2D.OverlapCircle(transform.position,0.1f,BreakableLayers);

        if (hit != null)
        {
            ObjectToDamage = CanDamage(hit.transform.gameObject);

            //Check if has destructible
            Destructible destructible = ObjectToDamage.GetComponent<Destructible>();

            if (destructible != null)
            {
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

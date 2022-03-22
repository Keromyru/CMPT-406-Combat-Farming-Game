using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{   
      public void knockback(Vector3 origin, float scale)
    {   //Adds Momentum to the player in oposite direction of the Origin
        Vector2 knockback = (transform.position - origin).normalized*scale;
         GetComponent<Rigidbody>().AddForce(knockback,ForceMode.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDetection : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10 || collision.gameObject.layer == 3)
        {
            Combatant combatant = collision.gameObject.GetComponent<Combatant>();
            if(!combatant.IsDead())
            {
                combatant.TakeDamage(combatant.getCurrentHealth());
            }
        }
    }
}

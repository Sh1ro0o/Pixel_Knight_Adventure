using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDetection : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10 || collision.gameObject.layer == 3)
        {
           collision.gameObject.GetComponent<Combatant>().Die();
        }
    }
}

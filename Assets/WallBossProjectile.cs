using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBossProjectile : MonoBehaviour
{
    //boss
    [SerializeField] Boss boss;

    //spawn
    Vector3 spawnLocation = new Vector3(0, 0, 0);
    int mouthNumber = -1;

    private void Start()
    {
        mouthNumber = boss.GetMouthNumber();

        if (mouthNumber == 0)
            spawnLocation = new Vector3(0, 0, 0);
        else if (mouthNumber == 1)
            spawnLocation = new Vector3(0, 0, 0);
        else if (mouthNumber == 2)
            spawnLocation = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //player takes damage equal to boss damage
            collision.GetComponent<Combatant>().TakeDamage(boss.GetBossDamage());
            //destroys projectile
            Destroy(gameObject);
        }
    }
}

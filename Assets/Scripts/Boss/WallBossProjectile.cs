using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBossProjectile : MonoBehaviour
{
    //boss
    Boss boss;

    //stats
    [SerializeField] float projectileSpeed = 2.5f;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
    }

    private void Update()
    {
        //deltaTime movement (is negative because it needs to move to the left)
        transform.Translate(-(Time.fixedDeltaTime * projectileSpeed), 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if hit a player
        if (collision.gameObject.name == "Player")
        {
            //if player is not dead
            if(!collision.gameObject.GetComponent<Combatant>().IsDead())
            {
                //player takes damage equal to boss damage
                collision.GetComponent<Combatant>().TakeDamage(boss.GetBossDamage());
                //destroys projectile
                Destroy(gameObject);
            }
        }
        //checks if projectile hits BossTrigger
        if (collision.gameObject.name == "BossTrigger")
        {
            Destroy(gameObject);
        }
    }
}

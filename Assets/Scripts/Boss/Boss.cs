using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Combatant
{
    [Header("Boss")]
    [HideInInspector] public bool isBossActivated = false;
    Vector3 projectileSpawnLocation = new Vector3(0, 0, 0);
    int mouthNumber;

    [Header("Timer")]
    float attackTimer = 0f;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;

    protected override void Start()
    {
        base.Start();

        currentHealth = maxHealth;
        currentDamage = maxDamage;
        //boss starts with 1 second attack cooldown
        attackCooldown = 1f;
    }

    protected override void Update()
    {
        //checks if boss activated from the trigger and if it isn't dead
        if (isBossActivated && !isCurrentlyDead)
        {
            attackTimer += Time.deltaTime;

            //attacks every second
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0.0f;

                Attack();
            }
        }
    }
    protected override void Attack()
    {
        //shoots with a random mouth 0 - bottom, 1 - middle, 2 - top
        mouthNumber = Random.Range(0, 3);
        Debug.Log(mouthNumber);
        if (mouthNumber == 0)
        {
            animator.Play("boss_open_bottom");
            projectileSpawnLocation = new Vector3(4.3f, 0, 0);
        }
        else if (mouthNumber == 1)
        {
            animator.Play("boss_open_middle");
            projectileSpawnLocation = new Vector3(4.3f, 2f, 0);
        }
        else if (mouthNumber == 2)
        {
            animator.Play("boss_open_top");
            projectileSpawnLocation = new Vector3(4.3f, 4.2f, 0);
        }

        //spawns projectile at position Vector3 and zero rotation (Quaternion.identity).
        Instantiate(projectile, projectileSpawnLocation, Quaternion.identity);
    }

    public int GetBossDamage()
    {
        return currentDamage;
    }
}

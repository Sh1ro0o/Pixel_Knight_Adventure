using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Combatant
{
    //animator
    [SerializeField] Animator animator;

    //boss
    [HideInInspector] public bool isBossActivated = false;
    int mouthNumber;

    //timer
    float attackTimer = 0f;

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

                //shoots with a random mouth 0 - bottom, 1 - middle, 2 - top
                mouthNumber = Random.Range(0, 3);
                Debug.Log(mouthNumber);
                if (mouthNumber == 0)
                {
                    animator.Play("boss_open_bottom");
                }
                else if (mouthNumber == 1)
                {
                    animator.Play("boss_open_middle");
                }
                else if (mouthNumber == 2)
                {
                    animator.Play("boss_open_top");
                }
            }
        }
    }
    protected override void Attack()
    {

    }

    public int GetBossDamage()
    {
        return currentDamage;
    }

    public int GetMouthNumber()
    {
        return mouthNumber;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combatant
{
    public Transform attackPointRadius;

    //combat variables
    public float attackRange = 0.5f;
    float timeSinceLastAttack = 0f;

    protected override void Start()
    {
        base.Start();

        currentHealth = maxHealth;
        currentDamage = maxDamage;
        timeSinceLastAttack = attackCooldown;
    }

    // Update is called once per frame
    protected override void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= attackCooldown && Input.GetButtonDown("Attack"))
        {
            Attack();
            timeSinceLastAttack = 0f;
        }
        else
            isAttacking = false;
    }

    protected override void Attack()
    {
        if (!isCurrentlyDead)
        {
            //attack animation
            animator.SetTrigger("Attack");
            isAttacking = true;

            //enemies detection
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

            //damage enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                //Checks if enemy is already dead
                if (!(enemy.GetComponent<Combatant>().IsDead()))
                {
                    Debug.Log("You hit " + enemy.name);
                    enemy.GetComponent<Combatant>().TakeDamage(currentDamage);
                }
            }
        }
    }

    //draws our sphere for detecting
    private void OnDrawGizmosSelected()
    {
        // if attack point hasen't been assigned yet
        if (attackPointRadius == null)
            return;

        Gizmos.DrawWireSphere(attackPointRadius.position, attackRange);
    }
}

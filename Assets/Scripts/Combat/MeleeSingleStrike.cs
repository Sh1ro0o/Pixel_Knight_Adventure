using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSingleStrike : Combatant
{
    [Header("Attack Specifics")]
    public Transform attackPointRadius;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] protected float timeSinceLastAttackAnimation = 0f;

    [Header("Deal damage to enemy")]
    [SerializeField] float damageDealTimerStart = 0f;
    [SerializeField] float damageDealTimerStop = 1f;
    [SerializeField] bool isEnemyDetected = false;

    protected override void Start()
    {
        base.Start();

        //ignores collision with specified layers
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"));

        //sets samurai stats
        currentHealth = maxHealth;
        currentDamage = maxDamage;

        //resets cooldowns
        timeSinceLastAttackAnimation = attackCooldown;
    }

    protected override void Update()
    {
        //animation timer
        timeSinceLastAttackAnimation += Time.deltaTime;
        //damage timer
        damageDealTimerStart += Time.deltaTime;

        //detects enemies as long as we are alive   
        if (!IsDead())
        {
            EnemyDetection();

            //if we detected at least 1 enemy we launch an attack animation as long as the attack is not on cooldown
            if (!isAttacking && isEnemyDetected && (timeSinceLastAttackAnimation >= attackCooldown))
            {
                isAttacking = true;
                //attack animation
                animator.SetTrigger("Attack");
                //we start the cooldown timer if we tried to attack an enemy
                timeSinceLastAttackAnimation = 0f;
                //we wait for the sword animation to pull out the sword before the strike
                damageDealTimerStart = 0f;
            }

            if (isAttacking && (damageDealTimerStart > damageDealTimerStop))
            {
                Attack();
            }
        }
    }

    private void EnemyDetection()
    {
        //checks detection radius for enemies
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

        //checks if any alive enemies are in the detection zone
        isEnemyDetected = false;
        foreach (Collider2D enemy in detectedEnemies)
        {
            if (!enemy.GetComponent<Combatant>().IsDead())
            {
                isEnemyDetected = true;
                break;
            }
        }
    }

    protected override void Attack()
    {
        //Debug.Log("You Attacked " + enemy.name + " for " + currentDamage + "!");
        //damage enemies//enemies detection to actually deal damage
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

        foreach (Collider2D enemy in detectedEnemies)
        {
            //hits at least one if the detected is not dead
            if(!enemy.GetComponent<Combatant>().IsDead())
            {
                enemy.GetComponent<Combatant>().TakeDamage(currentDamage);
                Debug.Log(enemy.name + " has taken " + currentDamage);
            }
        }
        isAttacking = false;

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
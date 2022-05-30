using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : Enemy
{
    [Header("Attack Specifics")]
    public Transform attackPointRadius;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField] protected float timeSinceLastAttacked = 0f;
    [SerializeField] bool isAttacking = false;

    [Header("Deal damage to enemy")]
    [SerializeField] float damageDealTimerStart = 0f;
    [SerializeField] float damageDealTimerStop = 2f;

    private void Start()
    {
        //ignores collision with specified non collidable layers
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"));

        //attack cd can't be lower than 2
        if (attackCooldown < 2)
            attackCooldown = 2;

        //sets attack rate
        damageDealTimerStop = attackCooldown - 1;

        //sets samurai stats
        currentHealth = maxHealth;
        currentDamage = maxDamage;

        //resets cooldowns
        timeSinceLastAttacked = attackCooldown;
    }

    protected override void Update()
    {
        //animation timer
        timeSinceLastAttacked += Time.deltaTime;
        //damage timer
        damageDealTimerStart += Time.deltaTime;

        if (timeSinceLastAttacked >= attackCooldown)
        {
            //attacks if we are alive   
            if (!IsDead())
            {
                EnemyDetection();
            }
        }

        if (isAttacking && (damageDealTimerStart > damageDealTimerStop) && !IsDead())
        {
            Attack();
        }
    }

    protected override void EnemyDetection()
    {
        //enemies detection triggering animation
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

        bool foundAliveEnemy = false;
        foreach (Collider2D enemy in detectedEnemies)
        {
            //checks if any alive enemies are in the detection zone
            if (!enemy.GetComponent<PlayerCombat>().isCurrentlyDead)
            {
                foundAliveEnemy = true;
            }
        }

        //if we detected more than 1 enemy we launch an attack animation
        if (detectedEnemies.Length > 0 && !isAttacking && foundAliveEnemy)
        {
            isAttacking = true;
            //attack animation
            animator.SetTrigger("Attack");
            //we start the cooldown if tried to attack an enemy
            timeSinceLastAttacked = 0f;
            //we wait 1 second of animation for the sword to be pulled out before the strike
            damageDealTimerStart = 0f;
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
            if(!enemy.GetComponent<PlayerCombat>().isCurrentlyDead)
            {
                enemy.GetComponent<PlayerCombat>().TakeDamage(currentDamage);
                Debug.Log(enemy.name + " has taken " + currentDamage);
            }
        }
        isAttacking = false;

    }

    //draws our sphere for detecting
    private void OnDrawGizmosSelected()
    {
        // iff attack point hasen't been assigned yet
        if (attackPointRadius == null)
            return;

        Gizmos.DrawWireSphere(attackPointRadius.position, attackRange);
    }
}

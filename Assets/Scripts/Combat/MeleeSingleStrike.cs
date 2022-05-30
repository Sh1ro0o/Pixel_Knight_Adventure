using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSingleStrike : Combatant
{
    [Header("Attack Specifics")]
    public Transform attackPointRadius;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField] protected float timeSinceLastAttackAnimation = 0f;
    [SerializeField] protected float lowestCooldownPossible = 2f;
    [SerializeField] bool isAttacking = false;

    [Header("Deal damage to enemy")]
    [SerializeField] float damageDealTimerStart = 0f;
    [SerializeField] float damageDealTimerStop = 1f;
    [SerializeField] bool enemyDetected = false;

    public void Start()
    {
        //ignores collision with specified non collidable layers
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"));

        //attack cd can't be lower than lowest possible cooldown
        if (attackCooldown < lowestCooldownPossible)
            attackCooldown = lowestCooldownPossible;

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
            if (!isAttacking && enemyDetected && (timeSinceLastAttackAnimation >= attackCooldown))
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

    protected void EnemyDetection()
    {
        //checks detection radius for enemies
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

        //checks if any alive enemies are in the detection zone
        bool foundAliveEnemy = false;
        foreach (Collider2D enemy in detectedEnemies)
        {
            if (!enemy.GetComponent<Combatant>().IsDead())
            {
                foundAliveEnemy = true;
                break;
            }
        }

        //sets if we detected an enemy
        if (foundAliveEnemy)
            enemyDetected = true;
        else
            enemyDetected = false;
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

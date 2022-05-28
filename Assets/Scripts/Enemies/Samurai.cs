using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : Enemy
{
    [Header("Attack Specifics")]
    public Transform attackPointRadius;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] bool isAttacking = false;

    [Header("Deal damage to enemy")]
    [SerializeField] float damageDealTimerStart = 0f;
    [SerializeField] float damageDealTimerStop = 2f;

    [Header("Animator")]
    public Animator animator;

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

        if (isAttacking && (damageDealTimerStart > damageDealTimerStop))
        {
            //enemies detection to actually deal damage
            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

            
            foreach (Collider2D enemy in detectedEnemies)
            {
                //hits at least one
                Attack(enemy);
            }

            isAttacking = false;
        }
    }

    protected override void EnemyDetection()
    {
        //enemies detection triggering animation
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

        //if we detected more than 1 enemy we launch an attack animation
        if (detectedEnemies.Length > 0 && !isAttacking)
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

    protected override void Attack(Collider2D enemy)
    {
        Debug.Log("You Attacked " + enemy.name + " for " + currentDamage + "!");
        //damage enemies
    }

    public override void TakeDamage (int damage)
    {
        if(!isCurrentlyDead)
        {
            currentHealth -= damage;

            //play hurt animation
            animator.SetTrigger("isDamaged");

            //checks if dead
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    protected override void Die()
    {
        Debug.Log("Enemy died!");

        //Die animation
        animator.SetBool("isDead", true);

        isCurrentlyDead = true;
    }

    //uses isDead from parent class Enemy

    //draws our sphere for detecting
    private void OnDrawGizmosSelected()
    {
        // iff attack point hasen't been assigned yet
        if (attackPointRadius == null)
            return;

        Gizmos.DrawWireSphere(attackPointRadius.position, attackRange);
    }
}

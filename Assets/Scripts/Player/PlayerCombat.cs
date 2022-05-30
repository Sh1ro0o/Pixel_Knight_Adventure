using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPointRadius;
    public LayerMask enemy;

    //combat variables
    public int maxDamage = 10;
    public int maxHealth = 50;
    public float attackRange = 0.5f;
    public float attackCooldown = 1f;
    float timeSinceLastAttack = 0f;
    int currentHealth;
    int currentDamage;
    public bool isCurrentlyDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        currentDamage = maxDamage;
        timeSinceLastAttack = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if(timeSinceLastAttack >= attackCooldown && Input.GetButtonDown("Attack"))
        {
            Attack();
            timeSinceLastAttack = 0f;
        }
    }

    void Attack()
    {
        if (!isCurrentlyDead)
        {
            //attack animation
            animator.SetTrigger("Attack");

            //enemies detection
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

            //damage enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                //Checks if enemy is already dead
                if (!(enemy.GetComponent<Samurai>().IsDead()))
                {
                    Debug.Log("You hit " + enemy.name);
                    enemy.GetComponent<Enemy>().TakeDamage(currentDamage);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isCurrentlyDead)
        {
            currentHealth -= damage;

            //play hurt animation
            Debug.Log("Player received damage");
            animator.SetTrigger("isDamaged");

            //checks if dead
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("Player died!");

        //stop animations from playing and play death animation
        animator.StopPlayback();
        animator.Play("Player_death");

        //Die animation
        animator.SetBool("isDead", true);

        isCurrentlyDead = true;
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

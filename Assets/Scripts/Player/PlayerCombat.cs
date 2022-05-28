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
    public int maxHealth = 100;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    int currentHealth;
    int currentDamage;
    bool isAlive = true;

    private void Start()
    {
        currentHealth = maxHealth;
        currentDamage = maxDamage;
        attackRate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime && Input.GetButtonDown("Attack") && isAlive)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }

    }

    void Attack()
    {
        //attack animation
        animator.SetTrigger("Attack");

        //enemies detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRadius.position, attackRange, enemy);

        //damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            //Checks if enemy is already dead
            if(!(enemy.GetComponent<Samurai>().IsDead()))
            {
                Debug.Log("You hit " + enemy.name);
                enemy.GetComponent<Enemy>().TakeDamage(currentDamage);
            }
        }
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

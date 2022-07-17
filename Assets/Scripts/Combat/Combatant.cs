using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] protected Animator animator;

    [Header("Health")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    [Header("Attack")]
    [SerializeField] protected int maxDamage = 10;
    [SerializeField] protected int currentDamage;
    [SerializeField] protected LayerMask enemy;
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField] protected float lowestCooldownPossible = 2f;

    [Header("Status")]
    [SerializeField] protected bool isCurrentlyDead = false;
    [SerializeField] protected bool isAttacking = false;

    protected abstract void Update();
    protected abstract void Attack();

    protected virtual void Start()
    {
        //attack cd can't be lower than lowest possible cooldown
        if (attackCooldown < lowestCooldownPossible)
            attackCooldown = lowestCooldownPossible;
    }
    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died!");

        //stop animations from playing and play death animation
        animator.StopPlayback();
        animator.Play("death");

        //Die animation
        animator.SetBool("isDead", true);

        //so we can call Die function to just kill the enemy and remove its hp
        currentHealth = 0;

        isCurrentlyDead = true;
    }

    public virtual void Revive()
    {
        animator.SetBool("isDead", false);

        currentHealth = maxHealth;
        isCurrentlyDead = false;
    }

    public virtual void TakeDamage(int damage)
    {
        if (!isCurrentlyDead)
        {
            currentHealth -= damage;

            //checks if dead
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    public virtual bool IsDead()
    {
        return isCurrentlyDead;
    }

    public virtual bool IsAttacking()
    {
        return isAttacking;
    }

    public virtual float GetAttackCooldown()
    {
        return attackCooldown;
    }

    public virtual int getMaxDamage()
    {
        return maxDamage;
    }

    public virtual int getMaxHealth()
    {
        return maxHealth;
    }

    public virtual int getCurrentHealth()
    {
        return currentHealth;
    }
}

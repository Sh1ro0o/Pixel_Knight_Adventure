using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] protected Animator animator;

    [Header("Health")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    [Header("Attack")]
    [SerializeField] protected int maxDamage = 10;
    [SerializeField] protected int currentDamage;
    [SerializeField] public LayerMask enemy;

    [Header("Status")]
    [SerializeField] protected bool isCurrentlyDead = false;

    protected abstract void EnemyDetection();

    protected abstract void Attack();

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " died!");

        //stop animations from playing and play death animation
        animator.StopPlayback();
        animator.Play(gameObject.name + "_death");

        //Die animation
        animator.SetBool("isDead", true);

        isCurrentlyDead = true;
    }

    public virtual void TakeDamage(int damage)
    {
        if (!isCurrentlyDead)
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
    public virtual bool IsDead()
    {
        return isCurrentlyDead;
    }

    protected abstract void Update();
}

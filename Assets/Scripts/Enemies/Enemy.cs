using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    [Header("Attack")]
    [SerializeField] protected int maxDamage = 10;
    [SerializeField] protected int currentDamage;
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField] protected float timeSinceLastAttacked = 0f;
    [SerializeField] public LayerMask enemy;

    [Header("Status")]
    [SerializeField] protected bool isCurrentlyDead = false;


    protected abstract void EnemyDetection();

    protected abstract void Attack(Collider2D enemy);

    public abstract void TakeDamage(int damage);

    protected abstract void Die();

    public virtual bool IsDead()
    {
        return isCurrentlyDead;
    }

    protected abstract void Update();
}

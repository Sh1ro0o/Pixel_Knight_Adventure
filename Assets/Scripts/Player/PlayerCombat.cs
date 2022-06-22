using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combatant
{
    public Transform attackPointRadius;

    //combat variables
    public float attackRange = 0.5f;
    float timeSinceLastAttack = 0f;

    //invulnerable
    [Header("invulnerable")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float invulnerableDuration = 1f;
    float timeSinceLastTookDamage = 0f;
    bool isInvulnurable = false;

    protected override void Start()
    {
        base.Start();

        currentHealth = maxHealth;
        currentDamage = maxDamage;
        timeSinceLastAttack = attackCooldown;
        timeSinceLastTookDamage = invulnerableDuration;
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

        //invulnerable mechanics
        timeSinceLastTookDamage += Time.deltaTime;
        if (timeSinceLastTookDamage > invulnerableDuration && spriteRenderer.color == Color.red)
        {
            isInvulnurable = false;
            spriteRenderer.color = Color.white;
        }

        //Debug.Log("is invulnerable: " + isInvulnurable);
    }

    protected override void Attack()
    {
        //if we aren't dead and if the game isn't paused
        if (!isCurrentlyDead && Time.timeScale != 0)
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

            //play hit SFX if at least 1 enemy was hit otherwise just play the sword swing SFX
            if(hitEnemies.Length > 0)
                AudioManager.audioManager.PlaySound(AudioManager.SoundSystem.Player_hit);
            else
                AudioManager.audioManager.PlaySound(AudioManager.SoundSystem.Player_swing);
        }
    }

    public override void TakeDamage(int damage)
    {
        if (isInvulnurable)
            base.TakeDamage(0);
        else
        {
            base.TakeDamage(damage);
            timeSinceLastTookDamage = 0f;
            spriteRenderer.color = Color.red;
            isInvulnurable = true;
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

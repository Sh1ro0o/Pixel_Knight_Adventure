using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Combatant
{
    [Header("Boss")]
    [HideInInspector] bool isBossActivated = false;
    Vector3 projectileSpawnLocation = new Vector3(0, 0, 0);
    int mouthNumber;
    float sleepCounter = 0;
    bool isSleeping = false;

    [Header("Timer")]
    float attackTimer = 0f;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] int maxProjectiles = 20;
    [SerializeField] int projectilesLeft;

    [Header("Finish Flag")]
    [SerializeField] GameObject finishFlag;

    //boss throws 20 projectiles after that it stops attacking for 5 seconds (giving the player room to attack), when the boss is below half hp speed goes 2x faster
    //and has 40 projectiles
    protected override void Start()
    {
        base.Start();

        currentHealth = maxHealth;
        currentDamage = maxDamage;
        //boss starts with 1 second attack cooldown
        attackCooldown = 1f;

        //ammount of projectiles left to be thrown is set to max projectiles
        projectilesLeft = maxProjectiles;
    }

    public override void Die()
    {
        base.Die();
        finishFlag.SetActive(true);
    }

    protected override void Update()
    {
        //checks if boss activated from the trigger and if it isn't dead
        if (isBossActivated && !isCurrentlyDead)
        {
            //after all projectiles have been shot wait 5 seconds and refresh projectiles ammount
            if (projectilesLeft == 0)
            {
                //boss does nothing for given ammount of seconds
                isSleeping = true;
                sleepCounter += Time.deltaTime;

                //resets sleep counter, refreshes projectiles
                if(sleepCounter >= 5f)
                {
                    sleepCounter = 0f;
                    isSleeping = false;
                    projectilesLeft = maxProjectiles;
                }
            }

            if(!isSleeping)
            {
                attackTimer += Time.deltaTime;

                //attacks every second
                if (attackTimer >= attackCooldown)
                {
                    attackTimer = 0.0f;

                    Attack();
                }
            }

            //if monster is half hp attack speed increases
            if (currentHealth < maxHealth / 2)
            {
                attackCooldown = 0.5f;
                maxProjectiles = 40;
            }
        }
    }
    protected override void Attack()
    {
        //shoots with a random mouth 0 - bottom, 1 - middle, 2 - top
        mouthNumber = Random.Range(0, 3);
        Debug.Log(mouthNumber);
        if (mouthNumber == 0)
        {
            animator.Play("boss_open_bottom");
            projectileSpawnLocation = new Vector3(4.3f, 0, 0);
        }
        else if (mouthNumber == 1)
        {
            animator.Play("boss_open_middle");
            projectileSpawnLocation = new Vector3(4.3f, 2f, 0);
        }
        else if (mouthNumber == 2)
        {
            animator.Play("boss_open_top");
            projectileSpawnLocation = new Vector3(4.3f, 4.2f, 0);
        }

        //spawns projectile at position Vector3 and zero rotation (Quaternion.identity).
        Instantiate(projectile, projectileSpawnLocation, Quaternion.identity);
        projectilesLeft--;
    }

    public int GetBossDamage()
    {
        return currentDamage;
    }

    public void ActivateBoss()
    {
        //sets boss activation boolean to true
        isBossActivated = true;
    }

    public void DeactivateBoss()
    {
        ResetBossStats();
        //sets boss activation boolean to false
        isBossActivated = false;
    }


    public void ResetBossStats()
    {
        //resets boss hp
        currentHealth = maxHealth;
        //resets max projectiles
        maxProjectiles = 20;
        //resets projectiles left
        projectilesLeft = maxProjectiles;
    }
}

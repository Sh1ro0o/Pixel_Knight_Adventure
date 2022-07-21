using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyMovement : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] Animator animator;

    [Header("Direction")]
    [SerializeField] bool isFacingRight = true;

    [Header("Speed")]
    [SerializeField] float movementSpeed = 2f;

    [Header("Checks")]
    [SerializeField] Transform edgeCheck;
    [SerializeField] float edgeCheckRadius = 0.2f;
    [SerializeField] LayerMask ground;

    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckRadius = 0.05f;
    [SerializeField] LayerMask wall;

    Combatant combatant;
    protected float timeSinceLastAttack = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        combatant = GetComponent<Combatant>();
        //we reset the cooldown so the enemy moves as soon as it spawns
        timeSinceLastAttack = combatant.GetAttackCooldown();
    }

    // Update is called once per frame
    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        //if not dead
        if (!combatant.IsDead())
        {
            if (!combatant.IsAttacking() && timeSinceLastAttack >= combatant.GetAttackCooldown())
            {
                //if we move near the edge or a wall we turn around
                if (EdgeCheck() || WallCheck())
                    Flip();
                else
                    Move();
            }
            if (combatant.IsAttacking())
            {
                timeSinceLastAttack = 0f;
                animator.SetBool("isRunning", false);
            }
        }
    }

    private void FixedUpdate()
    {
    }

    //flips player in the y-axis
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void Move()
    {
        transform.Translate((Time.deltaTime * movementSpeed),0,0);
        animator.SetBool("isRunning", true);
    }

    private bool EdgeCheck()
    {
        //if we have not detected a ground layer we have found the edge
        Collider2D[] colliders = Physics2D.OverlapCircleAll(edgeCheck.position, edgeCheckRadius, ground);

        if (colliders.Length == 0)
        {
            return true;
        }
        else
            return false;
    }

    private bool WallCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, wallCheckRadius, wall);
        if (colliders.Length > 0)
        {
            Debug.Log("touching wall!");
            return true;
        }
        else
            return false;

    }

    //draws our sphere for detecting
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
    }
}

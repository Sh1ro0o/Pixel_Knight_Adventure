using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    

    // Update is called once per frame
    void Update()
    {
        //attacks if we are not wall sliding
        if (Input.GetButtonDown("Attack") && !animator.GetBool("isWallSliding"))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}

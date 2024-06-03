using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
           Attack(); 
        }
    }

    void Attack()
    {
        // attack animation
        animator.SetTrigger("attack");

        // detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // apply damage
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<tentacles>().takeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

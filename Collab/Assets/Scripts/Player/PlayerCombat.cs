using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private Animator animator;
    // Update is called once per frame

    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;

    
/*    private void Start()
    {
        
    }*/
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

    }

    private void Attack()
    {
        //Play an attack animation
        animator.SetTrigger("Attack");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name);
        }
    }

    private void OnDrawGizmos()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

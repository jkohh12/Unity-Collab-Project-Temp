using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;

    private Animator animator;
    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    public void TakeDmg(int dmg)
    {


        //Take Damage Animation
        currentHP -= dmg;
        animator.SetTrigger("TakeDamage");
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}

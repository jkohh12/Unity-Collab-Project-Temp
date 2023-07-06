using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;
    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDmg(int dmg)
    {
        //Take Damage Animation
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushCollisionDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int mushDmg;
    public PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDmg(mushDmg/2);
        }
    }

}

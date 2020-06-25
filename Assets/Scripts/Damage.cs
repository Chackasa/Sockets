using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.name != "Player")
        {
            if (collision.GetComponent<recieve_Damage>() != null)
            {
                collision.GetComponent<recieve_Damage>().DealDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}

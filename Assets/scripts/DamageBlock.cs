using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : MonoBehaviour
{
    [SerializeField] private float damage = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}

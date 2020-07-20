using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACHED TO COLLIDER CHILD

public class PlayerDamageController : MonoBehaviour
{
    public PlayerController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyDamageController>().Attacked();
            //Debug.Log("KnockBack");
            player.Attack();
        }
    }
}

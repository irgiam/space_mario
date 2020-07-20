using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    Collider2D thisCollider;
    public bool goingDeath = false;

    private void Awake()
    {
        thisCollider = this.GetComponent<Collider2D>();
    }

    public void Attacked()
    {
        Debug.Log("Slime Attacked");
        goingDeath = true;
        if (thisCollider.enabled == false)
            this.transform.position = this.transform.position;

        Destroy(this.gameObject, 3f);
    }
}

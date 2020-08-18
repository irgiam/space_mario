using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collected();
        }
    }

    void Collected()
    {
        GameManager.instance.collectedCoin++;
        this.gameObject.SetActive(false);
    }
}

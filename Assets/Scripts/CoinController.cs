using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioSource getCoinSound;
    SpriteRenderer thisSprite;
    Collider2D thisCollider;

    private void Awake()
    {
        thisSprite = this.GetComponent<SpriteRenderer>();
        thisCollider = this.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collected();
        }
    }

    void Collected()
    {
        getCoinSound.Play();
        GameManager.instance.collectedCoin++;
        thisSprite.enabled = false;
        thisCollider.enabled = false;
    }
}

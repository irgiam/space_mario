using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ClearLives();
            GameManager.instance.GameOver();
        }
    }

    void ClearLives()
    {
        for (int i=0; i<InGameView.instance.playerLives.Count; i++)
        {
            InGameView.instance.LoseLives();
        }
    }
}

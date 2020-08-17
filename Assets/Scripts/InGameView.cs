using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameView : MonoBehaviour
{
    public Image playerLivesPrefab;
    public static InGameView instance;
    public GameObject healthParent;
    public List<Image> playerLives = new List<Image>(); //health1 pos = (-320, 170), x+=40

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        Vector2 uiPosition = new Vector2(-320, 170);
        for (int i=0; i<3; i++)
        {
            Image life = (Image)Instantiate(playerLivesPrefab);
            life.transform.SetParent(healthParent.transform, false);
            life.transform.localPosition = uiPosition;
            playerLives.Add(life);
            uiPosition.x += 40;
        }
    }

    public void LoseLives()
    {
        Destroy(playerLives[playerLives.Count - 1].gameObject);
        Image losenLive = playerLives[playerLives.Count - 1];
        playerLives.Remove(losenLive);
    }
}

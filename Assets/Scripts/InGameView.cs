using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameView : MonoBehaviour
{
    public static InGameView instance;
    public List<GameObject> playerLives = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public List<GameObject> playerLives = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
}

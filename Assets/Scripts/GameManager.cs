using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState //simply create a new enum, which works like an object and can be passed to the method as a parameter
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public Canvas inGameView;
    public Canvas menuView;
    public Canvas gameOverView;

    public GameState currentGameState;

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            menuView.enabled = true;
            inGameView.enabled = false;
            //gameOverView.enabled = false;
        }
        else if (newGameState == GameState.inGame)
        {
            menuView.enabled = false;
            inGameView.enabled = true;
            //gameOverView.enabled = false;
        }
        else if (newGameState == GameState.gameOver)
        {
            menuView.enabled = false;
            inGameView.enabled = false;
            //gameOverView.enabled = true;
        }
        currentGameState = newGameState;
    }
}

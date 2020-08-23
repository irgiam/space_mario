using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState //simply create a new enum, which works like an object and can be passed to the method as a parameter
{
    menu,
    inGame,
    gameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Canvas inGameView;
    public Canvas menuView;
    public Canvas gameOverView;

    public int collectedCoin = 0;

    public GameState currentGameState;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetGameState(GameState.menu);
    }

    private void Update()
    {
        if (currentGameState == GameState.menu)
        {
            Time.timeScale = 0;
        }
        else if (currentGameState == GameState.inGame)
        {
            Time.timeScale = 1;
        }
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            menuView.enabled = true;
            inGameView.enabled = false;
            gameOverView.enabled = false;
        }
        else if (newGameState == GameState.inGame)
        {
            menuView.enabled = false;
            inGameView.enabled = true;
            gameOverView.enabled = false;
        }
        else if (newGameState == GameState.gameOver)
        {
            menuView.enabled = false;
            inGameView.enabled = false;
            gameOverView.enabled = true;
        }
        currentGameState = newGameState;
    }

    public void NewGame() //attached to play button in unity editor
    {
        SetGameState(GameState.inGame);
        InGameView.instance.StartGame();
        EnemyManager.instance.SpawnEnemy();
        //Debug.Log("error");
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        NewGame();
        PlayerController.instance.StartGame();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

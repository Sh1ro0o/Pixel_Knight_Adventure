using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;

    public int coins;
    public GameState state;

    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Level:
                break;
            case GameState.Victory:
                break;
            case GameState.Defeat:
                break;
        }
    }


    public enum GameState
    {
        MainMenu,
        Level,
        Victory,
        Defeat
    }

}

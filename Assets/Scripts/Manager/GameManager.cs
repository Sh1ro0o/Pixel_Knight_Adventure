using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;
    public int coins;
    public int totalCoins;

    private void Start()
    {
        //gets the ammount of coins at gamestart
        totalCoins = PlayerPrefs.GetInt("Coins", 0);
    }

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

    public void LoadScene(Scenes scene)
    {
        switch(scene)
        {
            case Scenes.MainMenu:
                SceneManager.LoadScene("MainMenu");
                break;
            case Scenes.Level1:
                coins = 0;
                SceneManager.LoadScene("Level1");
                break;
            case Scenes.Level2:
                coins = 0;
                SceneManager.LoadScene("Level2");
                break;
            case Scenes.Level3:
                coins = 0;
                SceneManager.LoadScene("Level3");
                break;
            case Scenes.test:
                coins = 0;
                SceneManager.LoadScene("TestScene");
                break;
            default:
                Debug.Log("Scene doesn't exist or could not be found!");
                break;
        }
    }
    public void RemoveCoins(int ammount)
    {
        totalCoins -= ammount;
        PlayerPrefs.SetInt("Coins", totalCoins);
    }

    public void FinishedLevel()
    {
        //saves coins
        totalCoins += coins;
        PlayerPrefs.SetInt("Coins", totalCoins);
        Debug.Log("Total coins: " + totalCoins);

        //loads main menu
        LoadScene(Scenes.MainMenu);
    }
}

public enum Scenes
{
    MainMenu,
    Level1,
    Level2,
    Level3,
    test
}

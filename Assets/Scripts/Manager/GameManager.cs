using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;
    public int coins;

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
                SceneManager.LoadScene("Level1");
                break;
            case Scenes.Level2:
                SceneManager.LoadScene("Level2");
                break;
            case Scenes.Level3:
                SceneManager.LoadScene("Level3");
                break;
            default:
                Debug.Log("Scene doesn't exist or could not be found!");
                break;
        }
    }
}

public enum Scenes
{
    MainMenu,
    Level1,
    Level2,
    Level3
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.gameManager.LoadScene(Scenes.Level2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

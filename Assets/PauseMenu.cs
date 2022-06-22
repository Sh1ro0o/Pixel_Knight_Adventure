using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject background;

    bool isDisplayingMenu = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isDisplayingMenu = !isDisplayingMenu;
            if (isDisplayingMenu)
                DisplayMenu();
            else
                HideMenu();
        }
    }

    public void Restart()
    {
        Debug.Log("Restart()");
    }

    public void BackToMainMenu()
    {
        //TO-DO: remove the coins we've gotten this run
        Debug.Log("BackToMainMenu()");

        HideMenu();

        //switches scene to main menu
        GameManager.gameManager.LoadScene(Scenes.MainMenu);
    }

    void DisplayMenu()
    {
        //display menu
        background.SetActive(true);
        pauseMenu.SetActive(true);

        //pause the game
        Time.timeScale = 0;
    }
    public void HideMenu()
    {
        isDisplayingMenu = false;

        //hide menu
        background.SetActive(false);
        pauseMenu.SetActive(false);

        //unpause the game
        Time.timeScale = 1;
    }
}

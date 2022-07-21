using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        //saves sound settings
        float musicVolumeTemp = PlayerPrefs.GetFloat("MusicVolume");
        float sfxVolumeTemp = PlayerPrefs.GetFloat("SfxVolume");

        //delets all saves
        PlayerPrefs.DeleteAll();

        //sets sound settings
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeTemp);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolumeTemp);

        //saves it to disk
        PlayerPrefs.Save();

        //reloads the Scene
        GameManager.gameManager.LoadScene(Scenes.MainMenu);
    }
}

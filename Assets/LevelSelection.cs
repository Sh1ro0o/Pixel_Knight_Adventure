using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public void LoadLevel1()
    {
        GameManager.gameManager.LoadScene(Scenes.Level1);
    }

    public void LoadLevel2()
    {
        GameManager.gameManager.LoadScene(Scenes.Level2);
    }

    public void LoadLevel3()
    {
        GameManager.gameManager.LoadScene(Scenes.Level3);
    }
}

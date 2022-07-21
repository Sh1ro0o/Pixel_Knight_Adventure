using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    bool level1Complete = false;
    bool level2Complete = false;

    [SerializeField] Button level2Button, level3Button;
    private void Start()
    {
        //checks which levels to display in level selection screen (level 1 complete unlocks level2 and level 2 complete unlocks level3)
        //0 = true, anything else is false
        level1Complete = PlayerPrefs.GetInt("Level1", 1) == 0;
        level2Complete = PlayerPrefs.GetInt("Level2", 1) == 0;

        //enables next level buttons when level before them is completed
        level2Button.gameObject.SetActive(level1Complete);
        level3Button.gameObject.SetActive(level2Complete);

        Debug.Log("Level selection START values " + level1Complete + " " + level2Complete);
    }

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

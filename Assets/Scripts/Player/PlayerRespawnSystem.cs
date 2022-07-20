using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerRespawnSystem : MonoBehaviour
{
    public Vector3 respawnPoint;
    Combatant combatant;
    // Update is called once per frame

    private void Start()
    {
        combatant = GetComponent<Combatant>();

        //scene spawn points
        SetDefaultRespawnPoint();
    }
    void Update()
    {
        if(Input.GetButtonDown("Respawn"))
        {
            if(combatant.IsDead())
            {
                combatant.Revive();
                transform.position = respawnPoint;
            }
        }
    }

    //sets default respawn point based on current scene
    private void SetDefaultRespawnPoint()
    {
        if (gameObject.scene == SceneManager.GetSceneByName("Level1"))
            respawnPoint = new Vector3(-3, -1, 0);
        else if (gameObject.scene == SceneManager.GetSceneByName("Level2"))
            respawnPoint = new Vector3(0, 0, 0);
        else if (gameObject.scene == SceneManager.GetSceneByName("Level3"))
            respawnPoint = new Vector3(-17f, -1f, 0);
        else
            respawnPoint = new Vector3(0, 0, 0);
    }
}

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

        if (gameObject.scene == SceneManager.GetSceneByName("Level1"))
            respawnPoint = new Vector3(-3, -1, 0);
        else if (gameObject.scene == SceneManager.GetSceneByName("Level3"))
            respawnPoint = new Vector3(-16, -1, 0);
        else
            respawnPoint = new Vector3(0, 0, 0);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameManager.gameManager.FinishedLevel();
            AudioManager.audioManager.StopSound(AudioManager.SoundSystem.Player_running);
        }
    }
}

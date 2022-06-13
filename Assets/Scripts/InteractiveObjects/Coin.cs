using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {

            GameManager.gameManager.coins++;
            AudioManager.audioManager.PlaySound(AudioManager.SoundSystem.Coin_pick_up);
            Destroy(gameObject);
        }
    }
}

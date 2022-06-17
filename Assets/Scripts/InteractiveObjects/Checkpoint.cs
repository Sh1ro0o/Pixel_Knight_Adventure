using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool isActivated = false;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite activeFlagSprite;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !isActivated)
        {
            spriteRenderer.sprite = activeFlagSprite;
            isActivated = true;
            collision.GetComponent<PlayerRespawnSystem>().respawnPoint = transform.position;
        }
    }
}

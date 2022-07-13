using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] Boss boss;
    PlayerCombat player;

    [SerializeField] GameObject bars;

    private void Start()
    {
        player = FindObjectOfType<PlayerCombat>();
    }

    private void Update()
    {
        if (player.IsDead())
        {
            boss.isBossActivated = false;
            bars.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            boss.isBossActivated = true;
            bars.SetActive(true);
        }
    }
}

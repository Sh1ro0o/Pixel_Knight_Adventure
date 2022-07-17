using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] Boss boss;
    PlayerCombat player;

    [SerializeField] GameObject bars;

    [SerializeField] CinemachineVirtualCamera vcam;
    CinemachineConfiner vcamConfiner;

    private void Start()
    {
        player = FindObjectOfType<PlayerCombat>();
        vcamConfiner = vcam.GetComponent<CinemachineConfiner>();
    }

    private void Update()
    {
        if (player.IsDead())
        {
            boss.DeactivateBoss();
            bars.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            boss.ActivateBoss();
            bars.SetActive(true);
            vcamConfiner.enabled = true;
        }
    }
}

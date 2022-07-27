using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoClipThroughTraps : MonoBehaviour
{
    bool isNoClipOn = false;
    Rigidbody2D _rigidBody;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("NoClipThroughTraps"))
        {
            isNoClipOn = !isNoClipOn;
            if(isNoClipOn)
            {
                Physics2D.IgnoreLayerCollision(3, 11); //player traps
                Physics2D.IgnoreLayerCollision(3, 14); //player projectile
                Debug.Log("Ignoring collision!");
            }
            else
            {
                Physics2D.IgnoreLayerCollision(3, 11, false); //stop ignoring player traps collision
                Physics2D.IgnoreLayerCollision(3, 14, false); //stop ignoring player projectile collision
                Debug.Log("Detecting collision!");
            }
        }
    }
}

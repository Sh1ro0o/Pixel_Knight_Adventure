using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HorizontalPlatform : MonoBehaviour
{
    [SerializeField] PlatformEffector2D platformEffector;
    bool isDownButtonPressed = false;
    bool DownButtonReleasedFlag = false;
    private CustomLayerCollisionCheck customLayerColisionCheck;

    private void Start()
    {
        customLayerColisionCheck = FindObjectOfType<CustomLayerCollisionCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Down"))
        {
            isDownButtonPressed = true;
        }

        if(Input.GetButtonUp("Down"))
        {
            DownButtonReleasedFlag = true;
        }
    }

    private void FixedUpdate()
    {
        //we bitshift the collidermask to remove the Player layer so on button press down it falls down and then we re-enable it when the button is released
        //for this code to work the Player layer must be Layer 3 (number 8 bitwise)
        if(isDownButtonPressed)
        {
            isDownButtonPressed = false;
            //Debug.Log("Before: " + Convert.ToString(platformEffector.colliderMask, 2).PadLeft(32, '0'));
            platformEffector.colliderMask = platformEffector.colliderMask & (2147483643-8);
            //Debug.Log("After: " + Convert.ToString(platformEffector.colliderMask, 2).PadLeft(32, '0'));
        }
        if(DownButtonReleasedFlag && !customLayerColisionCheck.isPlayerPlatformCollision)
        {
            DownButtonReleasedFlag = false; 
            platformEffector.colliderMask = platformEffector.colliderMask | 8;
        }
    }
}

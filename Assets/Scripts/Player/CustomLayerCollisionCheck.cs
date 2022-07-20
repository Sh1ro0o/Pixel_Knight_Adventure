using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLayerCollisionCheck : MonoBehaviour
{
    //PUBLIC
    [SerializeField] LayerMask platformLayer;
    public bool isPlayerPlatformCollision = false;
    public string platformObjectName;

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        PlayerPlatformCollisionCheck();
    }

    //checks for collision between Player layer and Platform layer
    void PlayerPlatformCollisionCheck()
    {
        //draw a blue ray of the detection raycast
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.7f), transform.TransformDirection(Vector2.up), Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.6f), transform.TransformDirection(Vector2.up), 1f, platformLayer);
        if (hit)
        {
            isPlayerPlatformCollision = true;
            //Debug.Log("Hit!");
        }
        else
            isPlayerPlatformCollision = false;
    }
}

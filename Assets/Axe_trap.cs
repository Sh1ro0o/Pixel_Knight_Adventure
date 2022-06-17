using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_trap : MonoBehaviour

{
    [SerializeField] Transform axeTransform;
    [SerializeField] float rotationSpeed;
    private float rotZ;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(axeTransform.rotation.z);
        rotZ += Time.deltaTime * rotationSpeed;

        axeTransform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<Combatant>().Die();
        }
    }
}

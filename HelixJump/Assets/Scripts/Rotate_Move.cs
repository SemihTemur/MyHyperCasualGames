using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Move : MonoBehaviour
{
    private float rotateSpeed=750.0f;
    private float mouseForce;
    void FixedUpdate()
    {
        if (!Ball.gameover)
        {
            mouseForce = Input.GetAxis("Mouse X");
            if (transform.childCount == 0)
            {
                Destroy(gameObject);
            }

            if (Input.GetMouseButton(0))
            {
                transform.Rotate(Vector3.up, mouseForce * rotateSpeed * Time.deltaTime);
            }
        }
        
    }
}

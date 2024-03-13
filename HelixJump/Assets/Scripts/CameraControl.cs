using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform ball;
    private Vector3 offset;
    void Start()
    {
   
    }

    
    void Update()
    {
        if (!Ball.gameover)
        {
            offset = new Vector3(transform.position.x, ball.transform.position.y, transform.position.z);
            transform.position = offset + new Vector3(0, 0.2f, 0);
        }

    }
}

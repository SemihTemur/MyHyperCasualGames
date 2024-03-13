using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour
{
    public GameObject ball;
    private Ball ballsc;
    private SpawnManager spawnManager;
    public int skor ;
    void Start()
    {
        skor = 0;
        ballsc = GameObject.Find("Ball").GetComponent<Ball>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
            if (transform.position.y > ball.transform.position.y-8)
            {
            ballsc.Skor(25);
            spawnManager.Spawn();
            Destroy(gameObject);

        }

    }

   
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int pointValue;
    public ParticleSystem explosionParticle;
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(),ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }
    
    void Update()
    {
        
    }

    private void OnMouseDown() // objenin ustune týklayýnca yok eder
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    
    }

    private void OnTriggerEnter(Collider other) // kaçýrdýgýmýz objelerýn yere deyýnce yok olmasýný saglar
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))//eger objem Bad tag'ýný tasýmýyorsa ve
        {                                 // yerle temas edýyorsa oyun býtsýn demek
            gameManager.UpdateLives();
        }
    
    }

    // Bu  kod oyuncumun havaya dogru gitmesini saðlýyor.
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    
    // oyuncumun donmesini saglýyo
  float RandomTorque()
    {
        return Random.Range(maxTorque,maxTorque);
    }

    // oyuncumun konumunu belýrlýyor sagda ya da solda doðmasýný falan
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }


}

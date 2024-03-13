using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 0.4f;

    private float ballRandomx;
    private float ballRandomy;
    Vector3 randomPosition;

    private Vector3 spawnBallPosition;
    private GameObject file;
    Bounds  boundsB;

    private GoalKeeperController goalKeeperController;
    private WinController winController;


    private void Awake()
    {
        spawnBallPosition = new Vector3(0.006f, 0.1436152f, 43.91f);
        transform.position = spawnBallPosition;
        file = GameObject.Find("GoalTrigger");
        rb = GetComponent<Rigidbody>();
        boundsB = file.GetComponent<Collider>().bounds;
        goalKeeperController = GameObject.Find("GoalKeeper").GetComponent<GoalKeeperController>();
        winController = GameObject.Find("WinController").GetComponent<WinController>();
       
    }

    void Start()
    {
        if (GameSettings.Instance.isPlaY)
        {
            winController.sounds.clip = winController.tribunSounds;
            winController.sounds.loop = true;
            winController.sounds.Play();
            winController.sounds.mute = false;
        }
        else
        {
            winController.sounds.clip = winController.tribunSounds;
            winController.sounds.loop = true;
            winController.sounds.Play();
            winController.sounds.mute = true;
        }
        StartCoroutine("WaitBallMovement");

    }

    private void Update()
    {

        if (winController.penaltyTaken1 >= 0 || winController.penaltyTaken2 >= 0)
        {
            Winning();
            if (!winController.devamEt)
            {
                Time.timeScale = 0f;
                winController.WinnerPlayer();

                DestroyImmediate(winController.firstBall1);
                DestroyImmediate(winController.firstBall2);
                winController.leftBall.RemoveAt(winController.leftBall.Count - 1);
                winController.rightBall.Remove(winController.rightBall[0]);

                DestroyImmediate(gameObject);
            }
           
        }
   
    }

    IEnumerator WaitBallMovement()
    {
        yield return new WaitForSeconds(3);
        BallMovement();
    }



    //Topa hareket verdiðim kýsým
    private void BallMovement()
    {
        RandomPositionNumber();
        randomPosition = new Vector3(ballRandomx, ballRandomy, transform.position.z);
        rb.AddForce(randomPosition * speed, ForceMode.VelocityChange);
        StartCoroutine("SpawnBall");
    }

    private void RandomPositionNumber()
    {
        ballRandomx = UnityEngine.Random.Range(-17, 17);
        ballRandomy = UnityEngine.Random.Range(0, 15);
    }

  

    // Yeni bir top üretme ve önceki topu  silme
    IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(4);
 
        //Gol Kontrolleri
        isGoal();
        //  kaleci kontrolleri
        GoalKeeperController();

        if (goalKeeperController.kaleciController)
        {
            Sprite player1Image = GameSettings.Instance.imageList1[GameSettings.Instance.count1];
            Instantiate(Resources.Load<GameObject>(player1Image.name), spawnBallPosition, gameObject.transform.rotation);
        }
        else
        {
            Sprite player2Image = GameSettings.Instance.imageList2[GameSettings.Instance.count2];
            Instantiate(Resources.Load<GameObject>(player2Image.name), spawnBallPosition, gameObject.transform.rotation);
        }

        Destroy(gameObject);

    }

    private void isGoal()
    {

        Vector3 ballPosition = transform.position;
            // Top kaledemi depilmi kontrolu
        if (!boundsB.Contains(ballPosition) && goalKeeperController.kaleciController)
        {
                winController.player1Count++;
                winController.player1.text = winController.player1NamePart + winController.player1Count;
        }
       
        else if (!boundsB.Contains(ballPosition) && !goalKeeperController.kaleciController)
        {
                winController.player2Count++;
                winController.player2.text = winController.player2NamePart + winController.player2Count;
               
        }

    }

   

    private void GoalKeeperController()
    {
        // kalecýyý tutma kontrolu ýký kýsýlýk oyunda
        if (goalKeeperController.kaleciController)
        {
            winController.penaltyTaken1--;
            goalKeeperController.kaleciController = false;
            if (winController.leftBall.Count >0)
            {

                if (winController.leftBall.Count == 1)
                {
                    winController.firstBall1 = winController.leftBall[winController.leftBall.Count - 1];
                    winController.firstBall1.gameObject.SetActive(false);
                }
               
                else
                {
                    GameObject lastBall = winController.leftBall[winController.leftBall.Count - 1];

                    //// Listenin son elemanýný kaldýr.
                    winController.leftBall.RemoveAt(winController.leftBall.Count - 1);

                    //// Son elemaný yok et.
                    DestroyImmediate(lastBall);
                }

            }
            
        }
        else
        {
            winController.penaltyTaken2--;
            goalKeeperController.kaleciController = true;

            if (winController.rightBall.Count >0)
            {

                if(winController.rightBall.Count == 1)
                {
                    winController.firstBall2 = winController.rightBall[winController.rightBall.Count - 1];
                    winController.firstBall2.gameObject.SetActive(false);
                }

                else
                {
                    GameObject lastBall2 = winController.rightBall[0];

                    //// Listenin son elemanýný kaldýr.
                    winController.rightBall.Remove(winController.rightBall[0]);

                    //Son elemaný yok et.s
                    DestroyImmediate(lastBall2); ;
                }

            }
           
        }

    }

    private void Winning()
    {
         if (winController.player1Count > winController.player2Count)
         {
            int control1 = winController.penaltyTaken2 + winController.player2Count;
               
            if (control1 < winController.player1Count)
            {
                    winController.devamEt = false;
            }

         }
         else if(winController.player1Count < winController.player2Count)
         {
            int control2 = winController.penaltyTaken1 + winController.player1Count;

            if (control2 < winController.player2Count)
            {
                    winController.devamEt = false;
            }

         }
         else if(winController.player1Count==winController.player2Count && winController.penaltyTaken1==0&&winController.penaltyTaken2==0)
         {  
            winController.penaltyTaken2++;
            winController.penaltyTaken1++;

            winController.firstBall1.gameObject.SetActive(true);

            winController.firstBall2.gameObject.SetActive(true);

        }

    }




    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("GoalNet"))
       {
            rb.velocity = Vector3.zero;

            if (GameSettings.Instance.isPlaY)
            {
                winController.sounds.clip = winController.goalSound;
                winController.sounds.loop = false;
                winController.sounds.Play();
                winController.sounds.mute = false;
            }

       }

       else if (other.gameObject.CompareTag("GoalKeeper"))
       {
            rb.velocity = Vector3.zero;
       }

    }




}

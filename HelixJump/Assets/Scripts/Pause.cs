using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button pauseButton;
    public GameObject panel;
    public Button continueButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GamePause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameContinue()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}

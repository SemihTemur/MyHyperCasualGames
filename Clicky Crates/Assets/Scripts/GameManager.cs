using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Slider volumeSlider;
    public AudioSource audioSource;
    private int score;
    private int lives;
    public bool isGameActive;
    public Button restartButton;
    private GameObject titleScreen;
    public GameObject pauseScreen;
    private bool paused;

    void Start()
    {
        titleScreen = GameObject.Find("Title Screen");
    }
      public void StartGame(int difficulty)
    {
        audioSource.volume = volumeSlider.value;
        spawnRate = difficulty;
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        lives = 3;
        UpdateScore(score);
        UpdateLives();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive) // sonsuz donguye gýrer ve sureklý obje spamlar
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
           
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score :" + score;
    }
    public void UpdateLives()
    {
        if (lives==0)
        {
            livesText.text = "Lives :" + lives;
            GameOver();
        }
        else if(lives>0)
        {
            livesText.text = "Lives :" + lives;
        }
        lives = lives - 1;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnSliderValueChanged()
    {
        // Slider deðeri ses þiddetini temsil ediyor, bu nedenle sesi güncelleyin.
        audioSource.volume = volumeSlider.value;
    }
    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

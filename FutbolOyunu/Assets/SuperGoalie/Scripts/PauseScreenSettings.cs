using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreenSettings : MonoBehaviour
{

    private Button pauseButton;
    public Button continueButton;
    public Button homeButton;
    public Button restartButton;
    public RawImage pauseScreen;

    void Start()
    {
        Time.timeScale = 1f;
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();

        pauseButton.onClick.AddListener(delegate {GamePause();});
        continueButton.onClick.AddListener(delegate {GameContinue();});
        homeButton.onClick.AddListener(delegate { OpenMenu(); });
        restartButton.onClick.AddListener(delegate { RestartGame(); });
    }


    public void GamePause()
    {
        Time.timeScale = 0f;
        pauseScreen.gameObject.SetActive(true);
    }

    private void GameContinue()
    {
        Time.timeScale = 1f;
        pauseScreen.gameObject.SetActive(false);
    }

    private void OpenMenu()
    {
        SceneManager.LoadScene(0);

        if (Menu.Instance.isPlay)
        {
            Menu.Instance.audioSource.Play();
        }

    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

}

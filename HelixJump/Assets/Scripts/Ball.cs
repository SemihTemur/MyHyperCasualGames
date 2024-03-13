using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Rigidbody ballRb;
    public GameObject splashprefab;
    public Button startButton;
    private int skore;
    public Text scoreText;
    public Text gameOverText;
    private float speed = 4.0f;
    public static bool gameover;
    private void Start()
    {
        SaveManager.Instance.LoadScore();
        gameover = false;
        skore = 0;
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameover)
        {
            string materialname = collision.gameObject.GetComponent<MeshRenderer>().material.name;
            if (materialname == "UnSafe Color (Instance)")
            {
                gameover = true;
                gameOverText.gameObject.SetActive(true);
                StartCoroutine(restart());
            }
            else
            {
                ballRb.velocity = Vector3.up * speed;
                GameObject splash = Instantiate(splashprefab, transform.position + new Vector3(0, -0.150f, 0), splashprefab.transform.rotation);
                splash.transform.SetParent(collision.gameObject.transform);
            }
        }
    }

    public void Skor(int skor)
    {
        skore += skor;
        SaveManager.Instance.SaveScore(skore);
        scoreText.text = skore.ToString();
    }

    IEnumerator restart()
    {
        yield return new WaitForSeconds(2);
        startButton.gameObject.SetActive(true);
        Destroy(gameOverText.gameObject);
    }

    public void StartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}

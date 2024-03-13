using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    public Text player1;
    public Text player2;

    // player1ýn ve player2 text kýsmýndaký ýsýmý tutuyorum burda çunku ismi tutmasam deðerleri güncelleyemem
    public string player1NamePart;
    public string player2NamePart;

    // Gol sayýlarý
    public int player1Count;
    public int player2Count;

    // Atýlýcak Penaltý sayýlarý
    public int penaltyTaken1;
    public int penaltyTaken2;

    public bool devamEt;

    public List<GameObject> leftBall = new List<GameObject>();
    public List<GameObject> rightBall = new List<GameObject>();

    public GameObject soldakiTop;
    public GameObject sagdakiTop;

    Sprite gameObjects1, gameObjects2;
    Color renk;

    private Vector3 spawnBallRotation;
    private GoalKeeperController goalKeeperController;

    public Text winnerPlayer;

    private PauseScreenSettings pauseScreenSettings;

    public AudioSource sounds;

    public AudioClip goalSound, tribunSounds;

    public GameObject firstBall1, firstBall2;

    public Sprite playSound, stopSound;

    public Button soundSettings;

    private void Awake()
    {
        goalKeeperController = GameObject.Find("GoalKeeper").GetComponent<GoalKeeperController>();
        gameObjects1 = GameSettings.Instance.imageList1[GameSettings.Instance.count1];
        gameObjects2 = GameSettings.Instance.imageList2[GameSettings.Instance.count2];
        pauseScreenSettings = GameObject.Find("PauseScreen").GetComponent<PauseScreenSettings>();
        
        foreach (Transform child in soldakiTop.transform)
        {
            child.gameObject.GetComponent<Image>().sprite = gameObjects1;
            leftBall.Add(child.gameObject);
        }
        foreach (Transform child in sagdakiTop.transform)
        {
            child.gameObject.GetComponent<Image>().sprite = gameObjects2;
            rightBall.Add(child.gameObject);
        }

        if (!GameSettings.Instance.isPlaY)
        {
            soundSettings.GetComponent<Image>().sprite = stopSound;
            sounds.mute = true;
        }
        else
        {
            soundSettings.GetComponent<Image>().sprite = playSound;
            sounds.mute = false;
        }


    }

    void Start()
    {
        spawnBallRotation = new Vector3(-0.017f, 0.139f, 43.996f);

        Instantiate(Resources.Load<GameObject>(gameObjects1.name), spawnBallRotation, gameObject.transform.rotation);

        if (ColorUtility.TryParseHtmlString(gameObjects1.name, out renk))
        {
            player1.color = renk;
        }
        if (ColorUtility.TryParseHtmlString(gameObjects2.name, out renk))
        {
            player2.color = renk;
        }

        player1.text = GameSettings.Instance.player1name;
        player2.text = GameSettings.Instance.player2name;

        player1NamePart = player1.text + " : ";
        player2NamePart = player2.text + " : ";

        player1.text += " : " + 0;
        player2.text += " : " + 0;

        player1Count = 0;
        player2Count = 0;

        penaltyTaken1 = 5;
        penaltyTaken2 = 5;

        devamEt = true;

        soundSettings.onClick.AddListener(delegate {SoundSettings();});

    }


    public void WinnerPlayer()
    {
        if (player1Count > player2Count)
        {
            winnerPlayer.gameObject.SetActive(true);
            winnerPlayer.color = player1.color;
            winnerPlayer.text = GameSettings.Instance.player1name + " Kazandý";
            StartCoroutine("WaitWinnerPlayer");

        }
        else
        {
            winnerPlayer.gameObject.SetActive(true);
            winnerPlayer.color = player2.color;
            winnerPlayer.text = GameSettings.Instance.player2name + " Kazandý";
            StartCoroutine("WaitWinnerPlayer");
        }
    }


    public IEnumerator WaitWinnerPlayer()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(4);
        pauseScreenSettings.GamePause();
    }


    public void SoundSettings()
    {

        if (GameSettings.Instance.isPlaY)
        {
            soundSettings.GetComponent<Image>().sprite = stopSound;
            GameSettings.Instance.isPlaY = false;
            sounds.mute = true;
        }
        else
        {
            soundSettings.GetComponent<Image>().sprite = playSound;
            GameSettings.Instance.isPlaY = true;
            sounds.mute = false;
        }

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettingsEvents : MonoBehaviour
{

    public Button audýoImage2;

    private Sprite sprite;

    public Image BallImage1;
    public InputField player1Name;

    public Image BallImage2;
    public InputField player2Name;

    public Text errorMessage;



    void Start()
    {

        player1Name.text = GameSettings.Instance.player1name;
        player2Name.text = GameSettings.Instance.player2name;

        BallImage1.sprite = GameSettings.Instance.imageList1[GameSettings.Instance.count1];
        BallImage2.sprite = GameSettings.Instance.imageList2[GameSettings.Instance.count2];
       
        if (Menu.Instance.isPlay)
        {
            audýoImage2.GetComponent<Button>().GetComponent<Image>().sprite = Menu.Instance.soundOn;
        }
        else
        {
            audýoImage2.GetComponent<Button>().GetComponent<Image>().sprite = Menu.Instance.soundOff;
        }

    }

    


    public void Increase1()
    {
        if (GameSettings.Instance.count1 < 4)
        {
            GameSettings.Instance.count1++;
            BallImage1.sprite = GameSettings.Instance.imageList1[GameSettings.Instance.count1];
        }
        else
        {
            GameSettings.Instance.count1 = 0;
            BallImage1.sprite = GameSettings.Instance.imageList1[GameSettings.Instance.count1];
        }
    }

    public void Decrease1()
    {
        
        if (GameSettings.Instance.count1 > 0)
        {
            GameSettings.Instance.count1--;
            BallImage1.sprite = GameSettings.Instance.imageList1[GameSettings.Instance.count1];
        }
        else
        {
            GameSettings.Instance.count1 = 4;
            BallImage1.sprite = GameSettings.Instance.imageList1[GameSettings.Instance.count1];
        }

    }

    public void Increase2()
    {
       
        if (GameSettings.Instance.count2 < 4)
        {
            GameSettings.Instance.count2++;
            BallImage2.sprite = GameSettings.Instance.imageList2[GameSettings.Instance.count2];
        }
        else
        {
            GameSettings.Instance.count2 = 0;
            BallImage2.sprite = GameSettings.Instance.imageList2[GameSettings.Instance.count2];
        }

    }

    public void Decrease2()
    {

        if (GameSettings.Instance.count2 > 0)
        {
            GameSettings.Instance.count2--;
            BallImage2.sprite = GameSettings.Instance.imageList2[GameSettings.Instance.count2];
        }
        else
        {
            GameSettings.Instance.count2 = 4;
            BallImage2.sprite = GameSettings.Instance.imageList2[GameSettings.Instance.count2];
        }

    }

    public void audýoImageSettings()
    {
        sprite = audýoImage2.GetComponent<Button>().GetComponent<Image>().sprite;

        if (sprite== Menu.Instance.soundOn)
        {
            Menu.Instance.isPlay = false;
            Menu.Instance.audioSource.mute = true;
            sprite = Menu.Instance.soundOff;
        }
        else
        {
            Menu.Instance.audioSource.mute = false;
            Menu.Instance.isPlay = true;
            sprite = Menu.Instance.soundOn;
        }

        audýoImage2.GetComponent<Button>().GetComponent<Image>().sprite = sprite;

    }




    public void StartGame()
    {
        GameSettings.Instance.player1Image = BallImage1;
        GameSettings.Instance.player2Image = BallImage2;
        
        if(GameSettings.Instance.player1name.Length==0&& GameSettings.Instance.player2name.Length == 0)
        {
            GameSettings.Instance.player1name = player1Name.text;
            GameSettings.Instance.player2name = player2Name.text;
        }

        if(GameSettings.Instance.player1Image.sprite != GameSettings.Instance.player2Image.sprite)
        {
            if(GameSettings.Instance.player1name.Length == 0)
            {
                GameSettings.Instance.player1name = "Player1";
                
            }
            if(GameSettings.Instance.player2name.Length == 0)
            {
                GameSettings.Instance.player2name = "Player2";
            }
            Menu.Instance.audioSource.Stop();
            SceneManager.LoadScene(2);
        }
        else
        {
            errorMessage.gameObject.SetActive(true);
            StartCoroutine(ErrorSeconds());  
        }

   
    }

    public void Back()
    {
        GameSettings.Instance.player1name = player1Name.text;
        GameSettings.Instance.player2name = player2Name.text;

        SceneManager.LoadScene(0);
    }


    IEnumerator ErrorSeconds()
    {
        yield return new WaitForSeconds(4);
        errorMessage.gameObject.SetActive(false);
    }

}

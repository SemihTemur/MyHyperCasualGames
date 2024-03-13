using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEvents : MonoBehaviour
{
    public Button audýoImage;


    private Sprite sprite;
    private void Start()
    {
       
        if (Menu.Instance.isPlay)
        {
            audýoImage.GetComponent<Button>().GetComponent<Image>().sprite = Menu.Instance.soundOn;
        }
        else
        {
            audýoImage.GetComponent<Button>().GetComponent<Image>().sprite = Menu.Instance.soundOff;
        }

    }


    public void OpenGameSettings()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(3);
    }

    public void AudýoImageSettings()
    {
        sprite = audýoImage.GetComponent<Button>().GetComponent<Image>().sprite;

        if (sprite == Menu.Instance.soundOn)
        {
            audýoImage.GetComponent<Button>().GetComponent<Image>().sprite = Menu.Instance.soundOff;
            Menu.Instance.isPlay = false;
            Menu.Instance.audioSource.mute=true;
        }
        else
        {
            audýoImage.GetComponent<Button>().GetComponent<Image>().sprite = Menu.Instance.soundOn;
            Menu.Instance.isPlay = true;
            Menu.Instance.audioSource.mute = false;
        }



    }

}

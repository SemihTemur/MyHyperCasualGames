using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public AudioSource audioSource;

    public Sprite soundOn, soundOff;



    public bool isPlay;
  

    // Sýnglenton
    public static Menu Instance;



    private void Awake()
    {
        // Eðer bir önceki örnek varsa, bu örneði yok etme
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            isPlay = true;
            // Bu nesneyi diðer sahnelerde yok etme
            DontDestroyOnLoad(gameObject);
        }
    }


 

}

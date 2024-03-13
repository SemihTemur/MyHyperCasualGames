using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    // 1.Oyuncu için
    public List<Sprite> imageList1 = new List<Sprite>();
    public int count1;
    public string player1name;
    public Image player1Image;  

    // 2.Oyuncu için
    public List<Sprite> imageList2 = new List<Sprite>();
    public int count2;
    public string player2name;
    public Image player2Image;

    public bool isPlaY;

    // Sýnglenton
    public static GameSettings Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            count1 = 0;
            count2 = 0;
            DontDestroyOnLoad(gameObject);
        }

        isPlaY = true;

    }



   


   

}

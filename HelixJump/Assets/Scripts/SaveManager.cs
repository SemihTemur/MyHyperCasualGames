using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public Text bestscoreText;
    private static int bestskor;
  public static  SaveManager Instance { get; private set;}
    void Awake()
    {
        LoadScore();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [System.Serializable]

    class SaveData
    {
        public int skor;
    }

    public void SaveScore(int bestscore)
    {
        SaveData data = new SaveData();
        if (bestskor < bestscore)
        {
            data.skor = bestscore;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    
        
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestskor = data.skor;
            bestscoreText.text = "Best:" + bestskor;
        }
    }
    
}

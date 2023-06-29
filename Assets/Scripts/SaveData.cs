using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveData : MonoBehaviour
{
    public static SaveData Instance;
    public string PlayerName;
    public int Score;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("multiple instance");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class HighRecord
    {
        public string playerName;
        public int score;
    }

    public void Save()
    {
        HighRecord newRecord = new HighRecord();
        newRecord.playerName = this.PlayerName;
        newRecord.score = this.Score;
        string json = JsonUtility.ToJson(newRecord);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        Debug.Log(path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighRecord newRecord = JsonUtility.FromJson<HighRecord>(json);
            PlayerName = newRecord.playerName;
            Score = newRecord.score;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class ScoreManager : MonoBehaviour
{
    public long HighScore;
    public long LevelScore;

    BinaryFormatter Formatter = new BinaryFormatter();

    private void Awake()
    {
        DontDestroyOnLoad(this);

        LoadScores();
    }

    void LoadScores()
    {
        string path = Application.persistentDataPath + "/Score.sav";

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            HighScore = (long)Formatter.Deserialize(stream);
            stream.Close();
        }

        else
        {
            SaveScores();
        }
    }

    public void SaveScores()
    {
        string path = Application.persistentDataPath + "/Score.sav";

        FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        Formatter.Serialize(stream, HighScore);
        stream.Close();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

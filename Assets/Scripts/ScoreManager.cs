using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static long HighScore;
    public static long LevelScore;
    [SerializeField] private Text HighScoreText;
    [SerializeField] private Text LevelScoreText;

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

        HighScoreText.text = "High Score: " + HighScore;
    }

    public void SaveScores()
    {
        string path = Application.persistentDataPath + "/Score.sav";

        FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        Formatter.Serialize(stream, HighScore);
        stream.Close();
    }
	
    public void UpdateScores(int Score)
    {
        LevelScore += Score;
        LevelScoreText.text = "Score: " + LevelScore;

        if (LevelScore > HighScore)
        {
            HighScore = LevelScore;
            HighScoreText.text = "High Score: " + HighScore;
        }
    }
}

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

/// <summary>
/// This script saves and loads the high score. This script also updates the high score if the level score is greater than the high score.
/// </summary>

public class ScoreManager : MonoBehaviour
{
    public static long HighScore;
    public static long LevelScore;

    static BinaryFormatter Formatter = new BinaryFormatter();

    private void Awake()
    {
        LoadScores();
    }

    public static void LoadScores()
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

    public static void SaveScores()
    {
        string path = Application.persistentDataPath + "/Score.sav";

        FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        Formatter.Serialize(stream, HighScore);
        stream.Close();
    }
	
    public static void UpdateScores(int Score)
    {
        LevelScore += Score;

        if (LevelScore > HighScore)
        {
            HighScore = LevelScore;
        }

        PlayerScore.ScoreInstance.UpdateScore();
    }
}

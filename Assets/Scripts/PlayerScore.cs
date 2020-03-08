using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script keeps the level score and the high score up to date.
/// </summary>

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private Text Score;
    [SerializeField] private Text HighScore;

    public static PlayerScore ScoreInstance = null;

    private void Awake()
    {
        if (ScoreInstance == null)
        {
            ScoreInstance = this;
        }

        if (ScoreInstance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScore();
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        Score.text = "Score: " + ScoreManager.LevelScore.ToString();
        HighScore.text = "High Score: " + ScoreManager.HighScore.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public RepeatingBackground background;
    public ColumnPool columns;

    public Text ScoreText;
    private int score;

    public Text HighscoreText;
    private int highscore;

    public float scrollSpeed = -1.5f;
    public float numberOfSceneryTiles;
    public float repositionOffset;

    [HideInInspector]
    public bool IsGameOver;
    [HideInInspector]
    public bool IsGameStarted = true;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerPrefs.SetInt("Highscore", 0);
    }

    void Start ()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
            HighscoreText.text = highscore.ToString();
        }
	}

    public void GameReset()
    {
        score = 0;
        ScoreText.text = score.ToString();

        background.BackgroundReset();
        columns.ColumnReset();
    }

    public void AddToScore()
    {
        score++;
        ScoreText.text = score.ToString();

        if(score > highscore)
        {
            highscore = score;
            HighscoreText.text = highscore.ToString();
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("Highscore", highscore);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Highscore", highscore);
    }
}

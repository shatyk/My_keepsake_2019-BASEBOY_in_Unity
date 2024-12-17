using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int gameScore, gameScoreFail;
    private bool isGameOver;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreTextFail;
    [SerializeField] private BoxCollider2D trigger;

    // Start is called before the first frame update
    void Start()
    {
        gameScore = 0;
        gameScoreFail = 0;
        isGameOver = false;
        scoreText.text = gameScore.ToString();
        scoreTextFail.text = gameScoreFail.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incGameScore(int count)
    {
        if (!isGameOver)
        {
            gameScore += count;
            scoreText.text = gameScore.ToString();
        }
    }

    public void incGameScoreFail(int count)
    {
        if (!isGameOver)
        {
            gameScoreFail += count;
            scoreTextFail.text = gameScoreFail.ToString();
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            trigger.enabled = false;
            scoreText.text = ( gameScore.ToString() );
            scoreText.color = Color.red;
        }
    }
}


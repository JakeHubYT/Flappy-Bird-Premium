using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static float highScore = 0;
    public TextMeshProUGUI highScoreText;


    public float pipeMoveSpeed = 5;

    public GameObject startGameScreen;
    public GameObject menuUi;


  


    public bool playingMusic = false;
    public AudioClip gamePlauMusic;

    public TextMeshProUGUI[] scoreText;


    public float score;
    bool gameStarted = false;


    bool gotHighScore= false;

    private void OnEnable()
    {
        Actions.OnCollectPoint += AddToScore;
    

    }
    private void OnDisable()
    {
        Actions.OnCollectPoint -= AddToScore;
      


    }




    private void Start()
    {
        highScoreText.text = "HIGH SCORE = " + highScore.ToString();
        UpdateScoreUi();

        startGameScreen.SetActive(true);
        Time.timeScale = 0;
        gotHighScore = false;

    }

    private void Update()
    {
        #region Commands

        if(Input.GetKeyDown(KeyCode.R)) 
        {
            highScore = 0;
        }

        #endregion
        if (highScore < score)
        {
            highScore = score;
            highScoreText.text = "HIGH SCORE = " + highScore.ToString();

            if(!gotHighScore) 
            {
                gotHighScore= true;
                Actions.OnNewHighScore();
            }
            //new High Score effects
            //play particles
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) && !gameStarted)
        {
            startGameScreen.SetActive(false);
            Time.timeScale = 1;
        }

        if (!menuUi.gameObject.activeSelf && playingMusic == false) 
        {
            AudioManager.Instance.PlayMusic(gamePlauMusic);
            playingMusic = true;
        }
        else if (menuUi.gameObject.activeSelf && playingMusic == true) 
        {
            playingMusic = false;
            AudioManager.Instance.FadeOut(3);
           
        }
    }
    void AddToScore()
    {
        score++;
        UpdateScoreUi();
    }

    void UpdateScoreUi()
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = score.ToString();
        }
      
    }

  



}

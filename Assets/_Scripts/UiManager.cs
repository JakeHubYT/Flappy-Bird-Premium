using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UiManager : MonoBehaviour
{
    public GameObject deathScreen;
    public TextMeshProUGUI[] scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText = null;
    [SerializeField] private GameObject startGameScreen = null;

    public static UiManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Actions.OnPlayerDeath += EnableDeathScreen;
    }
    private void OnDisable()
    {
        Actions.OnPlayerDeath -= EnableDeathScreen;
    }

    private void Start()
    {
        DisableDeathScreen();

       
    }

    public void UpdateHighScoreUi()
    {
        highScoreText.text = "HIGH SCORE = " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void EnableDeathScreen()
    {
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }
    }

    void DisableDeathScreen()
    {
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }

    }

    public void UpdateScoreUi(float score)
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = score.ToString();
        }

    }


    public void EnableStartScreen()
    {
        startGameScreen.SetActive(true);
    }

    public void DisableStartScreen()
    {
        startGameScreen.SetActive(false);
    }
}

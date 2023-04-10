
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    
    public Animator highScoreTxtAnim;
    public Animator highScoreScreenAnim;


    public AudioClip highScoreScound;
    public int score = 0;
    private bool gameStarted = false;
    private bool gotHighScore = false;

    #region SINGLETON
    public static GameManager Instance { get; private set; }

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

    #endregion

    #region SUBSCRIBE TO EVENTS
    private void OnEnable()
    {
        Actions.OnCollectPoint += AddToScore;
    }

    private void OnDisable()
    {
        Actions.OnCollectPoint -= AddToScore;
    }
    #endregion

    private void Start()
    {
       // PlayerPrefs.SetInt("HighScore", 0);


        UiManager.Instance.UpdateScoreUi(score);

        UiManager.Instance.EnableStartScreen();
        Time.timeScale = 0;
        gotHighScore = false;
  
    }

    private void Update()
    {
        HandleInput();
        UpdateHighScore();

        highScoreScreenAnim.SetBool("GotHighScore", gotHighScore);
    }

    private void HandleInput()
    {
        if (!gameStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            StartGame();
            Actions.OnClickStartScreen();
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        UiManager.Instance.DisableStartScreen();
        Time.timeScale = 1;
    }

    #region Score Managment
    private void UpdateHighScore()
    {
       


        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            Debug.Log("UpdateHighScore");
         
            PlayerPrefs.SetInt("HighScore", score);
         


            UiManager.Instance.UpdateHighScoreUi();

            if (!gotHighScore)
            {
                gotHighScore = true;

               

                highScoreTxtAnim.SetTrigger("HighScore");
                AudioManager.Instance.PlaySound(highScoreScound);
                Actions.OnNewHighScore();

            }

           
           

            // TODO: Trigger new high score effects (e.g. particles)
        }
    }

    private void AddToScore()
    {
        score++;
        UiManager.Instance.UpdateScoreUi(score);
    }

    #endregion


    #region Scene Stuff
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}

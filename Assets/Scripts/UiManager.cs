using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject deathScreen;

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

    void EnableDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    void DisableDeathScreen()
    {
        deathScreen.SetActive(false);

    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuiitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject menuUi;
    public GameObject shopUi;

    public AudioClip gamePlauMusic;
    public AudioClip shopMusic;


    public bool playingMusic = false;


    private void Start()
    {
        AudioManager.Instance.PlayMusic(gamePlauMusic);
    }

    void Update()
    {
        if (shopUi.gameObject.activeSelf && playingMusic == false)
        {
            AudioManager.Instance.PlayMusic(shopMusic);
            playingMusic = true;
        }
        else if (!shopUi.gameObject.activeSelf && playingMusic == true)
        {
            AudioManager.Instance.PlayMusic(gamePlauMusic);
            playingMusic = false;
        }
    }
}

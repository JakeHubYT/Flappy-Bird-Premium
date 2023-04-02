using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
  
    public AudioSource musicSource;
    bool playingMusic = false;


    float startVolume;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

       
        startVolume = musicSource.volume;

    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        playingMusic = true;

        musicSource.volume = 0;

        musicSource.volume = startVolume;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();

    }

    public void StopMusic()
    {
        playingMusic = false;

        musicSource.Stop();
    }

    public void FadeOut(float duration)
    {
        Debug.Log("Fading");

        playingMusic = false;


        StartCoroutine(FadeOutCoroutine(duration));


        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {

         if(playingMusic) { yield break; }

        startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            if (playingMusic) { yield break; }

            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }
}



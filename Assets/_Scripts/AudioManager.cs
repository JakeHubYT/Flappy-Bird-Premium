using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
  
    public AudioSource musicSource;
    public AudioSource longSFXSource;
    public AudioSource sfxSource;
    public AudioSource sfxSecondarySource;


    bool playingMusic = false;


    float startVolume;
    public Coroutine fadeCoroutine;

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

    public void PlaySound(AudioClip clip, float pitch = 1, bool audioSource2 = false)
    {
        if(!audioSource2)
        {
            sfxSource.PlayOneShot(clip);
            sfxSource.pitch = pitch;
        }
        else if (audioSource2)
        {
            sfxSecondarySource.PlayOneShot(clip);
            sfxSecondarySource.pitch = pitch;
        }

    }

    public void PlayMusic(AudioClip clip, bool sFX = false, bool loop = false, float volume = 1)
    {
        AudioSource thisSource;
        if (sFX == true) { thisSource = longSFXSource; }
        else { thisSource = musicSource; }
     

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        playingMusic = true;

        thisSource.volume = 0;

        thisSource.volume = volume;
        thisSource.clip = clip;
        thisSource.loop = loop;
        thisSource.Play();

    }

    public void StopMusic()
    {
        playingMusic = false;

        musicSource.Stop();
    }

    public void FadeOut(float duration, bool sFX = false)
    {
        AudioSource thisSource;

        if (sFX == true) { thisSource = longSFXSource; }
        else { thisSource = musicSource; }

      //  Debug.Log("Fading");

        playingMusic = false;


        StartCoroutine(FadeOutCoroutine(duration, thisSource));


        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
    }

    private IEnumerator FadeOutCoroutine(float duration, AudioSource sourceToFade)
    {

         if(playingMusic) { yield break; }

        startVolume = sourceToFade.volume;

        while (sourceToFade.volume > 0)
        {
            if (playingMusic) { yield break; }

            sourceToFade.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        sourceToFade.Stop();
        sourceToFade.volume = startVolume;
    }
}



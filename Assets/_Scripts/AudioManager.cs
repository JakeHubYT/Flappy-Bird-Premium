using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource longSFXSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource sfxSecondarySource;
    [SerializeField] public AudioClip dieSound;
    [SerializeField] public AudioClip hitMetal;


    private Coroutine fadeCoroutine;
    private bool playingMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        playingMusic = false;
    }


    private void OnEnable()
    {
        Actions.OnPlayerDeath += StopVfx;
    }
    private void OnDisable()
    {
        Actions.OnPlayerDeath -= StopVfx;

    }



    public void PlaySound(AudioClip clip, float pitch = 1, bool useSecondarySource = false)
    {
        var source = useSecondarySource ? sfxSecondarySource : sfxSource;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        source.PlayOneShot(clip);
        source.pitch = pitch;
    }

    public void PlayMusic(AudioClip clip, bool isSFX = false, bool loop = false, float volume = 1)
    {
        var source = isSFX ? longSFXSource : musicSource;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        playingMusic = true;

        source.volume = 0;
        source.volume = volume;
        source.clip = clip;
        source.loop = loop;
        source.Play();
    }

    public void StopMusic()
    {
        playingMusic = false;
        musicSource.Stop();
    }

    public void FadeOut(float duration, bool isSFX = false)
    {
        var source = isSFX ? longSFXSource : musicSource;

        playingMusic = false;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeOutCoroutine(duration, source));
    }

    private IEnumerator FadeOutCoroutine(float duration, AudioSource sourceToFade)
    {
        if (playingMusic) { yield break; }

        var startVolume = sourceToFade.volume;

        while (sourceToFade.volume > 0)
        {
            if (playingMusic) { yield break; }

            sourceToFade.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        sourceToFade.Stop();
        sourceToFade.volume = startVolume;
    }

    public void PlayHitMetalSound()
    {
        PlaySound(hitMetal);
    }

    void StopVfx()
    {
        FadeOut(1, true);
    }
}

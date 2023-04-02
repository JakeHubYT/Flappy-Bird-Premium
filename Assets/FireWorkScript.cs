using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorkScript : MonoBehaviour
{
    public ParticleSystem[] fireWork;
    public AudioClip fireWorkSound;

    private void OnEnable()
    {
        Actions.OnNewHighScore += StartCoroutine;
    }
    private void OnDisable()
    {
        Actions.OnNewHighScore -= StartCoroutine;

    }

    private void StartCoroutine()
    {
        StartCoroutine(ExecuteFunctionRandomly());
    }

    IEnumerator ExecuteFunctionRandomly()
    {
        float waitTime = Random.Range(0f, 1.5f);
        yield return new WaitForSeconds(waitTime);

        // Call your function here
        LaunchFireWork();

       // StartCoroutine(ExecuteFunctionRandomly());
    }


    void LaunchFireWork()
    {


        for (int i = 0; i < fireWork.Length; i++)
        {
            fireWork[i].Play();
        }

        AudioManager.Instance.PlaySound(fireWorkSound);
    }
}

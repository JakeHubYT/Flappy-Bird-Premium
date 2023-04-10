using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorkDaddy : MonoBehaviour
{


    public GameObject[] fireWorks;
    private void OnEnable()
    {
        Actions.OnNewHighScore += StartFireworkCoroutine;
    }
    private void OnDisable()
    {
        Actions.OnNewHighScore -= StartFireworkCoroutine;

    }


 


 

    private void StartFireworkCoroutine()
    {
        StartCoroutine(LaunchFireworks(.15f));
    }

    IEnumerator LaunchFireworks(float secondsInbetween)
    {
        for (int i = 0; i < fireWorks.Length; i++)
        {
            fireWorks[i].GetComponent<FireWorkScript>().LaunchFireWork();
            yield return new WaitForSeconds(secondsInbetween);
        }
       

        
    }
}

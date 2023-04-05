using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpBackgroundScript : MonoBehaviour
{

    public GameObject spedUpBG;



    private void OnEnable()
    {
        Actions.OnSkipStart += IncreaseBGSpeed;
        Actions.OnSkipStartEnd += ResetSpeed;

    }
    private void OnDisable()
    {
        Actions.OnSkipStart -= IncreaseBGSpeed;
        Actions.OnSkipStartEnd -= ResetSpeed;


    }

    private void Start()
    {
        spedUpBG.SetActive(false);
    }

    void IncreaseBGSpeed()
    {
        spedUpBG.SetActive(true);

    }

    void ResetSpeed()
    {
        spedUpBG.SetActive(false);

    }
}

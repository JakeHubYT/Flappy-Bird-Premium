using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyIncreaseSpeed : MonoBehaviour
{
    public Material thisMat;

    public float startValue = 0.1332262f;
    public float multiplier = 1f;
   

    public float maxValue;

    public float currentValue;


    // Start is called before the first frame update
    void Start()
    {
        maxValue = startValue * 2;   

        thisMat.SetFloat("_ScrollSpeed", startValue);
        currentValue = startValue;
    }

    private void FixedUpdate()
    {
        IncreaseValue();
    }


    private void IncreaseValue()
    {
        float newValue = currentValue * multiplier;
        if (newValue > maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            currentValue = newValue;
        }



        thisMat.SetFloat("_ScrollSpeed", currentValue);
    
    }
}

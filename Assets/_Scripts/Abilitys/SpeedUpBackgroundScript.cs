using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpBackgroundScript : MonoBehaviour
{
    public Material[] materials; // array of materials to modify
    private static float[] originalScrollSpeeds; // static array to store original _ScrollSpeed values
    private bool isDoubled = false; // flag to track whether scroll speed is currently doubled

    private void OnEnable()
    {
        Actions.OnSkipStart += DoubleScrollSpeeds;
        Actions.OnSkipStartEnd += DecreaseScrollSpeed;
    }

    private void OnDisable()
    {
        Actions.OnSkipStart -= DoubleScrollSpeeds;
        Actions.OnSkipStartEnd -= DecreaseScrollSpeed;
    }

    private void Start()
    {
        SetOriginalScrollSpeeds();
        ResetScrollSpeeds();
    }

    private void SetOriginalScrollSpeeds()
    {
        // store the original _ScrollSpeed values if they haven't been set yet
        if (originalScrollSpeeds == null)
        {
            originalScrollSpeeds = new float[materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                originalScrollSpeeds[i] = materials[i].GetFloat("_ScrollSpeed") * 1.5f;
            }
        }
    }

    private void ResetScrollSpeeds()
    {
        // reset to original _ScrollSpeed values
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_ScrollSpeed", originalScrollSpeeds[i]);
        }
        isDoubled = false;
    }

    private void DoubleScrollSpeeds()
    {
        // double the scroll speed
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_ScrollSpeed", originalScrollSpeeds[i] * 2);
        }
        isDoubled = true;
    }

    private void DecreaseScrollSpeed()
    {
        // if the scroll speed is currently doubled, decrease it gradually
        if (isDoubled)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                float currentScrollSpeed = materials[i].GetFloat("_ScrollSpeed");
                float newScrollSpeed = Mathf.Lerp(currentScrollSpeed, originalScrollSpeeds[i], Time.deltaTime * 3f);
                materials[i].SetFloat("_ScrollSpeed", newScrollSpeed);
            }
        }
    }
}









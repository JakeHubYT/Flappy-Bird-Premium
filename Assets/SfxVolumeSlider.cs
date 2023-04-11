using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxVolumeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = AudioManager.preferedSFXVolume;


    }

    public void OnSFXSliderValueChanged(float value)
    {
        AudioManager.Instance.UpdateSFXVolume(value);
    }
}

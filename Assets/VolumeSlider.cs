using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = AudioManager.preferedMusicVolume;


    }

    public void OnSFXSliderValueChanged(float value)
    {
        AudioManager.Instance.UpdateSFXVolume(value);
    }
    public void OnMusicSliderValueChanged(float value)
    {
        AudioManager.Instance.UpdateMusicVolume(value);
    }
 
}
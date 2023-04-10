using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
       
    }

    public void OnMusicSliderValueChanged(float value)
    {
        AudioManager.Instance.UpdateMusicVolume(value);
    }
    public void OnSFXSliderValueChanged(float value)
    {
        AudioManager.Instance.UpdateSFXVolume(value);
    }
}
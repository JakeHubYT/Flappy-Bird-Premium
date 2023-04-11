using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanelAudio : MonoBehaviour
{

    [SerializeField] public AudioClip popIn;
    [SerializeField] public AudioClip click;
    [SerializeField] public AudioClip money;
    [SerializeField] public AudioClip victory;

    public void PopInSound() => AudioManager.Instance.PlaySound(popIn);

    public void ClickSound() => AudioManager.Instance.PlaySound(click);

    public void MoneySound() => AudioManager.Instance.PlaySound(money);

    public void VictorySound() => AudioManager.Instance.PlaySound(victory);
}

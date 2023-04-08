using UnityEngine;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public enum AbilityState
{
    Active,
    Deactive,
    Ready
}

public class AbilityManager : MonoBehaviour
{
    public static Ability currentAbility;
    public GameObject abilityUI;
    public Animator powerUpAnim;

    public AudioClip powerUpStartSound;
    public AudioClip powerUpContinueSound;


    bool enteredReady = true;


    public static AbilityManager Instance { get; private set; }

    public AbilityState abilityState;

    // time that's passed
    public float elapsed;

    private bool canActivate = false;
    public bool tookDamage = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (currentAbility.activateOnStart)
        {
            ActivateAbility();
        }
        abilityState = AbilityState.Ready;
        UiManager.Instance.UpdateSliderUi(currentAbility.coolDownTime);

    }

    private void Update()
    {
        switch (abilityState)
        {
            case AbilityState.Ready:
                HandleReadyState();
                break;
            case AbilityState.Active:
                HandleActiveState();
                break;


            case AbilityState.Deactive:
                HandleDeactiveState();
                break;
        }
    }

    private void HandleReadyState()
    {
        if (currentAbility == null) { return; }

        if (enteredReady)
        {
            abilityUI.SetActive(true);
            ResetAbilityValuesAnyTime();
            UiManager.Instance.UpdateSliderUi(currentAbility.coolDownTime);
            UiManager.Instance.ResetUiColor(abilityUI);
            enteredReady = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            ActivateAbility();
            abilityState = AbilityState.Active;

        }
    }

    private void HandleActiveState()
    {
        elapsed += Time.deltaTime;

        UiManager.Instance.DecreaseAbilitySlider();

        Debug.Log("in active state");


        if (elapsed > currentAbility.coolDownTime)
        {
            Debug.Log("in countdown");
            // called when ability ends
            ResetAbilityValuesAnyTime();
            UiManager.Instance.GreyOutUI(abilityUI);

            elapsed = 0;
            abilityState = AbilityState.Deactive;
        }
    }

    private void HandleDeactiveState()
    {
        elapsed += Time.deltaTime;


        if (elapsed > currentAbility.DeactiveTime)
        {
            // called when deactive ends
            ResetAbilityValuesAnyTime();
            UiManager.Instance.UpdateSliderUi(currentAbility.coolDownTime);
            UiManager.Instance.ResetUiColor(abilityUI);

            elapsed = 0;
            abilityState = AbilityState.Ready;
            ResetOnlyOnAbilityEnd();
        }
    }

    public void ResetAbilityValuesAnyTime()
    {
        currentAbility.ResetAbilityValuesAnyTime();
        powerUpAnim.SetBool("Activate", false);
        AudioManager.Instance.FadeOut(1, true);
    }

    void ResetOnlyOnAbilityEnd()
    {
        currentAbility.CallOnlyWhenAbilityEnds();
    }

    public void ResetState()
    {
        abilityState = AbilityState.Ready;
        enteredReady = true;
    }

    public void EquipAbility(Ability abilityToEquip)
    {
        currentAbility = abilityToEquip;
        Debug.Log("equipping " + abilityToEquip);
        UiManager.Instance.UpdateSliderUi(currentAbility.coolDownTime);
    }

    void ActivateAbility()
    {
        Debug.Log("Ability Used");

        currentAbility.Activate();
        powerUpAnim.SetBool("Activate", true);
        AudioManager.Instance.PlaySound(powerUpStartSound);
        AudioManager.Instance.PlayMusic(powerUpContinueSound, true, true, .5f);

        elapsed = 0;
        UiManager.Instance.UpdateSliderUi(currentAbility.coolDownTime);
      
    }

    public void SetToActiveState()
    {
        abilityState = AbilityState.Active;

    }


}

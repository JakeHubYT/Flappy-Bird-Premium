using UnityEngine;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using System;

public enum AbilityState
{
    Active,
    Deactive,
    Ready
}

public class AbilityManager : MonoBehaviour
{
    public static Ability currentAbility;
    public Animator powerUpAnim;

    public AudioClip powerUpStartSound;
    public AudioClip powerUpContinueSound;

    
    public GameObject abilityIcon;
    public GameObject abilityIconHolder;


    bool enteredState = true;

    public static AbilityManager Instance { get; private set; }

    public AbilityState abilityState;

    // time that's passed
    public float elapsed;

    private bool canActivate = false;
    public bool tookDamage = false;



    private void OnEnable()
    {
        Actions.OnClickStartScreen += ClickStartScreen;
    }
    private void OnDisable()
    {
        Actions.OnClickStartScreen -= ClickStartScreen;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
       
    }

    private void Start()
    {
      

        abilityState = AbilityState.Ready;

        if (currentAbility == null) { return; }
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


        if(currentAbility != null)
        {
            abilityIconHolder.SetActive(true);
            abilityIcon.GetComponent<Image>().sprite = currentAbility.sprite; 
        }
        else
            abilityIconHolder.SetActive(false);
    }

    private void HandleReadyState()
    {
       ///Ability Is ready To Be Used
        if (currentAbility == null) { return; }

        if (enteredState) 
        {
            ResetAbility();
            enteredState = false;
        }


        if (Input.GetKeyDown(KeyCode.Mouse1) || currentAbility.activateOnStart && !UiManager.Instance.shopScreen.activeSelf && !UiManager.Instance.deathScreen.activeSelf && !UiManager.Instance.startGameScreen.activeSelf)
        {
            abilityState = AbilityState.Active; 
            enteredState = true;
        }
    }

  

    private void HandleActiveState()
    {

        if (enteredState)
        {
            ActivateAbility();
            enteredState = false;
        }

        elapsed += Time.deltaTime;
        UiManager.Instance.DecreaseAbilitySlider();


        if (elapsed > currentAbility.coolDownTime)
        {
  
            elapsed = 0;
            abilityState = AbilityState.Deactive;
            enteredState = true;
        }
    }

    private void HandleDeactiveState()
    {
        if (enteredState)
        {
            DeactivateAbility();
            AudioManager.Instance.FadeOut(1, true);
            enteredState = false;
        }

        elapsed += Time.deltaTime;


        if (elapsed > currentAbility.DeactiveTime)
        {
            // called when deactive ends
           
            elapsed = 0;
            abilityState = AbilityState.Ready;
            enteredState = true;
            ResetOnlyOnAbilityEnd();
        }
    }

  

    public void ResetAbilityValuesAnyTime()
    {
        currentAbility.ResetAbilityValuesAnyTime();
        powerUpAnim.SetBool("Activate", false);
        AudioManager.Instance.FadeOut(1, true);
    }


    void ClickStartScreen()
    {
        if(currentAbility == null) { return; }
        if (!currentAbility.activateOnStart) { return; }
  
        ActivateAbility();
        abilityState = AbilityState.Active;
    }
    void ResetOnlyOnAbilityEnd()
    {
        currentAbility.CallOnlyWhenAbilityEnds();
    }

    public void ResetState()
    {
        abilityState = AbilityState.Ready;
        enteredState = true;
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
        AudioManager.Instance.PlayMusic(powerUpContinueSound, true, true);

        elapsed = 0;
        UiManager.Instance.UpdateSliderUi(currentAbility.coolDownTime);
      
    }

    public void SetToActiveState()
    {
        abilityState = AbilityState.Active;

    }

    private void ResetAbility()
    {
        UiManager.Instance.ResetAbilityUi();
        ResetAbilityValuesAnyTime();
        UiManager.Instance.UpdateSliderUi(currentAbility.coolDownTime);
        UiManager.Instance.ResetUiColor(UiManager.Instance.abilityUI);
    }

    public void DeactivateAbility()
    {
        ResetAbilityValuesAnyTime();
        AudioManager.Instance.FadeOut(1, true);
        UiManager.Instance.GreyOutUI(UiManager.Instance.abilityUI);
        abilityState = AbilityState.Deactive;
    }
}

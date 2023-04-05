using UnityEngine;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
    public Slider abilitySlider;
    public Animator powerUpAnim;

    public AudioClip powerUpStartSound;
    public AudioClip powerUpContinueSound;

    bool enteredReady = true;


    public static AbilityManager Instance { get; private set; }

    private AbilityState abilityState;

    // time that's passed
    private float elapsed;

    private bool canActivate = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        abilityState = AbilityState.Ready;
        UpdateSliderUi();


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
        
        if(enteredReady)
        {
            abilityUI.SetActive(true);
            DeactivateAbility();
            UpdateSliderUi();
            ResetUiColor();
            enteredReady = false;
        }
     

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (currentAbility == null)
            {
                Debug.Log("No Ability Equipped");
            }
            else
            {
                Debug.Log("Ability Used");


                currentAbility.Activate();
                powerUpAnim.SetBool("Activate", true);
                AudioManager.Instance.PlaySound(powerUpStartSound);
                AudioManager.Instance.PlayMusic(powerUpContinueSound, true);




                elapsed = 0;
                abilitySlider.value = abilitySlider.maxValue;
                abilityState = AbilityState.Active;
            }
        }
    }

    private void HandleActiveState()
    {
        elapsed += Time.deltaTime;
        abilitySlider.value -= Time.deltaTime;

        if (!canActivate)
        {
            if (elapsed > currentAbility.coolDownTime)
            {
                DeactivateAbility();
                GreyOutUI();
                elapsed = 0;
                canActivate = true;
            }
        }
        else if (canActivate)
        {
            if (elapsed > currentAbility.DeactiveTime)
            {
                DeactivateAbility();
                UpdateSliderUi();
                ResetUiColor();
                elapsed = 0;
                canActivate = false;
                abilityState = AbilityState.Ready;
            }
        }
    }

    private void HandleDeactiveState()
    {
        Debug.Log("Ability Deactivated");
        currentAbility.Deactivate();
        abilityState = AbilityState.Ready;
       
    }

    public void EquipAbility(Ability abilityToEquip)
    {
        currentAbility = abilityToEquip;
        Debug.Log("equipping " + abilityToEquip);
        UpdateSliderUi();
    }

    private void GreyOutUI()
    {
        var abilIcon = abilityUI.GetComponent<Image>();
        abilIcon.color = new Color(abilIcon.color.r, abilIcon.color.g, abilIcon.color.b, .25f);
    }

    private void ResetUiColor()
    {
        var abilIcon = abilityUI.GetComponent<Image>();
        abilIcon.color = new Color(abilIcon.color.r, abilIcon.color.g, abilIcon.color.b, 1);
    }

    private void DeactivateAbility()
    {
        currentAbility.Deactivate();
        powerUpAnim.SetBool("Activate", false);
        AudioManager.Instance.FadeOut(1, true);

    }

    public void ResetState()
    {
        abilityState = AbilityState.Ready;
        enteredReady = true;
    }

    private void UpdateSliderUi()
    {
        if(abilitySlider == null) { return; }
        if (currentAbility == null) { return; }


        abilitySlider.maxValue = currentAbility.coolDownTime;
        abilitySlider.value = abilitySlider.maxValue;
    }
}

using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AbilityManager2 : MonoBehaviour
{

    public static Ability currentAbility;
    public GameObject abilityUI;
    public Slider abilitySlider;

    public static AbilityManager2 Instance { get; private set; }

    public bool usedAbility = false;

    //time thats passed
    public float elapsed;

    bool canActivate = false;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        UpdateSliderUi();

    }


    private void Update()
    {
        // abilityUI.SetActive(!usedAbility);



        if (usedAbility)
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
                    usedAbility = false;
                }


            }

        }



        if (Input.GetKeyDown(KeyCode.Mouse1) && usedAbility == false)
        {
            if (currentAbility == null)
            {
                Debug.Log("No Ability Equipped");

            }
            else
            {
                Debug.Log("Ability Used");
                currentAbility.Activate();
                usedAbility = true;


            }


        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && usedAbility == true)
        {
            Debug.Log("Ability Deactivated");

            currentAbility.Deactivate();
            usedAbility = false;


        }
    }

    public void EquipAbility(Ability abilityToEquip)
    {
        currentAbility = abilityToEquip;
        Debug.Log("equipping " + abilityToEquip);

        UpdateSliderUi();

    }

    void UpdateSliderUi()
    {
        abilitySlider.maxValue = currentAbility.coolDownTime;
        abilitySlider.value = abilitySlider.maxValue;
    }

    void GreyOutUI()
    {

        var abilIcon = abilityUI.GetComponent<Image>();
        abilIcon.color = new Color(abilIcon.color.r, abilIcon.color.g, abilIcon.color.b, .25f);
    }
    void ResetUiColor()
    {

        var abilIcon = abilityUI.GetComponent<Image>();
        abilIcon.color = new Color(abilIcon.color.r, abilIcon.color.g, abilIcon.color.b, 1);
    }

    void DeactivateAbility()
    {
        currentAbility.Deactivate();


    }
}

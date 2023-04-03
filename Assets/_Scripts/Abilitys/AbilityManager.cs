using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{

    public static Ability currentAbility;
    public GameObject abilityUI;
    public Slider abilitySlider;

    public static AbilityManager Instance { get; private set; }

    public bool usedAbility = false;

    //time thats passed
    public float elapsed;

    


   
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


   
        if(usedAbility) 
        {
            elapsed += Time.deltaTime;
            abilitySlider.value -= Time.deltaTime;

            if (elapsed > currentAbility.coolDownTime) 
            {
                DeactivateAbility();
                UpdateSliderUi();
                elapsed = 0;
                usedAbility = false;

             
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
        currentAbility= abilityToEquip;
        Debug.Log("equipping " + abilityToEquip);

        UpdateSliderUi();
       
    }

    void UpdateSliderUi()
    {
        abilitySlider.maxValue = currentAbility.coolDownTime;
        abilitySlider.value = abilitySlider.maxValue;
    }


    void DeactivateAbility()
    {
        currentAbility.Deactivate();
       
       
    }
}

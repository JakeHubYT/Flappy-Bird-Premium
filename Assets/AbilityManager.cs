using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public Ability ability;

    public bool abilityActivated = false;


    public static AbilityManager Instance { get; private set; }


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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && abilityActivated == false)
        {   
                ability.Activate();
                abilityActivated = true;

        }

       else if (Input.GetKeyDown(KeyCode.Mouse1) && abilityActivated == true)
        {
            ability.Deactivate();
            abilityActivated = false;

        }

    }
}

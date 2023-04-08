using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandScript : MonoBehaviour
{

    bool invincibleToggled = false;

    public static CommandScript Instance { get; private set; }


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

    public void KillPlayer()
    {
        Actions.OnDamagePlayer();
    }

    public void ToggleInvincibility()
    {
        if(!invincibleToggled)
        {
            Actions.OnInvulnerable();
            invincibleToggled = true;
        }
        else if (invincibleToggled) 
        {
            Actions.OnVulnerable();
            invincibleToggled = false;
        }

    }
}

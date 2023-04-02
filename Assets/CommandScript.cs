using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandScript : MonoBehaviour
{

    public bool takeDamage = true;
    

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

}

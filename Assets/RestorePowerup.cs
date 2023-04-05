using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorePowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AbilityManager.Instance.ResetState();
            Destroy(gameObject);
        }
    }
}

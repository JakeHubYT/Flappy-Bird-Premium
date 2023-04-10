using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDamagePlayerScript : MonoBehaviour
{
    public bool groundHurtsPlayer = true;

    private void OnEnable()
    {
        Actions.OnDirtSurf += CanDirtSurf;
    }
    private void OnDisable()
    {
        Actions.OnDirtSurf -= CanDirtSurf;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && groundHurtsPlayer)
        {
            Actions.OnDamagePlayer();
        }
    }

    void CanDirtSurf()
    {
        groundHurtsPlayer = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool hasDoubleLife = false;
  

    private void OnEnable()
    {
        Actions.OnDamagePlayer += CheckPlayerHealth;
        Actions.OnPlayerDeath += KillPlayer;   

        Actions.OnDoubleLife += EnableDoubleLife;
    }
    private void OnDisable()
    {
        Actions.OnDamagePlayer -= CheckPlayerHealth;
        Actions.OnPlayerDeath -= KillPlayer;

        Actions.OnDoubleLife -= EnableDoubleLife;
    }

    void CheckPlayerHealth()
    {
        Debug.Log("called");
        //if player has ability launch on start itle just be the light effects 

        if (hasDoubleLife) 
        {
            Actions.OnInvulnerable();
            AbilityManager.Instance.DeactivateAbility();
            UiManager.Instance.abilitySlider.value = 0;
            StartCoroutine(MyCoroutine(2));
            return;
        }

        Actions.OnPlayerDeath();
        
    }

    void KillPlayer()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySound(AudioManager.Instance.dieSound);
    }


    IEnumerator MyCoroutine(float timeTillVulnerable)
    {
        Debug.Log("Starting coroutine...");

        yield return new WaitForSeconds(timeTillVulnerable);

        hasDoubleLife = false;
        Actions.OnVulnerable();
      
      
        yield break;
    }

    void EnableDoubleLife()
    {
        hasDoubleLife = true;
    }

}

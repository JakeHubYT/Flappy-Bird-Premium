using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameObject pipePart;
    public GameObject particles;

    public AudioClip hitMetal;


 
   
private void Start()
    {
        if(pipePart == null ) { return; }
        pipePart.SetActive(true);
        particles.SetActive(false);

    }
    private void OnTriggerEnter(Collider collision)
    {
        


        if (collision.gameObject.tag == "Player" && PipeSpawner.Instance.canDamage)
        {
          
                Actions.OnPlayerDeath();

        }
        else if (collision.gameObject.tag == "Player" && !PipeSpawner.Instance.canDamage && gameObject.tag == "Pipe")
        {
            pipePart.SetActive(false);
            particles.SetActive(true);
            AudioManager.Instance.PlaySound(hitMetal, 1, true);
        }

    }

    private void OnTriggerStay(Collider other)  
    {

       

        if (other.gameObject.tag == "Player" && PipeSpawner.Instance.canDamage)
        {

            Actions.OnPlayerDeath();

        }
        else if (other.gameObject.tag == "Player" && !PipeSpawner.Instance.canDamage && gameObject.tag == "Pipe")
        {
            pipePart.SetActive(false);
            particles.SetActive(true);

         

        }
    }

    private void OnTriggerExit(Collider other)
    {
        


        if (other.gameObject.tag == "Player" && !PipeSpawner.Instance.canDamage && gameObject.tag == "Pipe")
        {
            pipePart.SetActive(false);
            particles.SetActive(true);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.gameObject.tag == "Player" && PipeSpawner.Instance.canDamage)
        {

            Actions.OnPlayerDeath();

        }
    }

   
}


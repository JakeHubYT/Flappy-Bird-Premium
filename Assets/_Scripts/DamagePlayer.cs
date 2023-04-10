using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameObject pipePart;
    public GameObject particles;


    private bool canDamage = true;
  

    private void Start()
    {
        if (pipePart == null)
        {
            return;
        }

        pipePart.SetActive(true);
        particles.SetActive(false);

    }

   

    private void Update()
    {
        canDamage = PipeManager.Instance.GetCanDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            TriggerPlayerDeath();
        }
        else if (other.CompareTag("Player") && !canDamage && gameObject.CompareTag("Pipe"))
        {
            pipePart.SetActive(false);
            particles.SetActive(true);
            AudioManager.Instance.PlaySound(AudioManager.Instance.hitMetal, 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            TriggerPlayerDeath();
        }
        else if (other.CompareTag("Player") && !canDamage && gameObject.CompareTag("Pipe"))
        {
            pipePart.SetActive(false);
            particles.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !canDamage && gameObject.CompareTag("Pipe"))
        {
            pipePart.SetActive(false);
            particles.SetActive(true);
        }
    }

   

    void TriggerPlayerDeath()
    {
        Actions.OnDamagePlayer();
    }

 
}



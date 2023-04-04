using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public AudioClip flapSound;
    public AudioClip dieSound;
    public Transform skinHolder;

    public float jumpForce = 5f;
    public Animator anim;
    bool flap = false;

    private void OnEnable()
    {
        Actions.OnPlayerDeath += KillPlayer;
    }
    private void OnDisable()
    {
        Actions.OnPlayerDeath -= KillPlayer;

    }

    private void Start()
    {
        for (int i = 0; i < skinHolder.childCount; i++)
        {
            if (skinHolder.GetChild(i).gameObject.activeSelf)
            {
                anim = skinHolder.GetChild(i).GetComponent<Animator>();
            }
        }
           
        
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) && flap == false)
        {
            anim.SetTrigger("Flap");

            AudioManager.Instance.PlaySound(flapSound);
            flap = true;
        }
    
    }
    private void FixedUpdate()
    {
        if(flap)
        {
            rb.velocity = Vector3.zero;
            rb.velocity = Vector2.up * jumpForce;
            flap = false;
        }


    }

    public void KillPlayer()
    {

        gameObject.SetActive(false);
        AudioManager.Instance.PlaySound(dieSound);

    }

}

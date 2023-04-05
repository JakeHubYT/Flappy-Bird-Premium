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
    public ParticleSystem dirt;


    bool flap = false;
    bool canFastFall = false;

    private void OnEnable()
    {
        Actions.OnPlayerDeath += KillPlayer;
        Actions.OnFastFall += FastFall;
    }
    private void OnDisable()
    {
        Actions.OnPlayerDeath -= KillPlayer;
        Actions.OnFastFall -= FastFall;


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
           
        if(dirt.isPlaying)
            dirt.Stop();


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) && flap == false)
        {
            anim.SetTrigger("Flap");

            AudioManager.Instance.PlaySound(flapSound);
            flap = true;
            canFastFall = false;
        }

        if (canFastFall)
        {
         
            rb.velocity = Vector3.up * -jumpForce;

        }

        anim.SetBool("FastFall", canFastFall);

     


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

    public void FastFall()
    {
        canFastFall = true;
    }


    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            anim.SetBool("onGround", true);
            dirt.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("onGround", false);
            dirt.Stop();

        }
    }
}

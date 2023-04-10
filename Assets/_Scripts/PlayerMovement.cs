using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public AudioClip flapSound;

    public Transform skinHolder;

    public float jumpForce = 5f;
    public Animator PlayerHolderAnim;
    public Animator anim;
    public ParticleSystem dirt;


    bool flap = false;
    bool canFastFall = false;
    bool canToggleGravity = false;

    private void OnEnable()
    {
        Actions.OnFastFall += FastFall;
        Actions.OnFlipGravity += CanToggleGravity;
    }
    private void OnDisable()
    {
        Actions.OnFastFall -= FastFall;
        Actions.OnFlipGravity -= CanToggleGravity;

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


        if(Physics.gravity.y > 0)
        {
            Physics.gravity = -Physics.gravity;
        }

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



        if (Input.GetKeyDown(KeyCode.Mouse1) && canToggleGravity)
        ToggleGravity();




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

    void ToggleGravity()
    {
        Physics.gravity = -Physics.gravity;
        jumpForce = -jumpForce;
        PlayerHolderAnim.SetTrigger("Rotate");
    }

    void CanToggleGravity()
    {
        canToggleGravity = true;
    }
  
}

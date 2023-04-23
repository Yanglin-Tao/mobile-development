using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public float cooldownTime = 1f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;
    public Transform player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (anim){
            print("THERE IS ANIM at start");
        }
    
    }

    public void clicked() {
        lastClickedTime = Time.time;
        noOfClicks++;
    }

    public void Update()
    {
        print(noOfClicks);
        ComboSystem();
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee1"))
        {
            anim.SetBool("hit1", false);
            noOfClicks = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
        {
            anim.SetBool("hit2", false);
            noOfClicks = 0;
            
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        //cooldown time
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + cooldownTime;
        }
    }

    public void ComboSystem()
    {
    
        lastClickedTime = Time.time;

        if (noOfClicks == 1)
        {
            anim.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2  && anim.GetCurrentAnimatorStateInfo(0).IsName("melee1"))
        {
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);
            rb.AddForce(new Vector2(0, 1200));
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
        }
    }
}

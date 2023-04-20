using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public float cooldownTime = 1f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;

    //difficultyLevel is out of 10. 1 is easiest and 10 is hardest
    public int difficultyLevel = 5;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void StartAttack()
    {
        // Rearranges the order of the logic to check for mouse input first,
        // then handle the animations and cooldown time.
        // This should allow the player to trigger melee attacks while walking
        // without the animations being overridden.
        // Check for mouse input
        if (Random.Range(1, 11) < difficultyLevel)
        {
            ComboSystem();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee1"))
        {
            anim.SetBool("hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
        {
            anim.SetBool("hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee3"))
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

    void ComboSystem()
    {
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        lastClickedTime = Time.time;
        noOfClicks++;
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

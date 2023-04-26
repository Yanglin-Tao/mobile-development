using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graffitiCombo : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public float cooldownTime = 1f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;
    public Transform player;
    public GameObject[] Spray;
    private Player gArtist;
    private float bulletSpeed;
    public Transform shootPosition;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gArtist = GetComponent<Player>();
    }

    public void clicked() {
        lastClickedTime = Time.time;
        noOfClicks++;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            clicked();
        }
        if (noOfClicks > 0){
            ComboSystem();
        }
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

        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
            GameObject newBullet = Instantiate(Spray[0], shootPosition.position, Quaternion.identity, transform);
            bulletSpeed = gArtist.updateBulletdirection();
            if (bulletSpeed < 0){
                Vector3 localScale = newBullet.transform.localScale;
                newBullet.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);

            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ult : MonoBehaviour
{
    private Animator Animator;
    private Rigidbody2D rb;
    private Spawner Cena;
    public float cooldownTime = 1f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    public Transform player;
    bool inUlt;
    float lastTime;
    private bool clickEnabled = false;
    public bool stayStill = false;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Cena = GameObject.FindObjectOfType<Spawner>();
        lastClickedTime = -10f;
        lastTime = -10f;

    }

    public void clicked() {
        if (Time.time - lastClickedTime > 10f){
            lastClickedTime = Time.time;
            noOfClicks++;
            inUlt = true;
        }
    }

    public bool getUltStatus(){
        if (stayStill){
            return false;
        }
        return inUlt;
    }

    public void Update()
    {

         if (((Input.GetButtonDown("Fire2") || (noOfClicks > 0)) && (Time.time - lastTime >= 10f))){
            // GameObject newBullet = Instantiate(Attacks[current], shootPosition.position, Quaternion.identity);
            // updateBulletdirection();
            // newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0));
            // current = (current + 1) % Attacks.Length;

            Cena.CenaUlt();
            Animator.SetBool("ULT", true);
            lastTime = Time.time;
            inUlt = true;
            clickEnabled = true;
        }
        
        if (clickEnabled && (Time.time - lastTime >= 5f))
        {
            // Disable clicking the button for the cooldown duration
            clickEnabled = false;
            Animator.SetBool("ULT", false);
            inUlt = false;
            noOfClicks  = 0;
            lastTime = Time.time;
            // Do something here when the button is clicked
            Debug.Log("Button clicked");
        }


        if (getUltStatus()){
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), localScale.y, localScale.z);
        }
    }


    private void EnableClick()
    {

        clickEnabled = true;
    }


}

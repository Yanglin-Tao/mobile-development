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
    public bool useThisUlt = false;
    AudioSource _audioSource;
    public AudioClip ultSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
            clickEnabled = true;
        }
    }

    public bool getUltStatus(){
        if (stayStill){
            return false;
        }
        return inUlt;
    }

    public bool FallingUlt(){
        return useThisUlt;
    }


    public void Update()
    {

        if ((Input.GetButtonDown("Fire2"))){
            clicked();
            // if cooled down, then play ult sound
            // _audioSource.PlayOneShot(ultSound);
            if (clickEnabled){
                _audioSource.PlayOneShot(ultSound);
            } 
        }

        if ((noOfClicks > 0) && (Time.time - lastTime >= 10f)){
            if (Cena != null && useThisUlt){
                Cena.CenaUlt();
            }
            Animator.SetBool("ULT", true);    
            _audioSource.PlayOneShot(ultSound);
            lastTime = Time.time;
            inUlt = true;
            clickEnabled = true;
            stayStill = true;
        }

        if (clickEnabled && (Time.time - lastTime >= 5f))
        {
            // Disable clicking the button for the cooldown duration
            clickEnabled = false;
            Animator.SetBool("ULT", false);
            inUlt = false;
            noOfClicks  = 0;
            lastTime = Time.time;
            stayStill = false;
        }


        if (stayStill){
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), localScale.y, localScale.z);
        }

        // bool ultState = Animator.GetBool("ULT");
        // Debug.Log("ULT state: " + ultState);

    }


    private void EnableClick()
    {
        clickEnabled = true;
    }


}

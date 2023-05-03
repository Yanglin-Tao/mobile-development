using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ult : MonoBehaviour
{
    private Animator Animator;
    private Rigidbody2D rb;
    private Spawner Cena;
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

    // Cool down timer
    public float cooldownTime = 15f;
    public TMPro.TextMeshProUGUI cooldownText;
    private bool isCooldown = false;
    private float timer = 0f;

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
        if (!isCooldown && Time.time - lastClickedTime > 10f){
            lastClickedTime = Time.time;
            noOfClicks++;
            inUlt = true;
            clickEnabled = true;
            isCooldown = true;
            timer = cooldownTime;
            // cooldownText.gameObject.SetActive(true);
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
        if (isCooldown)
        {
            timer -= Time.deltaTime;
            if (timer <= 10 && timer > 0){
                cooldownText.gameObject.SetActive(true);
                cooldownText.text = "Cooldown: " + Mathf.Round(timer).ToString() + "s";
            }
            if (timer <= 0){
                isCooldown = false;
                cooldownText.gameObject.SetActive(false);
            }
        }

    }

    private void EnableClick()
    {
        clickEnabled = true;
    }


}

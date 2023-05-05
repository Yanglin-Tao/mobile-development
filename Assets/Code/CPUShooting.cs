using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUShooting : MonoBehaviour
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
    public string CPUname = "";

    private Transform user;
    private int current = 0;
    public GameObject[] Attacks;
    public Transform shootPosition;
    

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Cena = GameObject.FindObjectOfType<Spawner>();
        lastClickedTime = -10f;
        lastTime = -10f;
        user = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("clicked", 2f, 2f);

    }

    public void clicked() {
        lastClickedTime = Time.time;
        noOfClicks++;
        inUlt = true;
        clickEnabled = true;
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

        if (((noOfClicks > 0) && (Time.time - lastTime >= 10f)) && (useThisUlt)){

            Cena.CenaUlt();
            Animator.SetBool("ULT", true);
            lastTime = Time.time;
            inUlt = true;
            clickEnabled = true;
            stayStill = true;
        }

        if (clickEnabled)
        {
            // Disable clicking the button for the cooldown duration
            if (CPUname == "Shooting"){
                Vector3 myPos = transform.position; 
                Vector3 otherObjectPos = user.position;
                // Check if the other object is 5 pixels away in X direction
                float distanceX = Mathf.Abs(myPos.x - otherObjectPos.x);
                if (distanceX > 1f)
                {
                    Animator.SetBool("ULT", true);
                    StartCoroutine(Shoot());
                }
            clickEnabled = false;
            Animator.SetBool("ULT", false);
            inUlt = false;
            noOfClicks  = 0;
            lastTime = Time.time;
            stayStill = false;
            }
        }


        if (stayStill){
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), localScale.y, localScale.z);
        }
    }



    IEnumerator Shoot() {
        while(true){
            yield return new WaitForSeconds(1f);
            // Get the positions of the two objects
            Vector3 myPos = transform.position; 
            Vector3 otherObjectPos = user.position;

            // Check if the other object is 5 pixels away in X direction
            float distanceX = Mathf.Abs(myPos.x - otherObjectPos.x);
            if (distanceX > 1f)
            {
                GameObject newBullet;
                float xDirection = transform.localScale.x;
                newBullet = Instantiate(Attacks[current], shootPosition.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDirection*700, 0)); 
                current = (current + 1) % Attacks.Length;
            }
        }
    }

}  

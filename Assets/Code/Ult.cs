using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private GameObject mainPlayer;
    private Scene scene;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Cena = GameObject.FindObjectOfType<Spawner>();
        lastClickedTime = -10f;
        lastTime = -10f;
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        scene = SceneManager.GetActiveScene();
    }

    public void clicked() {
        Debug.Log("clicked");
        if (!isCooldown && Time.time - lastClickedTime > 10f && Time.time > 0.1){
            lastClickedTime = Time.time;
            noOfClicks++;
            inUlt = true;
            clickEnabled = true;
            isCooldown = true;
            Debug.Log("cooling down");
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

    if (Time.time > 0.1){

        if ((noOfClicks > 0) && (Time.time - lastTime >= 10f)){
            if (Cena != null && useThisUlt){
                Cena.CenaUlt();
            }
            // Debug.Log(scene.name);
            // Debug.Log(mainPlayer != null);
            if (mainPlayer != null && (scene.name == "Level3" || scene.name == "Level4")){
                Player playerScript = mainPlayer.GetComponent<Player>();
                playerScript.Shoot();
            }
            Animator.SetBool("ULT", true);    
            _audioSource.PlayOneShot(ultSound);
            lastTime = Time.time;
            inUlt = true;
            clickEnabled = true;
            if (useThisUlt){
                stayStill = true;
            }
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
            if (scene.name == "Level3" || scene.name == "Level4"){
                if (timer > 0){
                    cooldownText.gameObject.SetActive(true);
                    cooldownText.text = "Cooldown: " + Mathf.Round(timer).ToString() + "s";
                }
            }
            else{
                if (timer <= 10 && timer > 0){
                    cooldownText.gameObject.SetActive(true);
                    cooldownText.text = "Cooldown: " + Mathf.Round(timer).ToString() + "s";
                }
            }
            if (timer <= 0){
                isCooldown = false;
                cooldownText.gameObject.SetActive(false);
            }
        }

        }
    }

    private void EnableClick()
    {
        clickEnabled = true;
    }


}

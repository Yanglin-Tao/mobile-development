using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator Animator;
    private Collider2D _collider2D;
    public GameObject explosion;
    GameManager _gameManager;
    private Spawner spawner;

    // Audio
    AudioSource _audioSource;
    public AudioClip jumpSound;

    public GameObject[] Attacks;
    public int specialCharge = 0;
    public Transform shootPosition;
    public LayerMask ground;
    public Transform feet;
    public Ult ultStatus;

    public float speed;
    public int health;
    public float jumpForce = 900;
    public float bulletSpeed = 900;
    bool isGrounded = false;
    int current;
    float lastTime;
    public float xDirection;

    private bool clickGUARD = false;
    int noOfClicks;

    // Update is called once per frame
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        ultStatus = GetComponent<Ult>();
        noOfClicks = 0;
    }

    public void clickFunction(){
        if (Time.time > 0.1f){
            noOfClicks+=1;
        }
    }

    void Update(){
        //print(Time.time);
        if (Time.time > 0.1f){
            //_gameManager.SetLife(health);
            if (Input.GetButtonDown("Jump") && isGrounded){
                rb.AddForce(new Vector2(0, jumpForce));
                // play jump sound
                _audioSource.PlayOneShot(jumpSound);
            }

            if (noOfClicks >= 1 && (Attacks.Length != 0)){

                Animator.SetBool("ULT", true);
                GameObject newBullet;

                if (specialCharge == 0){
                    newBullet = Instantiate(Attacks[current], shootPosition.position, Quaternion.identity);
                    // newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0));
                    newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDirection*bulletSpeed, 0)); 
                    current = (current + 1) % Attacks.Length;
                }
                else{
                    newBullet = Instantiate(Attacks[current], shootPosition.position, Quaternion.identity);
                    //newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDirection*bulletSpeed, 0));
                    current = (current + 1) % Attacks.Length;
                }
                
                /*
                updateBulletdirection();
                if (bulletSpeed < 0){
                    Vector3 localScale = newBullet.transform.localScale;
                    newBullet.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);

                }*/
                lastTime = Time.time;
                noOfClicks-=1;

            }
            if ((Time.time - lastTime > .3f) && (!ultStatus.FallingUlt()) ){
                Animator.SetBool("ULT", false);
                // Debug.Log("set to false");
            }
            Animator.SetBool("Jump", !isGrounded);
        }

    }

    public void Shoot(){
        Debug.Log("shoot runs");
        if (Attacks.Length != 0){
            Animator.SetBool("ULT", true);
            GameObject newBullet;

            if (specialCharge == 0){
                newBullet = Instantiate(Attacks[current], shootPosition.position, Quaternion.identity);
                // newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0));
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDirection*bulletSpeed, 0)); 
                current = (current + 1) % Attacks.Length;
            }
            else{
                newBullet = Instantiate(Attacks[current], shootPosition.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDirection*bulletSpeed, 0));
            }
            /*
            updateBulletdirection();
            if (bulletSpeed < 0){
                Vector3 localScale = newBullet.transform.localScale;
                newBullet.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);

            }*/
            lastTime = Time.time;
        }

    }

    void FixedUpdate() {
        float horizontalMovement = Input.GetAxis("Horizontal") * speed;
        xDirection = transform.localScale.x;
        if (horizontalMovement < 0 && xDirection > 0 || horizontalMovement > 0 && xDirection < 1){
            transform.localScale *= new Vector2(-1,1);
        }

        isGrounded = Physics2D.OverlapCircle(feet.position, .3f, ground);
        if (!clickGUARD){
            Animator.SetFloat("Speed", 0);
        }
// UNCOMMENT FROM HERE FOR VIRTUAL MOVEMENT LEFT AND RIGHT
        // if (_gameManager.getHealth() > 0){
        //     float xSpeed = Input.GetAxis("Horizontal") * speed;
        //     rb.velocity = new Vector2(xSpeed, rb.velocity.y);

        //     float xScale = transform.localScale.x;
        //     if ((xSpeed < 0 && xScale > 0) || (xSpeed > 0 && xScale < 0) && (!ultStatus.getUltStatus())){
        //         // get current localScale
        //         Vector3 localScale = transform.localScale;
        //         // flip x axis
        //         transform.localScale = new Vector3(-xScale, localScale.y, localScale.z);
        //     }
        //     Animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        // }
// UNCOMMENT ABOVE HERE FOR VIRTUAL MOVEMENT LEFT AND RIGHT
        Animator.SetFloat("Health", _gameManager.getHealth());
        Animator.SetBool("Attack", false);

        if (CheckIsAttacked())
        {
            Animator.SetBool("Damage", true);
        }
        else
        {
            Animator.SetBool("Damage", false);
        }
    }

// Mobile Stuff
    public void Jump(){
        if (isGrounded){
            rb.AddForce(new Vector2(0, jumpForce));
            // play jump sound
            _audioSource.PlayOneShot(jumpSound);
        }
    }

    public void MoveLeft(){
        clickGUARD = true;
        rb.velocity = new Vector2(-speed, rb.velocity.y);

        float xScale = transform.localScale.x;
        if (xScale > 0)
        {
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(-xScale, localScale.y, localScale.z);
        }

        Animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        clickGUARD = false;
    }

    private bool CheckIsAttacked()
    {
        // if time is paused, return false
        if (Time.timeScale == 0)
        {
            return false;
        }
        // if player's health changed in the last frame
        if (health > (int)_gameManager.getHealth())
        {
            // player is attacked
            health = (int)_gameManager.getHealth();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveRight()
    {
        clickGUARD = true;
        rb.velocity = new Vector2(speed, rb.velocity.y);

        float xScale = transform.localScale.x;
        if (xScale < 0)
        {
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(-xScale, localScale.y, localScale.z);
        }

        Animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        clickGUARD = false;
    }

    public float updateBulletdirection(){
        if (transform.localScale.x < 0){
            bulletSpeed = -Mathf.Abs(bulletSpeed);
        }
        else{
            bulletSpeed = Mathf.Abs(bulletSpeed);
        }
        return bulletSpeed;
    }




}

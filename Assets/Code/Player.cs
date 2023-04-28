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

    private bool clickGUARD = false;

    // Update is called once per frame
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        spawner = GetComponent<Spawner>();
        ultStatus = GetComponent<Ult>();
    }

    void Update(){
        //_gameManager.SetLife(health);
        if (Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(new Vector2(0, jumpForce));
            // play jump sound
            _audioSource.PlayOneShot(jumpSound);
        }

        if (Input.GetButtonDown("Fire2") && (Attacks.Length != 0)){
            Animator.SetBool("ULT", true);
            GameObject newBullet;

            if (specialCharge == 0){
                newBullet = Instantiate(Attacks[current], shootPosition.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0));
                current = (current + 1) % Attacks.Length;
            }
            else{
                newBullet = Instantiate(Attacks[current], shootPosition.position, Quaternion.identity);

            }

            updateBulletdirection();
            if (bulletSpeed < 0){
                Vector3 localScale = newBullet.transform.localScale;
                newBullet.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);

            }
            lastTime = Time.time;

        }
        if ((Time.time - lastTime > .3f) && (!ultStatus.FallingUlt()) ){
            Animator.SetBool("ULT", false);
        }
        Animator.SetBool("Jump", !isGrounded);

    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(feet.position, .3f, ground);
        if (!clickGUARD){
            Animator.SetFloat("Speed", 0);
        }
// UNCOMMENT FROM HERE FOR VIRTUAL
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
// UNCOMMENT ABOVE HERE FOR VIRTUAL
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

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

    // audio
    public AudioClip jumpSound;
    public AudioClip meleeSound;
    public AudioClip hitSound;
    AudioSource _audioSource;

    public GameObject[] Attacks;
    public int specialCharge;
    public Transform shootPosition;
    public LayerMask ground;
    public Transform feet;

    // melee
    private float lastMeeleAttackTime;
    public float meleeAttackRange = 1f;
    // The desired rate limit for melee attacks in seconds
    // if 1f, then melee attacks will be limited to 1 per second
    // if 2f, then melee attacks will be limited to 1 per 2 seconds
    public float meleeAttackDelay = 1f;
    public float meleeDamage = 1f;
    public Transform meleeAttackSpawnPoint;

    public float speed;
    public int health;
    public float jumpForce = 900;
    public float bulletSpeed = 900;
    bool isGrounded = false;
    float curTime;
    int currAttack;

    // Update is called once per frame
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        _gameManager.SetLife(health);
        if (Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(new Vector2(0, jumpForce));
            // play jump sound
            _audioSource.PlayOneShot(jumpSound);
        }

        if (Input.GetButtonDown("Fire1")){
            // Check if enough time has passed since the last attack
            if (Time.time - lastMeeleAttackTime < meleeAttackDelay)
            {
                return;
            }
            // Update the last attack time to the current time
            lastMeeleAttackTime = Time.time;
            PerformMeleeAttack();

            curTime = Time.time + .5f;
            currAttack = 1;
            if (Input.GetButtonDown("Fire1") && Time.time - curTime < .5f){
                // curTime = Time.time;
                // currAttack = 2;
                // print(true);
                // if (Time.time - curTime < .5f){
                //     currAttack = 3;
                // }
            }
        }


        if (Input.GetButtonDown("Fire2")){
            updateBulletdirection();
            GameObject Bullet = Instantiate(Attacks[specialCharge], shootPosition.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(bulletSpeed, 0, 1));
            specialCharge = (specialCharge + 1) % Attacks.Length;
            Destroy(Bullet, 2);
        }
        //print(currAttack);

        Animator.SetBool("Jump", !isGrounded);
        Animator.SetInteger("melee", currAttack);
        currAttack = 0;

    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(feet.position, .3f, ground);
        if (_gameManager.getHealth() > 0){
            float xSpeed = Input.GetAxis("Horizontal") * speed;
            rb.velocity = new Vector2(xSpeed, rb.velocity.y);

            float xScale = transform.localScale.x;
            if ((xSpeed < 0 && xScale > 0) || (xSpeed > 0 && xScale < 0)){
                // get current localScale
                Vector3 localScale = transform.localScale;
                // flip x axis
                transform.localScale = new Vector3(-xScale, localScale.y, localScale.z);
            }
            Animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        }
        Animator.SetFloat("Health", _gameManager.getHealth());
        Animator.SetBool("Attack", false);

    }


    void PerformMeleeAttack()
    {
        // Get the direction the player is facing
        int facingDirection = transform.localScale.x < 0 ? -1 : 1;

        // Calculate the melee attack range in the direction the player is facing
        Vector2 attackDirection = new Vector2(facingDirection, 0);
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * meleeAttackRange;

        // Calculate the start position of the melee attack relative to the player
        Vector2 spawnPosition = (Vector2)meleeAttackSpawnPoint.position;

        // Detect all colliders within the melee attack range centered at the spawn point
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, meleeAttackRange);

        bool hitEnemy = false;
        foreach (Collider2D collider in colliders)
        {
            // Perform melee attack on detected game objects with "Enemy" tag
            if (collider.gameObject.tag == "Enemy")
            {
                hitEnemy = true;
                Debug.Log("Melee attack hit: " + collider.gameObject.name);
            }
        }

        if (hitEnemy)
        {
            // Play melee attack sound
            _audioSource.PlayOneShot(hitSound);
        }
        else {
            // Play melee attack miss sound
            _audioSource.PlayOneShot(meleeSound);
        }
    }









    void updateBulletdirection(){
        if (transform.localScale.x < 0){
            bulletSpeed = -Mathf.Abs(bulletSpeed);
        }
        else{
            bulletSpeed = Mathf.Abs(bulletSpeed);
        }
    }

    // Draw Gizmos for melee attack range in Scene view
    void OnDrawGizmos()
    {
        // Get the direction the player is facing
        int facingDirection = transform.localScale.x < 0 ? -1 : 1;

        // Calculate the start position of the melee attack relative to the player
        Vector2 attackDirection = new Vector2(facingDirection, 0);
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * meleeAttackRange;

        // Calculate the start position of the melee attack relative to the player
        Vector2 spawnPosition = (Vector2)meleeAttackSpawnPoint.position;

        // Draw a wire sphere gizmo to represent the spawn position of the melee attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPosition, meleeAttackRange); // Update with desired size for spawn position gizmo
    }
}

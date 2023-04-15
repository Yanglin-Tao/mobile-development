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
    AudioSource _audioSource;

    public GameObject[] Attacks;
    public int specialCharge;
    public Transform shootPosition;
    public LayerMask ground;
    public Transform feet;

    // melee
    public float meleeAttackRange = 1f;
    public float meleeDamage = 1f;
    public Transform meleeAttackSpawnPoint;

    public float speed;
    public int health;
    public float jumpForce = 900;
    public float bulletSpeed = 900;
    bool isGrounded = false;
    float curTime;
    int currAttack;


    public Animator myAnimator;
    public bool isAttacking = false;
    public static Player instance;

    private void Awake(){
        instance = this;
    }

    // Update is called once per frame
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }

    void Update(){
        MeleeCombo();
        _gameManager.SetLife(health);
        if (Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(new Vector2(0, jumpForce));
        }

        // if (Input.GetButtonDown("Fire1")){
        //     curTime = Time.time + .5f;
        //     currAttack = 1;
        //     //PerformMeleeAttack();
        //     if (Input.GetButtonDown("Fire1") && Time.time - curTime < .5f){
        //         // curTime = Time.time;
        //         // currAttack = 2;
        //         // print(true);
        //         // if (Time.time - curTime < .5f){
        //         //     currAttack = 3;
        //         // }
        //     }
        // }


        // if (Input.GetButtonDown("Fire2")){
        //     updateBulletdirection();
        //     GameObject Bullet = Instantiate(Attacks[specialCharge], shootPosition.position, Quaternion.identity);
        //     Bullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(bulletSpeed, 0, 1));
        //     specialCharge = (specialCharge + 1) % Attacks.Length;
        //     Destroy(Bullet, 2);
        // }
        // //print(currAttack);

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

        foreach (Collider2D collider in colliders)
        {
            // Perform melee attack on detected game objects with "Enemy" tag
            if (collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Melee attack hit: " + collider.gameObject.name);
            }
        }
    }









    public void MeleeCombo(){
        if (Input.GetButtonDown("Fire1") && !isAttacking){
            print("THIS RAN");
            isAttacking = true;
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

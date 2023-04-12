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

    public float speed;
    public int health;
    public float jumpForce = 900;
    public float bulletSpeed = 900;
    bool isGrounded = false;

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
            Animator.SetBool("Jump", !isGrounded);
        }
        Animator.SetBool("Jump", !isGrounded);


        if (Input.GetButtonDown("Fire2")){
            updateBulletdirection();
            GameObject Bullet = Instantiate(Attacks[specialCharge], shootPosition.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(bulletSpeed, 0, 1));
            specialCharge = (specialCharge + 1) % Attacks.Length;
            Destroy(Bullet, 2);
        }


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





    void updateBulletdirection(){
        if (transform.localScale.x < 0){
            bulletSpeed = -Mathf.Abs(bulletSpeed);
        }
        else{
            bulletSpeed = Mathf.Abs(bulletSpeed);
        }
    }

}

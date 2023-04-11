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
    public int specialStatus;
    public Transform shootPosition;
    public LayerMask ground;
    public Transform feet;

    public float speed;
    public int health;
    public float jumpForce = 300;
    bool isGrounded = false;

    // Update is called once per frame
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        _gameManager.ChangeLife(health);
        
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(feet.position, .2f, ground);
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

        if (Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(new Vector2(0, jumpForce));
            Animator.SetBool("Jump", !isGrounded);
        }
        Animator.SetBool("Jump", !isGrounded);

    }
}

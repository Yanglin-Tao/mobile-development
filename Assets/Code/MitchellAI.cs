using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MitchellAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 4f;
    public float jumpForce = 5f;
    public float jumpHeight = 1f;
    public float moveSpeed = 2f;
    public int speed = 10;
    float xspeed;

    GameManager _gameManager;
    private Rigidbody2D rb;
    public Transform feet;
    public LayerMask ground;
    private Animator Animator;
    AIAttack attackscript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        attackscript = GetComponent<AIAttack>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // if (player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0){
        //     transform.localScale *= new Vector2(-1,1);
        // }
        float distance = Mathf.Abs(transform.position.x - player.position.x);

        if (distance < attackRange)
        {
            attackscript.StartAttack();
            //Debug.Log("Attacking player!");
        }
        else
        {
            // // move towards the player
            // Vector2 direction = player.position - transform.position;
            // rb.velocity = direction.normalized * moveSpeed;
            // float xspeed = direction.x * speed;
            // Animator.SetFloat("Speed", Mathf.Abs(xspeed));

            // // jump to higher platform if player jumps
            // if (player.position.y > 0 && IsGrounded()){
            //     rb.AddForce(new Vector2(0f, jumpForce));
            // }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feet.position, .3f, ground);
    }

    private void OnDrawGizmos()
    {
        // Set the color of the gizmo
        Gizmos.color = Color.red;

        // Draw a wire sphere to show the attack range
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Attack")){
            _gameManager.SetEnemyHealth(_gameManager.getEnemyHealth() - 8);
            Destroy(other.gameObject);
        }
        if(other.CompareTag("ICECREAM")){
            _gameManager.SetEnemyHealth(_gameManager.getEnemyHealth() - 8);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("badThing")){
            _gameManager.SetEnemyHealth(_gameManager.getEnemyHealth() - 8);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // print("THIS RAN");
        if(other.gameObject.CompareTag("Attack")){
            _gameManager.SetEnemyHealth(_gameManager.getEnemyHealth() - 3);
        }

        
    }


    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     Debug.Log("OnCollisionEnter2D");
    // }


}

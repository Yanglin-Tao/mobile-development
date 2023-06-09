using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCombo : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public float cooldownTime = 1f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = .25f;
    public Transform player;

    GameManager _gameManager;

    AudioSource _audioSource;
    public AudioClip meleeSound;
    public AudioClip hitSound;

    // melee
    public float meleeAttackRange = 1f;
    public float meleeDamage = 1f;
    public Transform meleeAttackSpawnPoint;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void clicked() {
        if (noOfClicks < 3){
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks <= 3) {
                PerformMeleeAttack();
            }
        }
    }

    public void Update()
    {
        if (noOfClicks > 0){
            ComboSystem();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .99f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee1"))
        {
            anim.SetBool("hit1", false);
            noOfClicks = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .99f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
        {
            anim.SetBool("hit2", false);
            noOfClicks = 0;

        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .99f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        //cooldown time
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + cooldownTime;
        }
    }

    public void ComboSystem()
    {
        if (noOfClicks == 1)
        {
            anim.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2  && anim.GetCurrentAnimatorStateInfo(0).IsName("melee1"))
        {
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);

        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
        }
    }

    void PerformMeleeAttack()
    {
        // Debug.Log("Perform melee attack");
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
                // Deal damage to the enemy
                int enemyHealth = _gameManager.getEnemyHealth();
                // Debug.Log("Enemy health: " + enemyHealth);
                _gameManager.SetEnemyHealth(enemyHealth - (int)meleeDamage);
                // Debug.Log("Enemy health: " + enemyHealth);
                // Debug.Log("Melee attack hit: " + collider.gameObject.name);
            }
        }
        if (hitEnemy)
        {
            // Play hit sound
            _audioSource.PlayOneShot(hitSound);
        }
        else
        {
            // Play melee sound
            _audioSource.PlayOneShot(meleeSound);
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

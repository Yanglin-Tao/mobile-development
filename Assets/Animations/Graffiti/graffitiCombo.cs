using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graffitiCombo : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public float cooldownTime = 1f;
    // private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;
    public Transform player;
    public GameObject[] Spray;
    private Player gArtist;
    private float bulletSpeed;
    public Transform shootPosition;
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
        gArtist = GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void clicked() {
        lastClickedTime = Time.time;
        noOfClicks++;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            clicked();
            if (noOfClicks <= 3) {
                PerformMeleeAttack();
            }
        }
        if (noOfClicks > 0){
            ComboSystem();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee1"))
        {
            anim.SetBool("hit1", false);
            noOfClicks = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
        {
            anim.SetBool("hit2", false);
            noOfClicks = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        //cooldown time
        // if (Time.time > nextFireTime)
        // {
        //     nextFireTime = Time.time + cooldownTime;
        // }
        //print(noOfClicks);
    }

    public void ComboSystem()
    {
        //lastClickedTime = Time.time;

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
            GameObject newBullet = Instantiate(Spray[0], shootPosition.position, Quaternion.identity, transform);
            bulletSpeed = gArtist.updateBulletdirection();
            if (bulletSpeed < 0){
                Vector3 localScale = newBullet.transform.localScale;
                newBullet.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);

            }

        }
    }

    void PerformMeleeAttack()
    {
        Debug.Log("Perform melee attack");
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

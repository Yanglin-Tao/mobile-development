using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public float cooldownTime = 1f;
    //private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = .6f;
    float maxComboDelay = 1f;

    GameManager _gameManager;

    AudioSource _audioSource;
    public AudioClip meleeSound;
    public AudioClip hitSound;


    // melee
    public float meleeAttackRange = 1f;
    public float meleeDamage = 1f;
    public Transform meleeAttackSpawnPoint;

    //difficultyLevel is out of 3. 3 is easiest and 1 is hardest
    public int difficultyLevel = 1000;
    public string CPUname = "None";

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    public void StartAttack()
    {
        // Rearranges the order of the logic to check for mouse input first,
        // then handle the animations and cooldown time.
        // This should allow the player to trigger melee attacks while walking
        // without the animations being overridden.
        // Check for mouse input

        // if ((Time.time - lastClickedTime) > .6f){
        //     if ((Random.Range(1, 1000) < difficultyLevel)){
        //         print(Time.time);
        //         ComboSystem();
        //         lastClickedTime = Time.time;
        //     }
        // }
        // print("THIS RAN");
        if ((Time.time - lastClickedTime) > 2f){
            print("THIS IS RUNNING ");
            StartCoroutine(LoopWithDelay(Random.Range(1, 4), 0.4f));
            lastClickedTime = Time.time;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee1"))
        {
            anim.SetBool("hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
        {
            anim.SetBool("hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && anim.GetCurrentAnimatorStateInfo(0).IsName("melee3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        // //cooldown time
        // if (Time.time > nextFireTime)
        // {
        //     nextFireTime = Time.time + cooldownTime;
        // }
    }

    void ComboSystem()
    {
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        noOfClicks++;
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
            if (CPUname == "FatBoss"){
                rb.AddForce(new Vector2(0, 1000));
            }
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

        bool hitPlayer = false;
        foreach (Collider2D collider in colliders)
        {
            // Perform melee attack on detected game objects with "Player" tag
            if (collider.gameObject.tag == "Player")
            {
                hitPlayer = true;
                // Deal damage to the player
                float playerHealth = _gameManager.getHealth();
                _gameManager.SetHealth((int)playerHealth - (int)meleeDamage);
            }
        }
        if (hitPlayer)
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

    IEnumerator LoopWithDelay(int attackCombo, float hitDelay) {
        for (int i = 0; i < attackCombo; i++) {
            // Do something
            ComboSystem();
            if (noOfClicks <= 3) {
                PerformMeleeAttack();
            }

            // Wait for 0.6 seconds before continuing to the next iteration
            yield return new WaitForSeconds(hitDelay);
        }
    }
}

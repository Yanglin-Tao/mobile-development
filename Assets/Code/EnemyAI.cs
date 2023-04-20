using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Transform user;
    public float distance = 10000;
    public int movement = 4;

    GameManager _gameManager;
    AudioSource _audioSource;
    private Animator Animator;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();

        user = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Follow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Follow() {
        while(true){
            yield return new WaitForSeconds(0.1f);
            if(Vector2.Distance(transform.position, user.position) < distance && Vector2.Distance(transform.position, user.position) > 1){
                if (user.position.x > transform.position.x && transform.localScale.x < 0 || user.position.x < transform.position.x && transform.localScale.x > 0){
                    transform.localScale *= new Vector2(-1,1);
                }
                Vector2 angle_direction = (user.position - transform.position);
                _rigidbody.velocity = angle_direction.normalized * movement;
            }
        }
    }
}

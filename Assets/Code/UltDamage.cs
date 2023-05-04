using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltDamage : MonoBehaviour
{
    public int ultDamage = 5;
    public float ultRange = 1f;
    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // check what collides with current object
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ultRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                // Deal damage to the enemy
                int enemyHealth = _gameManager.getEnemyHealth();
                _gameManager.SetEnemyHealth(enemyHealth - (int)ultDamage);
                Debug.Log("Enemy health deducted");
                // destroy itself
                Destroy(gameObject);
            }
        }
    }

    // draw Gizmos for ult range in Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ultRange);
    }
}

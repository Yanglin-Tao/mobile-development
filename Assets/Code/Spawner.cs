using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float rate;
    public int speed;
    public GameObject[] enemies;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        int Start_Posistion_X = Random.Range(-9, 17);
        int Start_Posistion_Y = 7;
        GameObject NPCBoss = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Start_Posistion_X-4, Start_Posistion_Y, 0), Quaternion.identity);
    }
}

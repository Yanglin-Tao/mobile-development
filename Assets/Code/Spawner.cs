using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float rate;
    public int speed;
    public GameObject[] enemies;
    private float CurrTime;


    // void Start(){
    //     CurrTime = Time.time;
    // }

    // Update is called once per frame
    public void CenaUlt(float currtime){
        print("THIS RAN");
        while (Time.time - currtime > 5f){
            InvokeRepeating("SpawnEnemy", rate, rate);
        }
    }

    public void SpawnEnemy()
    {
        int Start_Posistion_X = Random.Range(-9, 17);
        int Start_Posistion_Y = 7;
        GameObject NPCBoss = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Start_Posistion_X-4, Start_Posistion_Y, 0), Quaternion.identity);
    }
}

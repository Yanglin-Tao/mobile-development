using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float rate;
    public int speed;
    public GameObject[] enemies;


    // void Start(){
    //     CurrTime = Time.time;
    // }

    // Update is called once per frame
    public void CenaUlt(){
        InvokeRepeating("SpawnEnemy", rate, rate);
        Invoke("StopInvoke", 10);
    }

    public void SpawnEnemy()
    {
        int Start_Posistion_X = Random.Range(-9, 17);
        int Start_Posistion_Y = 7;
        GameObject NPCBoss = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Start_Posistion_X-4, Start_Posistion_Y, 0), Quaternion.identity);
    }
    private void StopInvoke()
    {
        CancelInvoke("SpawnEnemy");
    }
}

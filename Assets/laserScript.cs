using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    GameObject laser;
    public AudioClip ShootSound;
    AudioSource _audioSource;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        laser = GameObject.FindWithTag("Attack");
        if (laser){
            _audioSource.PlayOneShot(ShootSound);
        }
    }
}

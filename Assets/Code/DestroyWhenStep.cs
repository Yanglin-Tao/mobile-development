using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenStep : MonoBehaviour
{
    public AudioClip destroySound;
    public float destroyDelay = 1.0f;
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _audioSource.PlayOneShot(destroySound);
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bloco : MonoBehaviour
{
    private AudioSource BlockDestroy;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlockDestroy = GetComponent<AudioSource>();

        BlockDestroy.Play();

        Destroy(gameObject, 0.1f);
    }
}
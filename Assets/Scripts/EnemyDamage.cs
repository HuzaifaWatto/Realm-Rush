using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints <= 0)
        {
            kill();
        }
    }
    private void ProcessHit()
    {
        hitPoints = hitPoints - 1;
        hitParticlePrefab.Play();
        audioSource.PlayOneShot(enemyHitSFX); 
        //print("current hitpoints r " + hitPoints);
    
    }
    private void kill()
    {
        //important to instentiate before destroying this object
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();

        //destroy particle after delay
        Destroy(vfx.gameObject, vfx.main.duration);
       // AudioSource.PlayClipAtPoint(enemyDeathSFX, transform.position);
        //destroy enemy
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 10;
    [SerializeField] int healthDecrese = 1;
    [SerializeField] Text HealthText;
    [SerializeField] AudioClip playerDamageSFX;


    private void Start()
    {
        HealthText.text = playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(playerDamageSFX);
        playerHealth -= healthDecrese;
        HealthText.text = playerHealth.ToString();
        //playerHealth = playerHealth - healthDecrese;
    }
}

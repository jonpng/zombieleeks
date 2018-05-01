using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchboxPlus : MonoBehaviour {
    PlayerHealth playerHealth;                  // Reference to the player's health.
    GameObject player;                          // Reference to the player GameObject.
    int health;                                 // Reference to the amount of health taken.
    float sinkSpeed;                            // The speed at which the lunchbox sinks after being used.
    bool once;
    ParticleSystem healthParticles;
    public float speed = 12f;

    // Use this for initialization
    void Start () {
        healthParticles = GetComponentInChildren<ParticleSystem>();
        health = 50;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        sinkSpeed = 0.5f;
        once = true;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
		
	}

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Leek")
        {
            if (once)
            {
                // ...player takes health only once.
                playerHealth.TakeHealth(health);
                healthParticles.Emit(1);
                healthParticles.Stop();
                once = false;
            }
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            Destroy(gameObject, 2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchboxPlus : MonoBehaviour {
    PlayerHealth playerHealth;                  // Reference to the player's health.
    GameObject player;                          // Reference to the player GameObject.
    int health;                                 // Reference to the amount of health taken.
    float sinkSpeed;                            // The speed at which the lunchbox sinks after being used.
    bool once;

    // Use this for initialization
    void Start () {
        health = 50;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        sinkSpeed = 0.5f;
        once = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Player")
        {
            if (once)
            {
                // ...player takes health only once.
                playerHealth.TakeHealth(health);
                once = false;
            }
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            Destroy(gameObject, 2f);
        }
    }
}

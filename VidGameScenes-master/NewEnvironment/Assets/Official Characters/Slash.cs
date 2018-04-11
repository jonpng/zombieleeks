using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {
    public float timeBetweenAttacks = 0.2f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject enemy;                          // Reference to the enemy GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool enemyInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.
    charac player;
    bool isAttacking;

    void Awake()
    {
        // Setting up the references.
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemy = GetComponent<GameObject>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        player = GetComponent<charac>();
        isAttacking = false;
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the enemy...
        if (other.gameObject.CompareTag("Enemy"))
        {
            // ... the enemy is in range.
            enemyInRange = true;
            enemyHealth = other.GetComponent<EnemyHealth>();
            
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the enemy...
        if (other.gameObject.CompareTag("Enemy"))
        {
            // ... the enemy is no longer in range.
            enemyInRange = false;
            
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        isAttacking = player.isAtak;
        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && enemyInRange && enemyHealth.currentHealth > 0 && isAttacking)
        {

            // ... attack.
            Attack();
        }


        // If the player has zero or less health...
        //if (playerHealth.currentHealth <= 0)
        //{
        // ... tell the animator the player is dead.
        //    anim.SetTrigger("PlayerDead");
        //}
    }


    void Attack()
    {
        //anim.SetTrigger("Attack");
        // Reset the timer.
        timer = 0f;


        // If the enemy has health to lose...
        if (enemyHealth.currentHealth > 0)
        {
            // ... damage the enemy.
            enemyHealth.TakeDamage(attackDamage);
        }
    }
}

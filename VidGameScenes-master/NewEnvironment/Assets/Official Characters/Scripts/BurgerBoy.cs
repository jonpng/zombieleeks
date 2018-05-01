using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BurgerBoy : MonoBehaviour {

    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    Animator anim;
    Vector3 movement;                   // The vector to store the direction of the enemy's movement.
    public float speed = 10f;
    EnemyAttack IsAttacking;

    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("PlayerFound");
        //Debug.Log("found");
        IsAttacking = GetComponent<EnemyAttack>();
        
    }

    private void FixedUpdate()
    {
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;
        //anim.SetBool("IsRunning", true);
    }
    void Update()
    {
        // If the enemy and the player have health left...
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
            // ... set the destination of the nav mesh agent to the player.
            if (!IsAttacking.stopMoving)
            {
                nav.SetDestination(player.position);
                anim.SetTrigger("Running");
            }


        } else if (playerHealth.currentHealth < 0) {
			SceneManager.LoadScene ("EndGameMenu");
		}
        
            
        
        //}
        /// Otherwise...
        else
        {
        // ... disable the nav mesh agent.
         nav.enabled = false;
        }
    }
}

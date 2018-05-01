using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class IcyPunk : MonoBehaviour {

    // Use this for initialization
    Transform player;               // Reference to the player's position.
    Transform head;                 // Reference to ice cream head
    Transform body;                 // Reference to the body
    CapsuleCollider hC;   // Reference to ice cream head collider
    CapsuleCollider bC;
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    Animator anim;
    Vector3 movement;                   // The vector to store the direction of the enemy's movement.
    EnemyAttack IsAttacking;
    float cooldown = 6f;
    float timer = 6f;
    bool isAttacking = false;
    Vector3 chargePosition;

    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        head = transform.GetChild(0).transform;
        body = transform.GetChild(1).transform;
       // hC = head.GetComponent<CapsuleCollider>();
        //bC = head.GetComponent<CapsuleCollider>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        anim = body.GetComponent<Animator>();
        nav.speed = 2f * nav.speed;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isAttacking)
        {
            isAttacking = false;
            anim.SetTrigger("ChargeFinished");
            nav.isStopped = true;
            if (collision.gameObject.name == "Protagonist" || collision.gameObject.name == "Leek")
            {
                playerHealth.TakeDamage(60);
            } else
            {
                enemyHealth.TakeDamage(25);
            }
        }
    }

    void Update()
    {
        // If the enemy and the player have health left, and the charge isn't on cooldown, and the enemy isn't currently attacking
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 & timer >= cooldown && isAttacking == false && !Physics.Linecast(transform.position, player.position))
        {
            // ... set the destination of the nav mesh agent to the player.
            timer = 0;
            anim.SetTrigger("PlayerFound");
            isAttacking = true;
            chargePosition = player.position;
            nav.isStopped = false;
            nav.SetDestination(chargePosition);

        } //check if the enemy is near the position, reset if so and start the cooldown next update
        else if (isAttacking == true)
        {
            float goalDiff = Vector3.Distance(chargePosition, transform.position);
            if (goalDiff < 1.0f)
            {
                isAttacking = false;
                anim.SetTrigger("ChargeFinished");
            }
        } //if not attacking, increase the timer
        else if (isAttacking == false)
        {
            timer += Time.deltaTime;
        }
        else if (playerHealth.currentHealth < 0)
        {
            SceneManager.LoadScene("EndGameMenu");
        }
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }
}

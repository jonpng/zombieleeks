using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DonutDude : MonoBehaviour {

    Transform player;               // Reference to the player's position.
    Transform head;                 // Reference to ice cream head
    Transform body;                 // Reference to the body
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    Animator anim;
    float cooldown = 6f;
    float timer = 6f;
    bool isAttacking = false;
    bool headReturning = false;
    Vector3 throwPosition;
    Vector3 startPos;
    float step = 7f;
    bool hit = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        head = transform.GetChild(0).transform;
        startPos = head.position;
        body = transform.GetChild(1).transform;
        // hC = head.GetComponent<CapsuleCollider>();
        //bC = head.GetComponent<CapsuleCollider>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = body.GetComponent<Animator>();
        step = Time.deltaTime * step;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player);
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 & timer >= cooldown && isAttacking == false && !Physics.Linecast(transform.position, player.position))
        {
            // ... set the destination of the nav mesh agent to the player.
            timer = 0;
            anim.SetTrigger("PlayerFound");
            isAttacking = true;
            throwPosition = player.position + new Vector3(0.0f, startPos.y, 0.0f);
        }
        else if (isAttacking == true)
        {
            head.position = Vector3.MoveTowards(head.position, throwPosition, step);
            float goalDiff = Vector3.Distance(throwPosition, head.position);
            //Debug.Log(goalDiff);
            if (goalDiff < 0.1f)
            {
                if (headReturning == false)
                {
                    headReturning = true; // head should now move back
                    throwPosition = startPos; //now start moving the head back
                    //Debug.Log("moveback time");
                }
                else
                {
                    headReturning = false; //head is no longer returning1
                    head.position = startPos;//reset head
                    isAttacking = false;
                    hit = false;
                    anim.SetTrigger("AttackDone");
                }
            }
        }
        else
        {
            timer += Time.deltaTime;
        }

    }

    public void donutAttack(bool gotPlayer)
    {
        if (isAttacking && hit == false && gotPlayer)
        {
            playerHealth.TakeDamage(30);
            hit = true;
        }
        headReturning = true; // head should now move back
        throwPosition = startPos; //now start mo
    }
}

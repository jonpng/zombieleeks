using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;          // The amount of health the enemy starts the game with.
    public int currentHealth;                    // The current health the enemy has.
    public int scoreValue = 10;               // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;              // The sound to play when the enemy dies.
	GameObject leek;                          // The leek to die to.
	CapsuleCollider leekCol;

    EnemyManager manager;

    Animator anim;                              // Reference to the animator.
	Animator protanim;                              // Reference to the animator.
    AudioSource enemyAudio;              // Reference to the audio source.
    ParticleSystem hitParticles;             // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;     // Reference to the capsule collider.
    bool isDead;                                  // Whether the enemy is dead.
    bool isSinking;                               // Whether the enemy has started sinking through the floor.
    float timer = 0f;
    bool recoil = false;
    float timeBetweenAttack = 1.0f;

    void Start()
    {
		protanim = GameObject.Find ("Protagonist").GetComponent<Animator>();
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        deathClip = GetComponent<AudioClip>();
        hitParticles.Stop();
        if (capsuleCollider == null)
        {
            capsuleCollider = transform.GetChild(0).GetComponent<CapsuleCollider>();
        }
        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If the enemy should be sinking...
        if (recoil)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttack)
            {
                timer = 0;
                recoil = false;
            }
        }
      
    }

	void OnCollisionEnter (Collision col)
	{
		//Debug.Log (col.gameObject.name);
		if(col.gameObject.name == "Leek" && protanim.GetCurrentAnimatorStateInfo(0).IsName("Atak") && recoil == false)
		{
            recoil = true;
            Debug.Log("ow!");
            TakeDamage(25);
            hitParticles.Emit(1);
            hitParticles.Stop();
            enemyAudio.Play();
		}
	}

    public void TakeDamage(int amount)
    {
        
        if (isDead)
        {
            // ... no need to take damage so exit the function.
            hitParticles.Stop();
            
            return;
        }
        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;
        //Debug.Log(currentHealth);
        // If the current health is less than or equal to zero...
        if (currentHealth <= 0 )//&& isDead == false)
        {
            // ... the enemy is dead.
            //enemyAudio.PlayOneShot(deathClip);
            Death();
        }
        // If the enemy is dead...
        // Play the hurt sound effect.
        //enemyAudio.Play();

        // Set the position of the particle system to where the hit was sustained.
        //hitParticles.transform.position = hitPoint;

        // And play the particles.
        //hitParticles.Play();
    }



    void Death()
    {
        // The enemy is dead.
        isDead = true;
        
        //Debug.Log("dead!");
        // Turn the collider into a trigger so player can pass through it.
        if (capsuleCollider == null)
        {
            transform.GetChild(0).GetComponent<CapsuleCollider>().isTrigger = true;
            transform.GetChild(1).GetComponent<CapsuleCollider>().isTrigger = true;
        }
        else
        { 
            capsuleCollider.isTrigger = true;
        }
        // Increase the score by the enemy's score value.
        manager.KillUpdate(gameObject.transform.name);
        ScoreManager.score += scoreValue;
        // Tell the animator that the enemy is dead.
        if (anim == null)
        {
            anim = transform.GetChild(1).GetComponent<Animator>();
        }
        anim.SetBool("isDead", true);
        if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }
        // After 2 seconds destroy the enemy.
        Destroy(gameObject, 2.5f);
    }

    // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
    //enemyAudio.clip = deathClip;
    //enemyAudio.Play();
}



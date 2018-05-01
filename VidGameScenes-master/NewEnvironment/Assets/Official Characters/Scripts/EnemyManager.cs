using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    PlayerHealth playerHealth;
    public GameObject boi;
    public GameObject punk;
    public GameObject dude;

    public float initSpawnBoiTime;
    public float initSpawnPunkTime;
    public float initSpawnDudeTime; //when to start spawning 
    public float spawnBoiTime;
    public float spawnPunkTime;
    public float spawnDudeTime; // how often to spawn
    public Transform[] spawnPoints;

    public int numTotalBois;
    public int numTotalPunks;
    public int numTotalDudes; //to be decided by level
    public int totalEnemies;

    public int numAliveBois = 0;
    public int numAlivePunks = 0;
    public int numAliveDudes = 0; //number of currently alive enemies

    public int numDeadBois = 0;
    public int numDeadPunks = 0;
    public int numDeadDudes = 0; //number of currently dead enemies
    public int totalDead;

    public int spawnNumBois;
    public int spawnNumPunks;
    public int spawnNumDudes; //which spawn to use

    public bool isDone;

    void Start()
    { //at the designated init times, start spawning continuously
        InvokeRepeating("SpawnBois", initSpawnBoiTime, spawnBoiTime);
        InvokeRepeating("SpawnPunks", initSpawnPunkTime, spawnPunkTime);
        InvokeRepeating("SpawnDudes", initSpawnDudeTime, spawnDudeTime);
        playerHealth = GameObject.Find("Protagonist").GetComponent<PlayerHealth>();
        isDone = false;
        totalEnemies = numTotalBois + numTotalPunks + numTotalDudes;
        totalDead = 0;
    }


    void SpawnBois()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        } //if less than 4 bois are alive and more can spawn
        if (numAliveBois < 4 && (numAliveBois + numDeadBois) < numTotalBois)
        { //add one more alive boi
            numAliveBois += 1;
            Instantiate(boi, spawnPoints[spawnNumBois].position, spawnPoints[spawnNumBois].rotation);
        }
    }

    //spawns icy punks
    void SpawnPunks() {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }
        if (numAlivePunks < 3 && (numAlivePunks + numDeadPunks) < numTotalPunks)
        {
            numAlivePunks += 1;
            Instantiate(punk, spawnPoints[spawnNumPunks].position, spawnPoints[spawnNumPunks].rotation);
        }
    }

    //spawns donut dudes
    void SpawnDudes()
    {
        if (playerHealth.currentHealth <= 0f || numTotalDudes == 0)
        {
            return;
        }
        if (numAliveDudes < 1 && (numAliveDudes + numDeadDudes) < numTotalDudes)
        {
            numAliveDudes += 1;
            Instantiate(dude, spawnPoints[spawnNumDudes].position, spawnPoints[spawnNumDudes].rotation);
        }
    }

    public void KillUpdate(string name)
    {
        if (name.Equals("BurgerBoiWSound(Clone)"))
        {
            numAliveBois -= 1;
            numDeadBois += 1;
        } 
        else if (name.Equals("IcyPunkWSound(Clone)"))
        {
            numAlivePunks -= 1;
            numDeadPunks += 1;
        } else
        {
            numAliveDudes -= 1;
            numDeadDudes += 1;
        }
        totalDead += 1;
        if (totalDead == totalEnemies)
        {
            isDone = true;
        }
    }
}

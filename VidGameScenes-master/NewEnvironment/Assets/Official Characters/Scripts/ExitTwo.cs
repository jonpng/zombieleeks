using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTwo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Player" && GameObject.FindWithTag("Enemy") == null)
        {
            Debug.Log("player");
            // ... the player is in range.
            UnityEngine.SceneManagement.SceneManager.LoadScene("Cafeteria");
        }
    }
}

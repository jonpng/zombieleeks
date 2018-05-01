using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOne : MonoBehaviour {

    public string nextLevel;
    EnemyManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Player" && manager.isDone == true)
        {
            //Debug.Log("player");
            // ... the player is in range.
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);
        }
    }
}

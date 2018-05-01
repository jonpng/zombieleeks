using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOne : MonoBehaviour {

    public string nextLevel;
    EnemyManager manager;
    Animator fader;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        fader = GameObject.Find("Fader").GetComponent<Animator>();
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
            fader.SetBool("Fade", true);
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);
        }
    }
}

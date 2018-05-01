using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutHit : MonoBehaviour {

    DonutDude d;

	// Use this for initialization
	void Start () {
        d = transform.parent.gameObject.GetComponent<DonutDude>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name == "Protagonist" || collision.gameObject.name == "Leek")
        {
            d.donutAttack(true);
        }
        else
        {
            d.donutAttack(false);
        }
    }
}

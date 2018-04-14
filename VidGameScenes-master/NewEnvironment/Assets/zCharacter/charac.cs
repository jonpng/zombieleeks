using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class charac : MonoBehaviour {

	private Animator anim;
	private CharacterController controller;
	public float speed = 6.0f;
	public float turnSpeed = 100.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 0.0f;
	public bool isAtak = false;
    
    


	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponentInChildren<Animator> ();
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//make sure player on ground?
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);


		if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isAtak) 
		{
			speed = 6.0f;
			turnSpeed = 100.0f;
			isAtak = false;
		}
		if (Input.GetKey ("up")) {
			anim.SetInteger ("animP", 1);
		} else {
			anim.SetInteger ("animP", 0);
		}

		if (Input.GetKey ("x")) {
			speed = 0;
			turnSpeed = 0;
			anim.SetInteger ("animP", 0);
			anim.Play ("Sword");
			isAtak = true;
		}
		moveDirection = transform.forward * Input.GetAxis ("Vertical") * speed;

		float turn = Input.GetAxis ("Horizontal");
		transform.Rotate (0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move (moveDirection * Time.deltaTime);
	}
}

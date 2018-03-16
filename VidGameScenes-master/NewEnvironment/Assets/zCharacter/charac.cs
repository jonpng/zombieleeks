﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charac : MonoBehaviour {

	private Animator anim;
	private CharacterController controller;
	public float speed = 6.0f;
	public float turnSpeed = 60.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponentInChildren<Animator> ();
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("up")) {
			anim.SetInteger ("animP", 1);
		} else {
			anim.SetInteger ("animP", 0);
		}
		moveDirection = transform.forward * Input.GetAxis ("Vertical") * speed;

		float turn = Input.GetAxis ("Horizontal");
		transform.Rotate (0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move (moveDirection * Time.deltaTime);
	}
}

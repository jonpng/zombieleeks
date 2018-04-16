using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class player_input : MonoBehaviour {

	private Animator anim;
	private Rigidbody rbody;
	private CharacterController controller;
	private CharacterInputController cinput;

	private Transform leftFoot;
	private Transform rightFoot;

	public bool isGrounded;

	public float animationSpeed = 1f;
	public float rootMovementSpeed = 1f;
	public float rootTurnSpeed = 1f;


	void Awake()
	{

		anim = GetComponent<Animator>();

		if (anim == null)
			Debug.Log("Animator could not be found");

		rbody = GetComponent<Rigidbody>();

		if (rbody == null)
			Debug.Log("Rigid body could not be found");

		cinput = GetComponent<CharacterInputController>();
		if (cinput == null)
			Debug.Log("CharacterInput could not be found");
	}

	// Use this for initialization
	void Start () {
		leftFoot = this.transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
		rightFoot = this.transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg/mixamorig:RightFoot");

		if (leftFoot == null || rightFoot == null)
			Debug.Log("One of the feet could not be found");

		isGrounded = false;

		//never sleep so that OnCollisionStay() always reports for ground check
		rbody.sleepThreshold = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		anim.speed = animationSpeed;
	}

	void FixedUpdate()
	{

		float inputForward = 0f;
		float inputTurn = 0f;

		if (cinput.enabled) {
			inputForward = cinput.Forward;
			inputTurn = cinput.Turn;
		}

		anim.SetFloat("x", inputTurn);
		anim.SetFloat("y", inputForward);
		if (Input.GetKey (KeyCode.Joystick1Button1) || Input.GetKey("x")) {
			anim.SetBool ("attack", true);
		} else {
			anim.SetBool ("attack", false);
		}
		Debug.Log (anim.GetBool("attack"));
	}

	void OnCollisionStay(Collision collision)
	{
		isGrounded = true;

	}

	void OnAnimatorMove()
	{

		Vector3 newRootPosition;
		Quaternion newRootRotation;

		if (isGrounded)
		{
			//use root motion as is if on the ground		
			newRootPosition = anim.rootPosition;
		}
		else
		{
			//Simple trick to keep model from climbing other rigidbodies that aren't the ground
			newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
		}

		//use rotational root motion as is
		newRootRotation = anim.rootRotation;

		//TODO Here, you could scale the difference in position and rotation to make the character go faster or slower

		//        this.transform.position = newRootPosition;
		//        this.transform.rotation = newRootRotation;
		this.transform.position = Vector3.LerpUnclamped (this.transform.position, newRootPosition, rootMovementSpeed);
		this.transform.rotation = Quaternion.LerpUnclamped (this.transform.rotation, newRootRotation, rootTurnSpeed);

		//clear IsGrounded
		isGrounded = false;
	}
}

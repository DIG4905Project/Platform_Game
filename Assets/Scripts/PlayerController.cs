using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	//player handling
	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 50;
	public float jumpHeight = 12;

	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;

	private PlayerPhysics playerPhysics;

	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics> ();
	}
	
	// Update is called once per frame
	void Update () {
		targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
		currentSpeed = IncrementTowards (currentSpeed, targetSpeed, acceleration);

		if (playerPhysics.grounded) {
			amountToMove.y = 0; //resets gravity's effects

			//Jump
			if(Input.GetButtonDown ("Jump")) {
				amountToMove.y = jumpHeight;
			}
		}

		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime; //set to 0 if you don't want gravity
		playerPhysics.Move (amountToMove * Time.deltaTime);
	}

	//increase n towards target by speed
	private float IncrementTowards(float n, float target, float a) {
				if (n == target) {
						return n;
				} else {
						float dir = Mathf.Sign (target - n); //which direction to move n to get closer to target?
						n += a * Time.deltaTime * dir;
						return (dir == Mathf.Sign (target - n)) ? n : target; //if n has now passed target then return target, otherwise return n
				}
		}
}

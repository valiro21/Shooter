using UnityEngine;
using System.Collections;

public class FPSWalking : MonoBehaviour {
	CharacterController CC;
	Vector3 velocity;
	public float MoveSpeed = 3f, JumpForce = 5f, GravityMultiplier = 1f, MaxGravityForce = 15f;
	const float g = -9.81f;

	// Update is called once per frame
	void Awake () {
		CC = GetComponent<CharacterController> ();
	}

	void Update () {
		velocity.x = velocity.z = 0f;
		if (Input.GetKey (KeyCode.W))
			velocity += transform.forward * MoveSpeed;

		if (Input.GetKey (KeyCode.S))
			velocity -= transform.forward * MoveSpeed;

		if (Input.GetKey (KeyCode.D))
			velocity += transform.right * MoveSpeed;

		if (Input.GetKey (KeyCode.A))
			velocity -= transform.right * MoveSpeed;

		if (!CC.isGrounded)
			velocity.y += GravityMultiplier * g * Time.deltaTime;
		else
			velocity.y = 0f;

		if (Input.GetKey (KeyCode.Space) && CC.isGrounded)
			velocity.y = JumpForce;

		CC.Move ( velocity * Time.deltaTime );
	}
}

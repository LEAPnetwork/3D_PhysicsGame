using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class SpaceMovement : MonoBehaviour {

	[Range(1f,30f)]
	public float maxSpeed = 10f;

	[Range(0.01f,1f), Tooltip("1 means fast acceleration and 0.01 means slow")]
	public float acceleration = 0.1f;

	[Range(0, 0.1f)]
	public float rotationSpeedModifier = 0.05f;

	private float currentSpeed = 0f;
	private Vector3 move;

	private Transform cameraTransform;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	public void Move (float input) {
		input = Mathf.Clamp (input, -1, 1);

		float speedIncrement = Mathf.Lerp (-acceleration, acceleration, (input / 2f) + 0.5f);
		currentSpeed += speedIncrement < 0 ? speedIncrement * 0.25f : speedIncrement;

		if (input < 0.05f && input > -0.05f) {
			currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.01f);
		}

		if (currentSpeed < maxSpeed * -0.25f) {
			currentSpeed = maxSpeed * -0.25f;
		}

		if (currentSpeed > maxSpeed) {
			currentSpeed = maxSpeed;
		}

		move = transform.forward * currentSpeed;
		if (float.IsNaN (move.x) || float.IsNaN (move.y) || float.IsNaN (move.z)) {
			move = Vector3.zero;
		}
		rb.velocity = move;
	}

	public void Rotate(){
		float rotationSpeed = Quaternion.Angle (transform.rotation, cameraTransform.rotation);
		rotationSpeed = Mathf.Clamp01(Mathf.Abs(rotationSpeed / 90f));
		rotationSpeed = rotationSpeed * rotationSpeedModifier;

		transform.rotation = Quaternion.Lerp (transform.rotation, cameraTransform.rotation, rotationSpeed);
	}

	void OnCollisionEnter(Collision col){
		currentSpeed = -Mathf.Sqrt (currentSpeed);
		rb.velocity = col.contacts [0].normal * rb.velocity.sqrMagnitude;
	}
}

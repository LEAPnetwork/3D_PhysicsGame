using UnityEngine;
using System.Collections;

public class FlyBullet : MonoBehaviour {

	public Rigidbody rb;

	public float movementSpeed;

	void Update () {
		rb.velocity = transform.forward * movementSpeed;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.GetComponent<DamagableObject> () != null) {
			col.gameObject.GetComponent<DamagableObject> ().AddHealth(-10);
		}

		Destroy (gameObject);
	}

}

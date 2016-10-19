using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SpaceObject : MonoBehaviour {

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = Random.onUnitSphere;
		rb.useGravity = false;
		rb.angularDrag = 0f;
	}

	public void AddExternalForce(Vector3 force){
		rb.velocity += force;
	}

}

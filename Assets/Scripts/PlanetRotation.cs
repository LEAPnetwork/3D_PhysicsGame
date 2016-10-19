using UnityEngine;
using System.Collections;

public class PlanetRotation : MonoBehaviour {

	[Range(-10f,10f)]
	public float rotationSpeed = 10f;
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, transform.up, rotationSpeed * Time.deltaTime);
	}
}

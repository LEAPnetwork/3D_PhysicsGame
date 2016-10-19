using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpaceObject),typeof(DamagableObject))]
public class Meteor : Killable {

	public float blastForce = 10f;

	public override void Kill(){
		foreach (Transform t in GetComponentsInChildren<Transform>()) {
			if (t != transform) {
				t.SetParent (null);
				Rigidbody rb = t.gameObject.AddComponent<Rigidbody> ();
				rb.useGravity = false;
				rb.AddForce (Random.onUnitSphere * blastForce);
			}
		}

		Destroy (gameObject);
	}

}

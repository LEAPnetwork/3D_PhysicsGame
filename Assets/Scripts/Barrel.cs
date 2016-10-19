using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpaceObject),typeof(DamagableObject))]
public class Barrel : Killable {

	[Range(50, 1000)]
	private int healthValue = 300;
		
	public override void Kill(){
		StartCoroutine (Explode ());
	}

	IEnumerator Explode(){
		yield return new WaitForSeconds (3f);
		Collider[] colliders = Physics.OverlapSphere (transform.position, 30f);

		foreach (Collider c in colliders) {
			if (c.gameObject != gameObject) {

				DamagableObject DO = c.gameObject.GetComponent<DamagableObject> ();
				if (DO != null) {
					DO.AddHealth (-50);
				}

				SpaceObject SO = c.gameObject.GetComponent<SpaceObject> ();
				if (SO != null) {
					Vector3 direction = (c.transform.position - transform.position).normalized;
					SO.AddExternalForce (direction * 20);
				}
				Debug.Log ("I hit " + c.gameObject.name);
			}
		}

		Debug.Log ("I exploded!");
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col){
		DamagableObject target = col.GetComponent<DamagableObject> ();
		if (target != null) {
			target.AddHealth (healthValue);
			Destroy (gameObject);
		}
	}

}
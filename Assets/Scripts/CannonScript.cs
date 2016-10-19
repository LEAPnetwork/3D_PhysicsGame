using UnityEngine;
using System.Collections;

public class CannonScript : MonoBehaviour {

	public Transform skudPos1;
	public Transform skudPos2;

	public GameObject bullet;

	private bool erHøjreKannonsTur = true;
	
	// Update is called once per frame
	public void Shoot (float stability) {
		if (erHøjreKannonsTur) {
			Quaternion newRotation = Quaternion.AngleAxis(Random.Range(-stability,stability), (transform.right * Random.Range(-1,1) + transform.up * Random.Range(-1,1)).normalized);
			GameObject go = Instantiate (bullet, skudPos2.position, newRotation * transform.rotation) as GameObject;
			Destroy (go, 10f);
		} else {
			Quaternion newRotation = Quaternion.AngleAxis(Random.Range(-stability,stability), (transform.right * Random.Range(-1,1) + transform.up * Random.Range(-1,1)).normalized);
			GameObject go = Instantiate (bullet, skudPos1.position, newRotation * transform.rotation) as GameObject;
			Destroy (go, 10f);
		}
		erHøjreKannonsTur = !erHøjreKannonsTur;
	}
}

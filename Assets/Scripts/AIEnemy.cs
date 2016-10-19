using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpaceMovement),typeof(DamagableObject),typeof(CannonScript))]
public class AIEnemy : Killable {

	private SpaceMovement spaceMovement;
	private CannonScript cannon;
	private DamagableObject DO;
	private Transform player;

	public enum AIStates{
		IDLE,
		CHASING,
		FLEEING,
		HEALING,
		PATROLLING
	}

	private AIStates myState;

	private Quaternion targetRot;
	private Vector3 targetPos;
	private Transform target;

	[Range(3,10)]
	public float stability = 5;

	private float movementInput = 0f;

	// Use this for initialization
	void Start () {
		myState = AIStates.IDLE;
		targetRot = Random.rotation;

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		spaceMovement = GetComponent<SpaceMovement> ();
		DO = GetComponent<DamagableObject> ();
		cannon = GetComponent<CannonScript> ();

		StartCoroutine (CheckForPlayer ());
	}
	
	// Update is called once per frame
	void Update () {
		switch (myState) {
		case AIStates.IDLE:
			transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, 0.5f);
			movementInput = 0f;

			if (Quaternion.Dot (transform.rotation, targetRot) > 0.9f) {
				GetNewState ();
			}
			//randomly rotates in space
			break;
		case AIStates.PATROLLING:
			movementInput = 1f;
			transform.LookAt (targetPos);

			if (Vector3.Distance (transform.position, targetPos) < 30f) {
				GetNewState ();
			}
			//moves to a specific location
			break;
		case AIStates.HEALING:
			if (target != null) {
				movementInput = 1f;
				transform.LookAt (target.position);
			} else {
				GetNewState ();
			}
			//moves to closest space barrel
			break;
		case AIStates.CHASING:
			float distance = Vector3.Distance (transform.position, player.position);
			if (distance > 40f) {
				movementInput = 1f;
				if (distance > 80) {
					myState = AIStates.IDLE;
				}
			}
			transform.LookAt (player.position);

			//moves towards player
			break;
		case AIStates.FLEEING:
			if (Vector3.Distance (transform.position, player.position) < 150f) {
				movementInput = 1f;
				transform.LookAt (player.position - transform.position);
			} else {
				GetNewState ();
			}

			//moves away from player
			break;
		}

		spaceMovement.Move (movementInput);
	}

	void GetNewState(){
		if (DO.CurHealth / DO.health < 0.2f) {
			myState = AIStates.HEALING;
		} else {
			if (myState != AIStates.CHASING) {
				if (Random.value > 0.8f) {
					myState = AIStates.IDLE;
					targetRot = Random.rotation;
				} else {
					myState = AIStates.PATROLLING;
					Vector3 dir = Random.onUnitSphere;
					targetPos = transform.position + new Vector3 (dir.x, dir.y * 0.3f, dir.z).normalized * Random.Range (50f, 100f);
				}
			}
		}
	}

	IEnumerator CheckForPlayer(){
		yield return new WaitForSeconds (5f);
		Vector3 localCoords = transform.InverseTransformPoint (player.position);
		if (localCoords.z > 0.7f) {
			if (Vector3.Distance (transform.position, player.position) < 60) {
				myState = AIStates.CHASING;
				StartCoroutine (Shoot());
			}
		}
		StartCoroutine (CheckForPlayer ());
	}

	IEnumerator Shoot(){
		yield return new WaitForSeconds (Random.Range(1f,2.4f));
		cannon.Shoot (stability);

		if (myState == AIStates.CHASING) {
			StartCoroutine (Shoot ());
		}
	}

	public override void Kill ()
	{
		Debug.Log ("Enemy killed!");
		Destroy (gameObject);
	}

}

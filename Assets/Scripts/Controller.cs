using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpaceMovement),typeof(CannonScript))]
public class Controller : MonoBehaviour {

	public string axis = "Vertical";
	private SpaceMovement spaceMovement;
	private CannonScript cannon;

	[Range(0f,10f)]
	public float stability = 3f;

	void Start(){
		spaceMovement = GetComponent<SpaceMovement> ();
		cannon = GetComponent<CannonScript> ();
	}

	void Update () {
		float v = Input.GetAxis (axis);
		spaceMovement.Move (v);
		spaceMovement.Rotate ();
		if (Input.GetMouseButtonDown (0)) {
			cannon.Shoot (stability);
		}
	}
}

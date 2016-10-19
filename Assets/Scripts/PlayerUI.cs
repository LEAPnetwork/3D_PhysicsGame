using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {

	public string tagToTrack = "Player";
	private DamagableObject player;
	private int xPos = 10;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag (tagToTrack).GetComponent<DamagableObject> ();
		if (tagToTrack != "Player") {
			xPos = Screen.width - 110;
		}
	}

	void OnGUI(){
		int hpPercentage;

		if (player.CurHealth != 0) {
			hpPercentage = player.CurHealth / player.health;
		} else {
			hpPercentage = 0;
		}

		GUI.Box (new Rect (xPos, 10, hpPercentage * 100, 30),"");
		GUI.Box (new Rect (xPos, 10, 100, 30), player.CurHealth + " / " + player.health);

		if (player.objectHasShield) {
			int shieldPercentage;

			if (player.CurShieldHealth != 0) {
				hpPercentage = player.CurShieldHealth / player.shieldHealth;
			} else {
				hpPercentage = 0;
			}

			GUI.Box (new Rect (xPos, 45, player.CurShieldHealth / player.shieldHealth * 100, 30), "");
			GUI.Box (new Rect (xPos, 45, 100, 30), player.CurShieldHealth + " / " + player.shieldHealth);
		}
	}

}

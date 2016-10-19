using UnityEngine;
using System.Collections;

public class DamagableObject : MonoBehaviour {

	[Range(10,1000)]
	public int health;
	private int curHealth;

	public bool objectHasShield = false;
	[Range(100,3000)]
	public int shieldHealth;
	private int curShieldHealth;

	public int CurHealth {	
		get{ return curHealth; }
	}
	public int CurShieldHealth {	
		get{ return curShieldHealth; }	
	}

	// Use this for initialization
	void Start () {
		curHealth = health;
		if (objectHasShield) {
			curShieldHealth = shieldHealth;
		}
	}

	/// <summary>
	/// Adds healthPickup to current health.
	/// </summary>
	/// <param name="healthPickup">The value to add to your health</param>
	public void AddHealth(int healthPickup){
		if(healthPickup < 0){
			if (objectHasShield) {
				if (curShieldHealth >= 0) {
					curShieldHealth += healthPickup;
					if (curShieldHealth < 0) {
						curHealth += curShieldHealth;
						curShieldHealth = 0;
					}
				}
			} else {
				curHealth += healthPickup;
			}
			StopAllCoroutines ();
			StartCoroutine (WaitForShieldRecharge ());
		} else {
			curHealth += healthPickup;
			if (curHealth > health) {
				curHealth = health;
			}
		}

		if (curHealth < 0) {
			gameObject.GetComponent<Killable> ().Kill();
		}

	}

	private IEnumerator WaitForShieldRecharge(){
		yield return new WaitForSeconds (20 / Mathf.Sqrt(shieldHealth));
		StartCoroutine (RechargeShield ());
	}

	private IEnumerator RechargeShield(){
		yield return new WaitForSeconds (0.05f);
		curShieldHealth += shieldHealth / 50;
		if (curShieldHealth > shieldHealth) {
			curShieldHealth = shieldHealth;
		} else {
			StartCoroutine (RechargeShield ());
		}
	}
}

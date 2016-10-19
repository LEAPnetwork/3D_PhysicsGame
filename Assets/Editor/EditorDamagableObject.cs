using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[CustomEditor(typeof(DamagableObject))]
public class EditorDamagableObject : Editor {

	public override void OnInspectorGUI(){
		DamagableObject DO = (DamagableObject)target;

		DO.health = EditorGUILayout.IntSlider("Max Health", DO.health, 100, 1000);

		if (DO.objectHasShield) {
			DO.shieldHealth = EditorGUILayout.IntSlider ("Max Shield Health", DO.shieldHealth, 200, 3000);
			if (GUILayout.Button ("Remove Shield")) {
				DO.objectHasShield = false;
			}
		} else {
			if (GUILayout.Button ("Add Shield")) {
				DO.objectHasShield = true;
			}
		}

		if (DO.gameObject.CompareTag ("Player")) {
			if (DO.gameObject.GetComponent<PlayerUI> () == null) {
				DO.gameObject.AddComponent<PlayerUI> ();
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision g_collision) {
		GameObject t_hit = g_collision.gameObject;
		Health t_health = t_hit.GetComponent<Health> ();

		if (t_health != null) {
			t_health.TakeDamage (10);
		}

		Destroy (this.gameObject);
	}
}

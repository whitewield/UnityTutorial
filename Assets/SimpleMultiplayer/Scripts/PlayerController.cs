using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Transform bulletSpawn;

	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}
	}

	[Command]
	private void CmdFire () {
		//create bullet 

		GameObject t_bullet = Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		//add velocity to the bullet

		t_bullet.GetComponent<Rigidbody> ().velocity = bulletSpawn.transform.forward * 6.0f;

		//Spawn the bullet on the Clients

		NetworkServer.Spawn (t_bullet);

		//destory bullet after 2 seconds

		Destroy (t_bullet, 2);
	}

	public override void OnStartLocalPlayer () {
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}
}

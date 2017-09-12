using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	
	// Update is called once per frame
	void Update () {
		
	}


	public const int maxHealth = 100;
	[SyncVar (hook = "OnChangeHealth")][SerializeField] int currentHealth = maxHealth;
	[SerializeField] RectTransform healthBar;
	[SerializeField] bool destroyOnDeath;
	private NetworkStartPosition[] spawnPoints;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		}
	}

	public void TakeDamage (int g_damage) {
		if (!isServer) {
			return;
		}
		currentHealth -= g_damage;
		if (currentHealth <= 0) {
			if (destroyOnDeath) {
				Destroy (this.gameObject);
			} else {
				currentHealth = maxHealth;
				RpcRespawn ();
			}
		}

	}

	void OnChangeHealth (int g_health) {
		healthBar.sizeDelta = new Vector2 (g_health * 2, healthBar.sizeDelta.y);
	}

	[ClientRpc] 
	void RpcRespawn () {
		if (isLocalPlayer) {
			Vector3 t_spawnPoint = Vector3.zero;
			if (spawnPoints != null && spawnPoints.Length > 0) {
				t_spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
			}

			transform.position = t_spawnPoint;
		}
	}
}

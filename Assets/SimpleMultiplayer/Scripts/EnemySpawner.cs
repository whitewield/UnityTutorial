using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

	[SerializeField] GameObject enemyPrefab;
	[SerializeField] int numberOfEnemies;

	public override void OnStartServer ()
	{
		for (int i = 0; i < numberOfEnemies; i++) {
			Vector3 t_spawnPosition = new Vector3 (Random.Range (-8.0f, 8.0f), 0, Random.Range (-8.0f, 8.0f));
			Quaternion t_spawnRotation = Quaternion.Euler (0.0f, Random.Range (0f, 180f), 0);

			GameObject t_enemy = Instantiate (enemyPrefab, t_spawnPosition, t_spawnRotation) as GameObject;
			NetworkServer.Spawn (t_enemy);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

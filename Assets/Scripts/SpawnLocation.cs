using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnLocation : MonoBehaviour {

	// element to spawn
	public GameObject enemy;
	public int TotalEnemies     = 10;
	public float TimeToSpawn   = 1f;

	// hold all enemies
	private GameObject[] enemies;


	// Loop Spawning enemies
	private IEnumerator SpawnLoop() 
	{
		// Defining the Spawning Position
		StartCoroutine( ChangePosition() );
		yield return new WaitForSeconds(0.2f);

		// Spawning the enemies
		int i = 0;
		while ( i <= (TotalEnemies-1) ) {

			enemies[i] = SpawnElement();
			i++;
			yield return new WaitForSeconds(Random.Range(TimeToSpawn,TimeToSpawn*3));
		}
	}

	// Spawn an enemy
	private GameObject SpawnElement() 
	{
		// spawn the element on a random position, inside a imaginary sphere
		GameObject baddy = Instantiate(enemy, (Random.insideUnitSphere*4) + transform.position, transform.rotation ) as GameObject;
		// define a random scale for the enemy
		float scale = Random.Range(0.5f, 2f);
		// change the cube scale
		baddy.transform.localScale = new Vector3( scale, scale, scale );
		baddy.transform.Rotate (0, 90, 0);
		return baddy;
	}

	// define if position was set
	private bool mPositionSet;
	// Use this for initialization
	private bool SetPos () 
	{
		Transform camera = Camera.main.transform;

		transform.position = camera.forward * 10;
		return true;
	}

	void Start () 
	{
		// Initializing spawning loop
		StartCoroutine( SpawnLoop() );
		enemies = new GameObject[ TotalEnemies ];
	}
		
	// delay before setting the position
	private IEnumerator ChangePosition() {

		yield return new WaitForSeconds(0.2f);
		if ( !mPositionSet ){
			if ( VuforiaBehaviour.Instance.enabled )
				SetPos();
			mPositionSet = false;
		}
	}

}

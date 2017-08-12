using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourScript : MonoBehaviour {

	//Max/Min scale
	public float ScaleMax  = 2f;
	public float ScaleMin  = 0.5f;

	// Orbit max Speed
	public float OrbitMaxSpeed = 30f;

	// Orbit speed
	private float OrbitSpeed;

	// Anchor point
	private Transform OrbitAnchor;

	// Orbit direction
	private Vector3 OrbitDirection;

	// Max Scale
	private Vector3 MaxScale;

	// Growing Speed
	public float GrowingSpeed  = 10f;
	private bool IsScaled  = false;

	void Start () {
		SphereSettings();
	}

	private void SphereSettings(){
		// defining the anchor point as the main camera
		OrbitAnchor = Camera.main.transform;

		// defining the orbit direction
		float x = Random.Range(-1f,1f);
		float y = Random.Range(-1f,1f);
		float z = Random.Range(-1f,1f);
		OrbitDirection = new Vector3( x, y , z );

		// defining speed
		OrbitSpeed = Random.Range( 5f, OrbitMaxSpeed );

		// defining scale
		float scale = Random.Range(ScaleMin, ScaleMax);
		MaxScale = new Vector3( scale, scale, scale );

		// set scale to 0, to grow it later
		transform.localScale = Vector3.zero;
	}

	void Update () {
		// makes the sphere orbit and rotate
		RotateSphere();
		if ( !IsScaled )
			ScaleObj();
	}
	private void ScaleObj(){

		// growing obj
		if ( transform.localScale != MaxScale )
			transform.localScale = Vector3.Lerp( transform.localScale, MaxScale, Time.deltaTime * GrowingSpeed );
		else
			IsScaled = true;
	}

	// Makes the sphere rotate around a anchor point
	// and rotate around its own axis
	private void RotateSphere(){
		// rotate cube around camera
		transform.RotateAround(
			OrbitAnchor.position, OrbitDirection, OrbitSpeed * Time.deltaTime);

		// rotating around its axis
		transform.Rotate( OrbitDirection * 30 * Time.deltaTime);
	}


	public int mCubeHealth  = 100;

	// Define if the Cube is Alive
	private bool mIsAlive       = true;

	// Cube got Hit
	// return 'false' when cube was destroyed
	public bool Hit( int hitDamage ){
		mCubeHealth -= hitDamage;
		if ( mCubeHealth >= 0 && mIsAlive ) {
			StartCoroutine( DestroyCube());
			return true;
		}
		return false;
	}

	// Destroy Cube
	private IEnumerator DestroyCube(){
		mIsAlive = false;

		// Make the cube desappear
		GetComponent<Renderer>().enabled = false;

		// we'll wait some time before destroying the element
		// this is usefull when using some kind of effect
		// like a explosion sound effect.
		// in that case we could use the sound lenght as waiting time
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
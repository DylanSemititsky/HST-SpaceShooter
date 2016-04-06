using UnityEngine;
using System.Collections;

public class RotatingEnemyScript_3_quad : MonoBehaviour {

	//this enemy travels to the center of the screen then unleashes hell upon the player

	public float speed;
	public GameObject laser1;
	public GameObject laser2;
	public GameObject laser3;
	public GameObject laser4;

	public float rateOfFire;
	private float nextFire;
	private bool movement = true;
	public float rotateSpeed;
	//public bool rotationDirection; //check which direction the object is facing
	[HideInInspector]
	public Vector3 frontDirection;//used to brag direction for lasers
	public Vector3 sideDirection;




	void Start () {

	}
	

	void Update () {
		
		if (movement) {//movement is true until z is less than 6.
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
		if(transform.position.z <= 8.5f) // stop position once enemy is half way into plane
		{
			movement = false;
		
				//transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);//clock wise rotation
		
				transform.Rotate (Vector3.down * rotateSpeed * Time.deltaTime);//counter clock wise rotation

			frontDirection = transform.forward;//grab front direction
			sideDirection = transform.right;
		}

		if (Time.time > nextFire && movement == false) {//rate of fire mechanic
			nextFire = Time.time + rateOfFire;
			FireLaser ();

		}





	}

	public void FireLaser(){ // fire laser
		
			Instantiate (laser1, transform.position, Quaternion.identity);
			Instantiate (laser2, transform.position, Quaternion.identity);
			Instantiate (laser3, transform.position, Quaternion.identity);
			Instantiate (laser4, transform.position, Quaternion.identity);
	}



}


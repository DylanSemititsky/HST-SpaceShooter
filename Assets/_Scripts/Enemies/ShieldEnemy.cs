using UnityEngine;
using System.Collections;

public class ShieldEnemy : MonoBehaviour {

	public float speed;
	public float rotateSpeed;
	public GameObject homingLaser;
	bool movement = true;
	bool hasFired;
	float counter;
	GameObject rotateBone;
	//HOMING LASER VARIABLES
	public int homingLaserWaveAmount; // how many homing laser execute
	public float homingLaserFireRate; //at which rate the homing lasers fire out together
	public float homingLaserWaveRate; // between homing waves how much time is in between each shot

	void Start () {
		rotateBone = gameObject.transform.GetChild (0).gameObject; //grab shield bone
	}
	
	void Update () {

		CheckHomingLaser ();//check to see if homing laser is shot so it can be re shot again
		AttackSequence();

		if (movement) {//movement is true until z is less than 6.
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
		if(transform.position.z <= 8.5f) // stop position once enemy is half way into plane
		{
			movement = false;

			// rotates shield around enemy
			rotateBone.transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
		}
	}
	IEnumerator ShootHomingLaser(float laserRate){ //create homing laser

		for (int i = 0; i < homingLaserWaveAmount; i++) { //fires the laser X amount of times
			Instantiate (homingLaser, transform.position, Quaternion.identity); //creates laser
			yield return new WaitForSeconds (laserRate); //waits until laser is shot and shoots again
		}
	}
	void AttackSequence(){

		if (movement == false && hasFired == false) {
			StartCoroutine (ShootHomingLaser (homingLaserFireRate));
			hasFired = true;
		}

	}
	void CheckHomingLaser(){
		if (movement == false) {
			counter += Time.deltaTime;
		}

		if (counter >= homingLaserWaveRate) { 
			counter = 0;
			hasFired = false;
		}
	}

}

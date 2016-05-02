using UnityEngine;
using System.Collections;

public class ShieldEnemy : MonoBehaviour {


	//DEFAULT MOVEMENT VARIABLES
	public float horizontalSpeed; //bosses horizontal speed
	public float verticalSpeed; // bosses vertical speed
	public float vAplitude; // the max vertical range the boss will travel
	public float hAplitude; // the max width range the boss with travel
	Vector3 tempPosition ;
	Vector3 originPos;
	bool enteredPlaySpace;


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


		if (!movement) {

			/*if (verticalSpeed < 1f) {
				verticalSpeed += Time.deltaTime * 0.2f;
			}
			tempPosition.x = Mathf.Sin (Time.realtimeSinceStartup * horizontalSpeed) * hAplitude;
			tempPosition.z = Mathf.Sin (Time.realtimeSinceStartup* verticalSpeed) * vAplitude;
			tempPosition.z += originPos.z;
			transform.position = tempPosition;*/
		}

		CheckHomingLaser ();//check to see if homing laser is shot so it can be re shot again
		AttackSequence();

		if (movement) {//movement is true until z is less than 6.
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
		if(transform.position.z <= 6.0f && movement == true) // stop position once enemy is half way into plane
		{
			tempPosition = transform.position;
			originPos = transform.position;

			movement = false;
		}
		if (!movement) {
			RotateShield ();
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
	void RotateShield(){
		// rotates shield around enemy
		rotateBone.transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);

	}

}

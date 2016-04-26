using UnityEngine;
using System.Collections;

public class Boss3Script : MonoBehaviour {

	//EnterPlaySpace Variables
	public float enterSpeed;
	bool hasEntered = false;
	//Side speed
	public float sideSpeed;
	bool dirRight;
	//grab turret objects
	GameObject[] turret;
	GameObject mainTurret;

	public float bottomTurretLaserRate;
	public float bottomTurretFireRate;
	public float topTurretLaserRate;

	public float homingLaserWaveAmount;
	public float homingLaserFireRate;
	public float homingLaserWaveRate;
	bool hasFired;
	bool hasFiredBottomLaser;
	bool hasFiredTopLaser;
	public GameObject homingLaser;
	public GameObject turretLaser;
	float counter;
	float bottomLaserCounter;
	float topLaserCounter;

	public int MainTurretHealth;
	public int turret1Health;
	public int turret2Health;
	public int turret3Health;
	public int turret4Health;

	bool turret1Exists = true;
	bool turret2Exists = true;
	bool turret3Exists = true;
	bool turret4Exists = true;

	//GameController Access
	//GameController gameController;

	void Start () {
		
		turret = new GameObject[4];//set all turret objects on
		for (int i = 0; i < turret.Length; i++) {
			turret [i] = gameObject.transform.GetChild (i).gameObject;
		}
		mainTurret = GameObject.Find ("Boss3");

		/*GameObject gameControllerObject = GameObject.Find ("GameController");	
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}*/
	}
	
	void Update () {
		EnterPlaySpace (); //enter play space
		//BossMovement (); //move left to right
		SideToSideMovement();

		CheckHomingLaser ();
		AttackSequence(); //shoots homing lasers

		CheckBottomLaser ();
		AttackBottomLaser ();

		AttackTopLaser ();
		CheckTopLaser ();

		CheckDamage ();
	}

	//enter play space
	//movement, left right
	//shoot home laser turret
	//main laser turret
	//check health
	//death


	void EnterPlaySpace(){
		if (hasEntered == false) {
			
			transform.Translate (Vector3.forward * -enterSpeed * Time.deltaTime);

			if (transform.position.z <= 10f) {
				hasEntered = true;

				Vector3 temp = transform.position; // sets the z position to an absolute one
				temp.z = 10f;
				transform.position = temp;
				//tempPosition = transform.position;
				//originPos = transform.position;
			}
		}
	}
	void SideToSideMovement(){
		if (dirRight) {
			transform.Translate (Vector3.right * sideSpeed * Time.deltaTime);
		} else {
			transform.Translate (-Vector3.right * sideSpeed * Time.deltaTime);
		}

		if (transform.position.x >= 2.0f) {
			dirRight = false;
		}
		if (transform.position.x <= -2.0f) {
			dirRight = true;
		}
	}
	IEnumerator ShootHomingLaser(float laserRate){ //create homing laser
		Vector3 temp = mainTurret.transform.position; // sets the z position to an absolute one
		temp.y = 0f;
		//transform.position = temp;

		for (int i = 0; i < homingLaserWaveAmount; i++) { //fires the laser X amount of times
			
			Instantiate (homingLaser, transform.position, Quaternion.identity); //creates laser

			yield return new WaitForSeconds (laserRate); //waits until laser is shot and shoots again
		}
	}
	IEnumerator BottomTurretShot(float laserRate){
		if (turret1Exists) {
			Instantiate (turretLaser, turret [0].transform.position, Quaternion.identity);
		}
		if (turret2Exists) {
			Instantiate (turretLaser, turret [1].transform.position, Quaternion.identity);
		}
		yield return new WaitForSeconds (laserRate);

	}
	IEnumerator TopTurretShot(float laserRate){
		if (turret3Exists) {
			Instantiate (turretLaser, turret [2].transform.position, Quaternion.identity);
		}
		if (turret4Exists) {
			Instantiate (turretLaser, turret [3].transform.position, Quaternion.identity);
		}
		yield return new WaitForSeconds (laserRate);

	}
	void CheckBottomLaser(){
		if (hasEntered == true) {
			bottomLaserCounter += Time.deltaTime;
		}
		if (bottomLaserCounter >= bottomTurretLaserRate) {
			bottomLaserCounter = 0;
			hasFiredBottomLaser = false;
		}

	}
	void CheckTopLaser(){
		if (hasEntered == true) {
			topLaserCounter += Time.deltaTime;
		}
		if (topLaserCounter >= topTurretLaserRate) {
			topLaserCounter = 0;
			hasFiredTopLaser = false;
		}

	}
	void CheckHomingLaser(){
		if (hasEntered == true) {
			counter += Time.deltaTime;
		}

		if (counter >= homingLaserWaveRate) { 
			counter = 0;
			hasFired = false;
		}
	}
	void AttackSequence(){

		if (hasEntered == true && hasFired == false) {
			StartCoroutine (ShootHomingLaser (homingLaserFireRate));
			hasFired = true;
		}

	}
	void AttackBottomLaser(){
		if (hasEntered == true && hasFiredBottomLaser == false) {
			StartCoroutine (BottomTurretShot(bottomTurretFireRate));
			hasFiredBottomLaser = true;
		}
	}
	void AttackTopLaser(){
		if (hasEntered == true && hasFiredTopLaser == false) {
			StartCoroutine (TopTurretShot(topTurretLaserRate));
			hasFiredTopLaser = true;
		}
	}

	void CheckDamage(){ //if turret health goes below the low of threshy bo

		if (turret1Health < 0) {
			Destroy (turret [0].gameObject);
			turret1Exists = false;
		}
		if (turret2Health < 0) {
			Destroy (turret [1].gameObject);
			turret2Exists = false;
		}
		if (turret3Health < 0) {
			Destroy (turret [2].gameObject);
			turret3Exists = false;
		}
		if (turret4Health < 0) {
			Destroy (turret [3].gameObject);
			turret4Exists = false;
		}

	}
}

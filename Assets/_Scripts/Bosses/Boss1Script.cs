using UnityEngine;
using System.Collections;

public class Boss1Script : MonoBehaviour {

	//Notes: needs shield for head, 3d model, items to drop

	public GameObject homingLaser;
	public GameObject waveLaser1;
	public GameObject waveLaser2;
	public GameObject waveLaser3;	
	public GameObject waveLaser4;
	public GameObject waveLaser5;

	public GameObject explosion;
	public GameObject droppedItem;

	public float enterSpeed;
	public float horzSpeed;

	public int homingLaserWaveAmount; // how many homing laser execute
	public float homingLaserFireRate; //at which rate the homing lasers fire out together
	public float homingLaserWaveRate; // between homing waves how much time is in between each shot
	public int waveLaserAmount; //
	public float waveLaserFireRate; //rate at which the spread laser fires

	bool enterPlaySpace = false; //check whether ther enemy is in the play space
	bool goLeft = false; //check whether which driection the enemy is going in
	bool hasFired = false;//check homing laser to play the coroutine once
	public bool bossHeadActive;
	bool rightArmExists = true;
	bool leftArmExists = true;

	float counter;
	//main boss health
	public int bossHealth = 300;
	public int rightArmHealth = 300;
	public int leftArmHealth = 300;

	void Start () {
		InvokeRepeating ("ShootWaveLaser", 2, waveLaserFireRate);

	}
	
	// Update is called once per frame
	void Update () {

		EnterPlaySpace (); // enters place space
		BossMovement (); //moves left and right in play space

		AttackSequence (); // shooting homing laser instiates
		CheckHomingLaser ();//check to see if homing laser is shot so it can be re shot again

		if (!rightArmExists && !leftArmExists) { //if both arms are destroyed then head is targetable
			bossHeadActive = true;
		}
			


		CheckDamage (); //check death
	}

	IEnumerator ShootHomingLaser(float laserRate){ //create homing laser

		for (int i = 0; i < homingLaserWaveAmount; i++) { //fires the laser X amount of times
			Instantiate (homingLaser, transform.position, Quaternion.identity); //creates laser
			yield return new WaitForSeconds (laserRate); //waits until laser is shot and shoots again
		}
	}

	void ShootWaveLaser(){ //shoot wave laser
			
			Transform rightArm = GameObject.Find ("rightHand").transform; //get right arm and shoot from it
			Transform leftArm = GameObject.Find ("leftHand").transform; // left arm and shoot from it

			for (int i = 0; i < waveLaserAmount; i++) {
				
				Instantiate (waveLaser1, rightArm.position, Quaternion.identity);
				Instantiate (waveLaser2, rightArm.position, Quaternion.identity);
				Instantiate (waveLaser3, rightArm.position, Quaternion.identity);
				Instantiate (waveLaser4, rightArm.position, Quaternion.identity);
				Instantiate (waveLaser5, rightArm.position, Quaternion.identity);

				Instantiate (waveLaser1, leftArm.position, Quaternion.identity);
				Instantiate (waveLaser2, leftArm.position, Quaternion.identity);
				Instantiate (waveLaser3, leftArm.position, Quaternion.identity);
				Instantiate (waveLaser4, leftArm.position, Quaternion.identity);
				Instantiate (waveLaser5, leftArm.position, Quaternion.identity);
				
			}

	}

	void EnterPlaySpace(){ //moves boss into play space

		transform.Translate (Vector3.forward * enterSpeed * Time.deltaTime);

		if (transform.position.z < 11.5f) {
			enterSpeed = 0;
			enterPlaySpace = true;
		}
	}

	void BossMovement(){ //moves boss left and right
		if (enterPlaySpace == true) {

			if (transform.position.x < -2.5f) {//change movement direction from right to left
				goLeft = true;
			} else if (transform.position.x > 2.1f) {
				goLeft = false;
			}

			if(!goLeft)
				transform.Translate (Vector3.left * horzSpeed * Time.deltaTime); //move left
			if(goLeft)
				transform.Translate (Vector3.right * horzSpeed * Time.deltaTime); // move right
		}
	}
	void AttackSequence(){

		if (enterPlaySpace == true && hasFired == false) {
			StartCoroutine (ShootHomingLaser (homingLaserFireRate));
			hasFired = true;
		}
	}
	void CheckHomingLaser(){
		if (enterPlaySpace == true) {
			counter += Time.deltaTime;
		}

		if (counter >= homingLaserWaveRate) { 
			counter = 0;
			hasFired = false;
		}
	}

	void CheckDamage(){
		if (bossHealth <= 0){
			if (tag == "Enemy"){
				Instantiate(explosion, transform.position, transform.rotation);
				//Instantiate (droppedItem, transform.position, transform.rotation);
			}
			Destroy(gameObject);
		}
		if (leftArmHealth <= 0) {
			Destroy (GameObject.Find ("leftArm"));
			Destroy (GameObject.Find ("leftHand"));
			leftArmExists = false;
			Transform leftHand = GameObject.Find ("leftHand").transform; //get position of arm and explode there
			Transform leftArm = GameObject.Find ("leftArm").transform;
			Instantiate(explosion, leftHand.position,  Quaternion.identity);
			Instantiate(explosion, leftArm.position, Quaternion.identity);
		}
		if (rightArmHealth <= 0) {
			rightArmExists = false;
			Destroy (GameObject.Find ("rightArm"));
			Destroy (GameObject.Find ("rightHand"));
			Transform rightHand = GameObject.Find ("rightHand").transform; //get position of arm and explode there
			Transform rightArm = GameObject.Find ("rightArm").transform;
			Instantiate(explosion, rightHand.position, Quaternion.identity);
			Instantiate(explosion, rightArm.position, Quaternion.identity);
		}
	}
}

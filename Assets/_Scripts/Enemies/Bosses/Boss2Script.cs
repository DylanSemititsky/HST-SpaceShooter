                                                                                                                                                                                                                                                                                                                                              using UnityEngine;
using System.Collections;

public class Boss2Script : MonoBehaviour {

	//DEFAULT MOVEMENT VARIABLES
	public float rotateSpeed; // bosses 
	public float horizontalSpeed; //bosses horizontal speed
	public float verticalSpeed; // bosses vertical speed
	public float vAplitude; // the max vertical range the boss will travel
	public float hAplitude; // the max width range the boss with travel

	float originalHspeed = 1;
	float originalVspeed = 2;
	float originalHaplitude = 3;
	float originalVaplitude = 1;
	//ROLL VARIABLES
	public float rollAttackSpeed;
	public int rollRightLength;
	public int rollDownLength;
	public int rollLeftLength;
	public int rollUpLength;
	bool isPrepareRollAttack;
	bool isRolling;
	int rollCounter;

	//EXPAND ATTACK VARIABLES
	float expandSpeed = 6;
	float expandRange = 2;
	float contract = 3;
	float expandCounter;
	bool expanding;

	public GameObject homingLaser;
	Vector3 tempPosition ;
	Vector3 originPos;
	GameObject rotatePiece;
	GameObject topArm;
	GameObject bottomArm;
	GameObject rightArm;
	GameObject leftArm;
	Vector3 topArmPos;
	//HOMING LASER VARIABLES
	public int homingLaserWaveAmount; // how many homing laser execute
	public float homingLaserFireRate; //at which rate the homing lasers fire out together
	public float homingLaserWaveRate; // between homing waves how much time is in between each shot
	bool hasFired;
	float homingLaserCounter;
	//Shaking Camera variables
	public float amplitude = 0.1f;
	public float duration = 0.5f;
	//VARIABLE TO CHECK STAGE
	bool stage0;
	bool stage1;
	bool stage2;
	bool stage3;
	bool stage4;
	//VARIABLE TO CHECK IF ARMS HAVE GROWN FOR EACH STAGE
	bool growTopArm;
	bool growBottomArm;
	bool growLeftArm;
	bool growRightArm;
	//CHECK IF ARM EXISTS
	bool topArmExists;
	bool bottomArmExists;
	bool leftArmExists;
	bool rightArmExists;
	//WAVE LASRER ATTACKS
	public int waveLaserAmount; //
	public float waveLaserFireRate; //rate at which the spread laser fires
	public GameObject waveLaser1;
	public GameObject waveLaser2;
	public GameObject waveLaser3;	
	public GameObject waveLaser4;
	public GameObject waveLaser5;
	bool fireWaveOnce;
	float waveLaserCounter;
	bool waveAttack;
	//VARIABLES TO KEEP TRACK OF HEALTH AND DAMAGE
	public int bossHealth = 400;
	bool shielded;
	private bool damaged;
	int maxBossHealth;
	GameObject shield;
	Color originalColor;// variable for shield color

	bool expandingMode;
	float expandModeCounter;
	bool fadeIntoPlaySpace;
	bool faded;

	//EXPLOSION WHEN DEAD
	public GameObject explosion;
	//AUDIO
	public AudioSource rollCharge;



	//GameController Access
	GameController gameController;

	void Start () {
		AudioSource[] soundClips = GetComponents<AudioSource> ();
		rollCharge = soundClips [0];

		rotatePiece = GameObject.Find ("RotatePiece"); //grab rotate bone object
		topArm = GameObject.Find ("TopArm"); //grab boss arms -->>
		bottomArm = GameObject.Find ("BottomArm");
		rightArm = GameObject.Find ("RightArm");
		leftArm = GameObject.Find ("LeftArm");
		shield = GameObject.Find ("BossShield");

		GameObject gameControllerObject = GameObject.Find ("GameController");	
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		//tempPosition = transform.position;
		//originPos = transform.position;
		//topArmPos = topArm.transform.position;

		Renderer rend = GetComponent<Renderer> ();
		originalColor = rend.material.color;

		maxBossHealth = bossHealth;

		//start stage
		stage0 = true;
		shield.GetComponent<Renderer> ().enabled = false;
		topArm.transform.localPosition = new Vector3 (0, 0, 0);
		bottomArm.transform.localPosition = new Vector3 (0, 0, 0);
		leftArm.transform.localPosition = new Vector3 (0, 0, 0);
		rightArm.transform.localPosition = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!faded) {
			FadeIntoPlaySpace ();
		}
		if (fadeIntoPlaySpace) {


			DefaultBossMovement (); //regular movement
			CheckToShootHomingLaser ();//check to see of can laser attack
			//AttackSequenceLaser();
			if (expanding) { //expand attack
				ExpandArms ();
			}
			StageControl ();

			if (damaged) {
				StartCoroutine (DamageFlash ());
			}
			HealthUpdate ();
			//EXPAND ATTACK MODE
			ExpandAttack ();
	
			
			/*if (Input.GetKeyDown (KeyCode.Space)) {
			//horizontalSpeed *= 2;
			//verticalSpeed *= 2;
			isPrepareRollAttack = !isPrepareRollAttack;

		}*/
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				expanding = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad0)) {
				stage0 = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad1)) {
				stage0 = false;
				stage1 = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad2)) {
				stage2 = true;
				stage1 = false;
			}
			if (Input.GetKeyDown (KeyCode.Keypad3)) {
				stage3 = true;
				stage2 = false;
			}
			if (Input.GetKeyDown (KeyCode.Keypad4)) {
				stage4 = true;
				stage3 = false;
			}
			if (Input.GetKeyDown (KeyCode.A)) {
			
			}

		}
	}

	void DefaultBossMovement(){

		//BOSS ARM ROTATION
		rotatePiece.transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime); //rotates Boss arms

		 // turn these functions off if roll attack is happening
		if (!isRolling) {
			//BOSS MOVEMENT
			//tempPosition.x += horizontalSpeed;
			tempPosition.x = Mathf.Sin (Time.realtimeSinceStartup * horizontalSpeed) * hAplitude;
			tempPosition.z = Mathf.Sin (Time.realtimeSinceStartup * verticalSpeed) * vAplitude;
			tempPosition.z += originPos.z;
			transform.position = tempPosition;
			AttackSequenceWaveLaser();
		}
		
		if (isPrepareRollAttack) {//becomes stationary for roll attack
				hAplitude -= Time.deltaTime * 2;
			if (hAplitude <= 0) {
				hAplitude = 0;
				isPrepareRollAttack = false; //turn prepare roll attack off
				StartCoroutine(RollAttack());
			}

		}

	}

	IEnumerator RollAttack(){ //roll attack sequence
		StartCoroutine(ActivateShield ()); //activate shield before roll hgfds
		if (!stage0) {
			rollCharge.Play ();
			for (int i = 0; vAplitude >= 0; i++) { // speeds aplitude back up
				vAplitude -= Time.deltaTime * 2;
				yield return new WaitForSeconds (0.001f);
			}
			vAplitude = 0;
			for (int i = 0; rotateSpeed < 700; i++) {
				rotateSpeed += 5;
				yield return new WaitForSeconds (0.001f);
			}
		}

			yield return new WaitForSeconds (0.5f);
		
		isRolling = true;

		//if(rollRight = true){
		for(int i = 0; transform.position.x < 3.45f; i++){ //rolls right
			transform.Translate (Vector3.right * rollAttackSpeed * Time.deltaTime);
			yield return new WaitForSeconds (0.0001f);
		}

		CameraShake.Instance.Shake (amplitude, duration);
		yield return new WaitForSeconds (0.25f); //rolls down

		for (int i = 0; transform.position.z > -1.25f; i++) {
			transform.Translate (Vector3.forward * -rollAttackSpeed * Time.deltaTime);
			yield return new WaitForSeconds (0.0001f);
		}
		CameraShake.Instance.Shake (amplitude, duration);
		yield return new WaitForSeconds (0.25f); //rolls left

		for (int i = 0; transform.position.x > -3.25f; i++) {
			transform.Translate (Vector3.right * -rollAttackSpeed * Time.deltaTime);
			yield return new WaitForSeconds (0.0001f);
		}
		CameraShake.Instance.Shake (amplitude, duration);
		yield return new WaitForSeconds (0.25f); //rolls up

		for (int i = 0; transform.position.z < 9.5f; i++) {
			transform.Translate (Vector3.forward * rollAttackSpeed * Time.deltaTime);
			yield return new WaitForSeconds (0.0001f);
		}
		CameraShake.Instance.Shake (amplitude, duration);
		Vector3 temp = transform.position; // sets the z position to an absolute one
		temp.z = 9.5f;
		transform.position = temp;

		yield return new WaitForSeconds (0.25f); 

		for(int i = 0; transform.position.x < 0; i++){ //rolls right to origin
			transform.Translate (Vector3.right * rollAttackSpeed * Time.deltaTime);
			yield return new WaitForSeconds (0.0001f);
		}
		temp.x = 0;
		transform.position = temp;
		//transform.position = tempPosition; //set position back to normal origin
		hAplitude = 0;// setlle the boss
		vAplitude = 0;// setlle the boss
		isRolling = false; //turn movement features back on and rolling mode off
		DeactivateShield();

		for (int i = 0; vAplitude <= 1; i++) { // speeds aplitude back up
			vAplitude += Time.deltaTime * 2;
			yield return new WaitForSeconds (0.001f);
		}
		vAplitude = 1;
		for(int i = 0; rotateSpeed > 75; i ++) { // slows rotation down
			rotateSpeed -= 5;
			yield return new WaitForSeconds (0.001f);
		}

		for (int i = 0; hAplitude <= 3; i++) { //speeds aplitude back up
			hAplitude += Time.deltaTime * 2;
			yield return new WaitForSeconds (0.001f);
		}
		hAplitude = 3;
		rollCounter += 1;
		StartCoroutine (DeactivateShield ());


	}
	void ExpandArms(){

			expandCounter += Time.deltaTime;

			if ( expanding && expandCounter < expandRange) { //expanding arms outward
				topArm.transform.Translate (Vector3.forward * expandSpeed * Time.deltaTime);
				if (bottomArmExists)
					bottomArm.transform.Translate (Vector3.forward * -expandSpeed * Time.deltaTime);
				if (leftArmExists)
					leftArm.transform.Translate (Vector3.right * -expandSpeed * Time.deltaTime);
				if (rightArmExists)
					rightArm.transform.Translate (Vector3.right * expandSpeed * Time.deltaTime);
			
				if (stage4) {
					for (int i = 0; rotateSpeed < 125; i++) {
						rotateSpeed += 5;

					}
				} else if (stage3) {
					for (int i = 0; rotateSpeed < 175; i++) {
						rotateSpeed += 5;

					}
				} else if (stage2) {
					for (int i = 0; rotateSpeed < 225; i++) {
						rotateSpeed += 5;

					}
				} else if (stage1) {
					for (int i = 0; rotateSpeed < 300; i++) {
						rotateSpeed += 5;

					}
				}
			}
			if (expandCounter > contract && expandCounter < contract + 2) { //contracting arms inward
				topArm.transform.Translate (Vector3.forward * -expandSpeed * Time.deltaTime);
				if (bottomArmExists)
					bottomArm.transform.Translate (Vector3.forward * expandSpeed * Time.deltaTime);
				if (leftArmExists)
					leftArm.transform.Translate (Vector3.right * expandSpeed * Time.deltaTime);
				if (rightArmExists)
					rightArm.transform.Translate (Vector3.right * -expandSpeed * Time.deltaTime);
			
				for (int i = 0; rotateSpeed > 75; i++) { // slows rotation down
					rotateSpeed -= 5;
				}
			}
			if (expandCounter >= contract + 2) { //once the expanding and contracting is done, reset action
				expandCounter = 0;
				expanding = false;
			}

	}
	IEnumerator ShootHomingLaser(float laserRate){ //create homing laser

		for (int i = 0; i < homingLaserWaveAmount; i++) { //fires the laser X amount of times
			Instantiate (homingLaser, transform.position, Quaternion.identity); //creates laser
			yield return new WaitForSeconds (laserRate); //waits until laser is shot and shoots again
		}
	}
	void AttackSequenceLaser(){

		if (hasFired == false) {
			StartCoroutine (ShootHomingLaser (homingLaserFireRate));
			hasFired = true;
		}
			
	}
	void AttackSequenceWaveLaser(){
		
		if (fireWaveOnce == false){
			StartCoroutine (ShootWaveLaser (waveLaserFireRate));
			fireWaveOnce = true;
		}
	}

	void CheckToShootHomingLaser(){//CHECK WHAT ATTACK IS HAPPEN AND SHOOT HOMING LASER ACCORINGLY
		homingLaserCounter += Time.deltaTime;
		waveLaserCounter += Time.deltaTime;

		if (homingLaserCounter >= homingLaserWaveRate) { 
			homingLaserCounter = 0;
			hasFired = false;
		}
		if (waveLaserCounter >= waveLaserFireRate) {
			waveLaserCounter = 0;
			fireWaveOnce = false;
		}
		if (isRolling) { //if rolling then do this attack with these ADD STAGE LEVELS*****************************
			if (stage0) {
				homingLaserWaveAmount = 15;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 2f;
			} else if (stage1) {
				homingLaserWaveAmount = 12;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 1.5f;
				rollAttackSpeed = 13;
			} else if (stage2) {
				homingLaserWaveAmount = 10;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 1.25f;
				rollAttackSpeed = 16;
			} else if (stage3) {
				homingLaserWaveAmount = 10;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 1f;
				rollAttackSpeed = 18;
			} else if (stage4) {
				homingLaserWaveAmount = 9;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 0.75f;
				rollAttackSpeed = 25;
			}
			AttackSequenceLaser ();

		}
		if (expanding) { //change
			if (stage1) {
				homingLaserWaveAmount = 10;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 1.5f;
				AttackSequenceLaser ();
			} else if (stage2) {
				homingLaserWaveAmount = 8;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 1.25f;
				AttackSequenceLaser ();
			} else if (stage3) {
				homingLaserWaveAmount = 5;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 0.8f;
				AttackSequenceLaser ();
			} else if (stage4) {
				homingLaserWaveAmount = 7;
				homingLaserFireRate = 0.075f;
				homingLaserWaveRate = 0.85f;
				AttackSequenceLaser ();
			}
		}
	}
	void StageControl(){
		

		if (stage0) { //sets all arms at level one
			topArm.transform.localPosition = new Vector3 (0, 0, 0);
			bottomArm.transform.localPosition = new Vector3 (0, 0, 0);
			leftArm.transform.localPosition = new Vector3 (0, 0, 0);
			rightArm.transform.localPosition = new Vector3 (0, 0, 0);
			waveLaserFireRate = 3;
			//expandingMode = false; //cannot expand arms
		}
		if (stage1) {
			if (!growTopArm && !expanding && rollCounter == 1)//if arm hasnt grown yet do this.
			GrowTopArm ();
			waveLaserFireRate = 2.75f;
			//expandingMode = true;//expand arms
		}
		if (stage2) {
			if(!growBottomArm && !expanding)
			GrowBottomArm ();
			waveLaserFireRate = 1.6f;
		}
		if (stage3) {
			if (!growLeftArm && !expanding)
				GrowLeftArm ();
			waveLaserFireRate = 1f;
		}
		if (stage4) {
			
			if (!growRightArm && !expanding)
				GrowRightArm ();
			waveLaserFireRate = 0.5f;
		}
	}
	void GrowTopArm(){
		
		float GrowSpeed = 1f;

		topArm.transform.Translate (Vector3.forward * GrowSpeed * Time.deltaTime);
		if (topArm.transform.localPosition.z > 0.8f) {
			growTopArm = true;
			topArmExists = true;
			topArm.transform.localPosition = new Vector3 (0, 0, 0.8f);
		}
	}
	void GrowBottomArm (){
		
		float GrowSpeed = 1f;

		bottomArm.transform.Translate (Vector3.forward * -GrowSpeed * Time.deltaTime);
		if (bottomArm.transform.localPosition.z < -0.8f) {
			growBottomArm = true;
			bottomArmExists = true;
			bottomArm.transform.localPosition = new Vector3 (0, 0, -0.8f);
		}
	}
	void GrowLeftArm(){
		float GrowSpeed = 1f;

		leftArm.transform.Translate (Vector3.right * -GrowSpeed * Time.deltaTime);
		if (leftArm.transform.localPosition.x < -0.8f) {
			growLeftArm = true;
			leftArmExists = true;
			leftArm.transform.localPosition = new Vector3 (-0.8f, 0, 0);
		}
	}
	void GrowRightArm(){
		float GrowSpeed = 1f;

		rightArm.transform.Translate (Vector3.right * GrowSpeed * Time.deltaTime);
		if (rightArm.transform.localPosition.x > 0.8f) {
			growRightArm = true;
			rightArmExists = true;
			rightArm.transform.localPosition = new Vector3 (0.8f, 0, 0);
		}
	}

	IEnumerator ShootWaveLaser(float laserRate){ //create homing laser
		for (int i = 0; i < waveLaserAmount; i++) {

			Instantiate (waveLaser1, transform.position, Quaternion.identity);
			Instantiate (waveLaser2, transform.position, Quaternion.identity);
			Instantiate (waveLaser3, transform.position, Quaternion.identity);
			Instantiate (waveLaser4, transform.position, Quaternion.identity);
			Instantiate (waveLaser5, transform.position, Quaternion.identity);

			yield return new WaitForSeconds (laserRate); //waits until laser is shot and shoots again

		}
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}
		if (!shielded) {
			if (other.tag == "pLaser 1") {
				damaged = true;
				bossHealth -= 10;
			}
			if (other.tag == "pLaser 2") {
				damaged = true;
				bossHealth -= 20;
			}
			if (other.tag == "pLaser 3") {
				damaged = true;
				bossHealth -= 30;
			}
			if (other.tag == "pLaser 4") {
				damaged = true;
				bossHealth -= 40;
			}
		}
	
	}
	public IEnumerator DamageFlash(){ //taking damage

		Renderer rend = GetComponent<Renderer> ();
		for(int i = 1; i <= 4; i++){
			rend.material.SetColor ("_Color", Color.red);
			yield return new WaitForSeconds (0.1f);
			rend.material.SetColor ("_Color", Color.white);
			yield return new WaitForSeconds (0.1f);
			damaged = false;
			rend.material.color = originalColor;
		}
	}
	void HealthUpdate(){
		if (bossHealth < 0 && stage0) {
			bossHealth = maxBossHealth;
			stage0 = false;
			stage1 = true;
			isPrepareRollAttack = true;
		} else if (bossHealth < 0 && stage1) {
			bossHealth = maxBossHealth;
			stage1 = false;
			stage2 = true;
			isPrepareRollAttack = true;
		} else if (bossHealth < 0 && stage2) {
			bossHealth = maxBossHealth;
			stage2 = false;
			stage3 = true;
			isPrepareRollAttack = true;
		} else if (bossHealth < 0 && stage3) {
			bossHealth = maxBossHealth;        
			stage3 = false;
			stage4 = true;
			isPrepareRollAttack = true;
		}else if (bossHealth < 0 && stage4){
			Instantiate(explosion, topArm.transform.position,  Quaternion.identity);
			Instantiate(explosion, bottomArm.transform.position,  Quaternion.identity);
			Instantiate(explosion, rightArm.transform.position,  Quaternion.identity);
			Instantiate(explosion, leftArm.transform.position,  Quaternion.identity);
			Instantiate(explosion,transform.position,  Quaternion.identity);

			gameController.LevelComplete (); //Execute Level complete function in GameController
			Debug.Log ("Boss is dead");

			Destroy (gameObject);
		}
	}
	//**********************************************ADD SOUND HERE
	IEnumerator ActivateShield(){ //create shield
		bossHealth = maxBossHealth;
		shield.GetComponent<Renderer> ().enabled = true;
		shielded = true;
		Color shieldColor = shield.GetComponent<Renderer> ().material.color;

		for (int i = 0; shieldColor.a < 0.4f; i++) { //loop to increase alpha
			shieldColor.a += 0.01f;
			shield.GetComponent<Renderer> ().material.color = shieldColor;

			yield return new WaitForSeconds (0.01f);
		}
	}
	IEnumerator DeactivateShield(){ //fade shield away
		Color shieldColor = shield.GetComponent<Renderer> ().material.color;

		for (int i = 0; shieldColor.a > 0.01f; i++) { //loop to increase alpha
			shieldColor.a -= 0.01f;
			shield.GetComponent<Renderer> ().material.color = shieldColor;

			yield return new WaitForSeconds (0.01f);
		}
		shield.GetComponent<Renderer> ().enabled = false;
		shielded = false;
	}
	void ExpandAttack(){
		if (!stage0) {//if we are on a higher stage
			if (bossHealth <= 110 && bossHealth >= 50) {
				expandModeCounter += Time.deltaTime;

				if (expandModeCounter > 1f) {
					expandModeCounter = 0;
					expanding = true;
					ExpandArms ();
				}
			}
		}
	}
	void FadeIntoPlaySpace(){
		

		if (!fadeIntoPlaySpace) {
			
			StartCoroutine(ActivateShield ());
			transform.Translate (Vector3.forward * -2f * Time.deltaTime);
			ActivateShield ();
			horizontalSpeed = 0;
			verticalSpeed = 0;
			hAplitude = 0;
			vAplitude = 0;
		}

		if (transform.position.z < 9.6f) {
			fadeIntoPlaySpace = true;
			Vector3 temp = transform.position; // sets the z position to an absolute one
			temp.z = 9.5f;
			transform.position = temp;
			tempPosition = transform.position;
			originPos = transform.position;
			topArmPos = topArm.transform.position;


		}
		if (fadeIntoPlaySpace) {
			if (!faded) {
				StartCoroutine (DeactivateShield ());

				horizontalSpeed = Mathf.Lerp (horizontalSpeed, originalHspeed, 0.75f * Time.time*Time.deltaTime );
				verticalSpeed = Mathf.Lerp (verticalSpeed, originalVspeed,1 * Time.time *Time.deltaTime);
				hAplitude = Mathf.Lerp (hAplitude, originalHaplitude, 1 * Time.time*Time.deltaTime );
				vAplitude = Mathf.Lerp (vAplitude, originalVaplitude, 1 * Time.time *Time.deltaTime );

				if (vAplitude >= 0.999f) {
					faded = true;
					horizontalSpeed = originalHspeed;
					verticalSpeed = originalVspeed;
					hAplitude = originalHaplitude;
					vAplitude = originalVaplitude;

				}
			}
		}
	}
}

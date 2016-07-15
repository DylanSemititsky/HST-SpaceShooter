// ---------------------------------------------------------------------------------------------------
// PLAYER ATTACK CONTROLLER
// Controls player: Attack levels and Fire Rate
// Controls attack sound effects
// Collects variables from GameState manager everytime a scene is loaded 
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[System.Serializable]
public class PrimaryAttack{ 	//Collapsible menu to access Primary Attack settings.
	public int setPrimaryAttackLevel; //Set Primary Attack level.
	public GameObject primaryAttackLv1, primaryAttackLv2, primaryAttackLv3, primaryAttackLv4; //Place Art for each level here (in Inspector).
	public Transform primaryShotSpawn; //Where Primary Attacks will spawn.
}

[System.Serializable]
public class MultiAttack{  	//Collapsible menu to access Multi Attack settings.
	public int setMultiAttackLevel; //Set Multi Attack level.
	public GameObject multiAttackLv1, multiAttackLv2, multiAttackLv2R, multiAttackLv2L, multiAttackLv3, multiAttackLv3R, multiAttackLv3L; //Place Art for each level here (in Inspector).
	public Transform multiShotSpawnLv1R, multiShotSpawnLv1L, multiShotSpawnLv2R,  multiShotSpawnLv2L;
}

[System.Serializable]
public class FusionAttack{  			//Collapsible menu to access Fusion Attack settings.
	public int setFusionAttackLevel; //Set Fusion Attack level.
	public GameObject fusionAttackLv1, fusionAttackLv2, fusionAttackLv3, fusionAttackLv4; //Place Art for each level here (in Inspector).
	public Transform fusionShotSpawn, fusionShotSpawnR, fusionShotSpawnL;
	public float maxFusion = 100, fusion = 100;
	public Image fusionBar;
	public int fusionAttackDamage;
}

[System.Serializable]
public class BombAttack{  		//Collapsible menu to access Bomb Attack settings.
	public int setBombLevel;
	public GameObject bombPurple, bombRed, bombOrange, bombYellow; 	//Place Art here
	public Transform shotSpawn;
	public float maxBomb = 100, bomb = 100;
	public Image bombBar;
}

public class PlayerAttack : MonoBehaviour {

	public PrimaryAttack primaryAttack;
	public MultiAttack multiAttack;
	public FusionAttack fusionAttack;
	public BombAttack bombAttack;
	public float fireRate, fusionFireRate;
	private float primaryAttackNextFire, multiAttackNextFire, fusionAttackNextFire;

	private float nextBombRecharge, bombFillAmount, bombRechargeDelay = 0.1f;
	private float nextFusionRecharge, fusionFillAmount, fusionRechargeDelay = 0.1f;
	private bool fusioning;

	private bool mouseDown;
	public bool disableLasers, disableFusion;
	public GameObject laserSound;
	public GameObject fusionSound;

	public GameObject popupAttackSpeed;
	public GameObject popupBombCharge;

	//private AudioSource audioSource;
	public AudioSource[] audioClips = null;
	public GameObject attackSpeedSound;

	//Game Manager
	GameState gameState;


	// ---------------------------------------------------------------------------------------------------
	// START
	// ---------------------------------------------------------------------------------------------------
	void Start() {

		//Find GameState Object to access it's script
		GameObject gameStateObject = GameObject.Find ("GameState");	
		if (gameStateObject != null) {
			gameState = gameStateObject.GetComponent<GameState> ();
		}

		//Set Upgrade values for new scene load (from GameState manager)
		primaryAttack.setPrimaryAttackLevel = gameState.getPrimaryAttackLevel ();
		multiAttack.setMultiAttackLevel = gameState.getMultiAttackLevel ();
		fusionAttack.setFusionAttackLevel = gameState.getFusionAttackLevel ();
		bombAttack.setBombLevel = gameState.getBombAttackLevel ();
		fireRate = gameState.getFireRate();

		//Store laser attack upgrade levels for temporary disabling/enabling during fusion
		fusioning = false;
		disableFusion = false;
	}

	// ---------------------------------------------------------------------------------------------------
	// UPDATE
	// ---------------------------------------------------------------------------------------------------
	void Update () {

		PrimaryAttack();

		MultiAttack();

		FusionAttack();

		BombAttack();

		UpgradeKeys();
	}

	// ---------------------------------------------------------------------------------------------------
	// PowerUp Control Gather
	// ---------------------------------------------------------------------------------------------------
		void OnTriggerEnter(Collider other){
		if (other.tag == "powerUp_fireRate"){
			fireRate -= 0.01f;
			if(fireRate <= 0.1f){
				fireRate = 0.1f;
			}
			Instantiate (popupAttackSpeed, transform.position, transform.rotation);
			Instantiate (attackSpeedSound, transform.position, transform.rotation);
			Destroy(other.gameObject);
			//audioClips[1].Play();
		}
		if (other.tag == "powerUp_watermelon"){
			multiAttack.setMultiAttackLevel += 1;
			if(multiAttack.setMultiAttackLevel >= 3){
				multiAttack.setMultiAttackLevel = 3;
			}
			Destroy(other.gameObject);
			//audioClips[1].Play();
		}
		if (other.tag == "powerUp_bomb"){
			bombAttack.bomb += 25;
			Instantiate (popupBombCharge, transform.position, transform.rotation);
			Destroy(other.gameObject);
			//audioClips[1].Play();
		}
	}


	// ---------------------------------------------------------------------------------------------------
	// ATTACK FUNCTIONS
	// ---------------------------------------------------------------------------------------------------
	// ---------------------------------------------------------------------------------------------------
	// ---------------------------------------------------------------------------------------------------
	//PRIMARY ATTACK. Auto-fired. Level based on setPrimaryAttackLevel.
	// ---------------------------------------------------------------------------------------------------
	void PrimaryAttack(){

		if (primaryAttack.setPrimaryAttackLevel == 1 && fusioning == false){	//Level 1
			if (Time.time > primaryAttackNextFire) {
				primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv1, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
				Instantiate(laserSound, transform.position, transform.rotation);
			}
		}
		else if (primaryAttack.setPrimaryAttackLevel == 2 && fusioning == false){	//Level 2
			if (Time.time > primaryAttackNextFire) {
				primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv2, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
				Instantiate(laserSound, transform.position, transform.rotation);
			}
		}
		else if (primaryAttack.setPrimaryAttackLevel == 3 && fusioning == false){	//Level 3
			if (Time.time > primaryAttackNextFire) {
            	primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv3, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
				Instantiate(laserSound, transform.position, transform.rotation);
			}
		}
		else if (primaryAttack.setPrimaryAttackLevel == 4 && fusioning == false){ 	//Level 4
			if (Time.time > primaryAttackNextFire) {
            	primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv4, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
				Instantiate(laserSound, transform.position, transform.rotation);
			}
		}
	}

	// ---------------------------------------------------------------------------------------------------
	//MULTI SHOT. Auto-fired. Level based on setPrimaryAttackLevel.
	// ---------------------------------------------------------------------------------------------------
	void MultiAttack(){
		if (multiAttack.setMultiAttackLevel == 1 && fusioning == false){	//Level 1
			if (Time.time > multiAttackNextFire) {
            	multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv1, multiAttack.multiShotSpawnLv1L.position, multiAttack.multiShotSpawnLv1L.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv1, multiAttack.multiShotSpawnLv1R.position, multiAttack.multiShotSpawnLv1R.rotation);
	            //audioSource.Play();
			}
		}
		else if (multiAttack.setMultiAttackLevel == 2 && fusioning == false){	//Level 2
			if (Time.time > multiAttackNextFire) {
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv2, multiAttack.multiShotSpawnLv1L.position, multiAttack.multiShotSpawnLv1L.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv2, multiAttack.multiShotSpawnLv1R.position, multiAttack.multiShotSpawnLv1R.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv2R, multiAttack.multiShotSpawnLv2R.position, multiAttack.multiShotSpawnLv1L.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv2L, multiAttack.multiShotSpawnLv2L.position, multiAttack.multiShotSpawnLv1R.rotation);
	            //audioSource.Play();
			}
		}
		else if (multiAttack.setMultiAttackLevel == 3 && fusioning == false){	//Level 3
			if (Time.time > multiAttackNextFire) {
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv3, multiAttack.multiShotSpawnLv1L.position, multiAttack.multiShotSpawnLv1L.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv3, multiAttack.multiShotSpawnLv1R.position, multiAttack.multiShotSpawnLv1R.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv3R, multiAttack.multiShotSpawnLv2R.position, multiAttack.multiShotSpawnLv1L.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv3L, multiAttack.multiShotSpawnLv2L.position, multiAttack.multiShotSpawnLv1R.rotation);
	            //audioSource.Play();
			}
		}
	}

	// ---------------------------------------------------------------------------------------------------
	//fusion ATTACK. Fire on LMB press
	// ---------------------------------------------------------------------------------------------------

	void FusionAttack ()
	{

		//Determine Fusion attack level
		if (primaryAttack.setPrimaryAttackLevel >= multiAttack.setMultiAttackLevel) {
			fusionAttack.fusionAttackDamage = primaryAttack.setPrimaryAttackLevel;
		} 
		else if (multiAttack.setMultiAttackLevel >= primaryAttack.setPrimaryAttackLevel) {
			fusionAttack.fusionAttackDamage = multiAttack.setMultiAttackLevel;
		}
		//fusionAttack.fusionAttackDamage = primaryAttack.setPrimaryAttackLevel + multiAttack.setMultiAttackLevel;

		fusionFireRate = fireRate * 0.2f;
		if(fusionFireRate <= 0.05f){
			fusionFireRate = 0.05f;
		}


		//Level 1
		if (fusionAttack.setFusionAttackLevel == 1 && disableFusion == false) {
			
			if (Input.GetMouseButton (0) && fusionAttack.fusion > 0) {

				//Disable auto laser cannons during Fusion Attack
				fusioning = true;

				//Set mouseDown to true to prevent recharging while using Fusion
				mouseDown = true;
	

				if (Time.time > fusionAttackNextFire) {
					fusionAttackNextFire = Time.time + fusionFireRate * 2;
					if (fusionAttack.fusionAttackDamage == 1) {
						Instantiate (fusionAttack.fusionAttackLv1, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv1, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 2) {
						Instantiate (fusionAttack.fusionAttackLv2, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv2, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 3) {
						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 4) {
						Instantiate (fusionAttack.fusionAttackLv4, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv4, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					/*else if (fusionAttack.fusionAttackDamage == 5) {
						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawn.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 6) {
						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawn.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 7) {
						Instantiate (fusionAttack.fusionAttackLv4, fusionAttack.fusionShotSpawn.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}*/
				}

				 
			} else {
				//Re-enable auto laser cannons
				fusioning = false;

				//Set mouseDown to false to allow recharge (below)
				mouseDown = false;
			}
		}

		//Level 2
		/*else if (fusionAttack.setFusionAttackLevel == 2 && disableFusion == false) {

			if (Input.GetMouseButton (0) && fusionAttack.fusion > 0) {

				//Disable auto laser cannons during Fusion Attack
				fusioning = true;

				//Set mouseDown to true to prevent recharging while using Fusion
				mouseDown = true;


				if (Time.time > fusionAttackNextFire) {
					fusionAttackNextFire = Time.time + fusionFireRate * 2;
					if (fusionAttack.fusionAttackDamage == 1) {
						Instantiate (fusionAttack.fusionAttackLv1, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv1, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 2) {
						Instantiate (fusionAttack.fusionAttackLv2, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv2, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 3) {
						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 4) {
						Instantiate (fusionAttack.fusionAttackLv4, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv4, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					/*else if (fusionAttack.fusionAttackDamage == 5) {
						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 6) {
						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv3, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
					else if (fusionAttack.fusionAttackDamage == 7) {
						Instantiate (fusionAttack.fusionAttackLv4, fusionAttack.fusionShotSpawnL.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);

						Instantiate (fusionAttack.fusionAttackLv4, fusionAttack.fusionShotSpawnR.position, fusionAttack.fusionShotSpawn.rotation);
						Instantiate(fusionSound, transform.position, transform.rotation);
						fusionAttack.fusion -= 5;
					}
				}
				//fusionAttack.fusion -= Time.deltaTime * 20;


			} else {
				//Re-enable auto laser cannons
				fusioning = false;

				//Set mouseDown to false to allow recharge (below)
				mouseDown = false;
			}
		}*/


		//Fusion to stop recharging at maxFusion
		if (fusionAttack.fusion >= fusionAttack.maxFusion){
			fusionAttack.fusion = fusionAttack.maxFusion;
		}

		//Show fusion meter fill amount
		fusionFillAmount = (fusionAttack.fusion / fusionAttack.maxFusion);
		if (fusionFillAmount != fusionAttack.fusionBar.fillAmount) {
			fusionAttack.fusionBar.fillAmount = fusionFillAmount;
		}

		//Allow fusion meter to recharge when mouse isn't pressed down
		if (mouseDown == false){
			if (Time.time > nextFusionRecharge){
				nextFusionRecharge = Time.time + fusionRechargeDelay;
				if (fusionAttack.setFusionAttackLevel == 1){
					fusionAttack.fusion += 0.5f;
				}
				else if (fusionAttack.setFusionAttackLevel == 2){
					fusionAttack.fusion += 0.5f;
				}
			}
		}
	}



	// ---------------------------------------------------------------------------------------------------
	//BOMB ATTACK. Fire on RMB press
	// ---------------------------------------------------------------------------------------------------
	void BombAttack(){
		if (Input.GetMouseButtonDown (1) && bombAttack.bomb >= bombAttack.maxBomb) {
			if(bombAttack.setBombLevel == 1){
			Instantiate(bombAttack.bombYellow, bombAttack.shotSpawn.position, bombAttack.shotSpawn.rotation);
			bombAttack.bomb = 0;
			}
			else if(bombAttack.setBombLevel == 2){
			Instantiate(bombAttack.bombOrange, bombAttack.shotSpawn.position, bombAttack.shotSpawn.rotation);
			bombAttack.bomb = 0;
			}
			else if(bombAttack.setBombLevel == 3){
			Instantiate(bombAttack.bombRed, bombAttack.shotSpawn.position, bombAttack.shotSpawn.rotation);
			bombAttack.bomb = 0;
			}
			else if(bombAttack.setBombLevel == 4){
			Instantiate(bombAttack.bombPurple, bombAttack.shotSpawn.position, bombAttack.shotSpawn.rotation);
			bombAttack.bomb = 0;
			}
		}

		//Bomb to stop recharging at maxFusion
		if (bombAttack.bomb >= bombAttack.maxBomb){
			bombAttack.bomb = bombAttack.maxBomb;
		}

		//Bomb fusion meter fill amount
		bombFillAmount = (bombAttack.bomb / bombAttack.maxBomb);
		if (bombFillAmount != bombAttack.bombBar.fillAmount) {
			bombAttack.bombBar.fillAmount = bombFillAmount;
		}

		//Allow Bomb to recharge after use
		if (Time.time > nextBombRecharge){
			nextBombRecharge = Time.time + bombRechargeDelay;
			bombAttack.bomb += 0.5f;
		}
	}

	// ---------------------------------------------------------------------------------------------------
	// Hotkeys to upgrade weapons during game (for testing purposes)
	// ---------------------------------------------------------------------------------------------------
	void UpgradeKeys(){

		//Temporary quick hotkeys to test upgrades
		if (Input.GetKeyDown("]") && primaryAttack.setPrimaryAttackLevel < 4){	//+1 Primary Attack Level
			primaryAttack.setPrimaryAttackLevel++;
		}
		if (Input.GetKeyDown("[") && primaryAttack.setPrimaryAttackLevel > 0){	//-1 Primary Attack Level
			primaryAttack.setPrimaryAttackLevel--;
		}
		if (Input.GetKeyDown("'") && multiAttack.setMultiAttackLevel < 3){	//+1 Multi Attack Level
			multiAttack.setMultiAttackLevel++;
		}
		if (Input.GetKeyDown(";") && multiAttack.setMultiAttackLevel > 0){	//-1 Multi Attack Level
			multiAttack.setMultiAttackLevel--;
		}
		if (Input.GetKeyDown(".") && fireRate > .1){	//+10% Fire Rate
			fireRate -= 0.04f;
			if(fireRate <= 0.1f){
				fireRate = 0.1f;
			}
		}
		if (Input.GetKeyDown(",") && fireRate < 0.5f){ 	//Reset Fire Rate to 0.5/second
			fireRate = 0.5f;
		}
	}


	// ---------------------------------------------------------------------------------------------------
	// Return values for storing in GameState Manager
	// ---------------------------------------------------------------------------------------------------
	public int getPrimaryAttack(){
		return primaryAttack.setPrimaryAttackLevel;
	}

	public int getMultiAttack(){
		return multiAttack.setMultiAttackLevel;
	}

	public float getFireRate(){
		return fireRate;
	}

	public int getBombAttack(){
		return bombAttack.setBombLevel;
	}

	public int getFusionAttack(){
		return fusionAttack.setFusionAttackLevel;
	}
}

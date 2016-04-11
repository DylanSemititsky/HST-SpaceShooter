﻿// ---------------------------------------------------------------------------------------------------
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
public class BurstAttack{  			//Collapsible menu to access Burst Attack settings.
	public int setBurstAttackLevel; //Set Multi Attack level.
	public GameObject burstAttackLv1, burstAttackLv2, burstAttackLv3, burstAttackLv4; //Place Art for each level here (in Inspector).
	public Transform burstShotSpawn, burstShotSpawnR, burstShotSpawnL;
}

[System.Serializable]
public class BombAttack{  		//Collapsible menu to access Bomb Attack settings.
	public GameObject bomb; 	//Place Art here
	public Transform shotSpawn;
}

public class PlayerAttack : MonoBehaviour {

	public PrimaryAttack primaryAttack;
	public MultiAttack multiAttack;
	public BurstAttack burstAttack;
	public BombAttack bombAttack;
	public GameObject specialAttack;
	public float fireRate;
	public float burstFireRate;
	private float primaryAttackNextFire;
	private float multiAttackNextFire;
	private float burstAttackNextFire;
	public int burstAttackDamage;

	public float maxBurst;
	public float burst;
	public float burstRechargeDelay;
	private float nextBurstRecharge;
	private float burstFillAmount;
	private bool bursting;
	public int bombsAvailable;
	public Image burstBar;
	public bool mouseDown;
	public bool disableLasers;
	private int primaryTemp;
	private int multiTemp;
	public GameObject laserSound;


	//private AudioSource audioSource;
	//public AudioSource[] audioClips = null;

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
		/*primaryAttack.setPrimaryAttackLevel = gameState.getPrimaryAttackLevel();
		multiAttack.setMultiAttackLevel = gameState.getMultiAttackLevel();
		fireRate = gameState.getFireRate();*/

		//Store laser attack upgrade levels for temporary disabling/enabling
		primaryTemp = primaryAttack.setPrimaryAttackLevel;
		multiTemp = multiAttack.setMultiAttackLevel;
		bursting = false;

	
	}

	// ---------------------------------------------------------------------------------------------------
	// UPDATE
	// ---------------------------------------------------------------------------------------------------
	void Update () {

		PrimaryAttack();

		MultiAttack();

		BurstAttack();

		BombAttack();

		UpgradeKeys();
	}

	// ---------------------------------------------------------------------------------------------------
	// PowerUp Control Gather
	// ---------------------------------------------------------------------------------------------------
		void OnTriggerEnter(Collider other){
		if (other.tag == "powerUp_fireRate"){
			fireRate = fireRate * 0.9f;
			if(fireRate <= 0.075f){
				fireRate = 0.075f;
			}
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
			bombsAvailable += 1;

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

		if (primaryAttack.setPrimaryAttackLevel == 1 && bursting == false){	//Level 1
			if (Time.time > primaryAttackNextFire) {
				primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv1, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
				Instantiate(laserSound, transform.position, transform.rotation);
            	//audioSource.Play();
				//audioClips[0].Play();
			}
		}
		if (primaryAttack.setPrimaryAttackLevel == 2 && bursting == false){	//Level 2
			if (Time.time > primaryAttackNextFire) {
				primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv2, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
				Instantiate(laserSound, transform.position, transform.rotation);
				//audioSource.Play();
				//audioClips[0].Play();
			}
		}
		if (primaryAttack.setPrimaryAttackLevel == 3 && bursting == false){	//Level 3
			if (Time.time > primaryAttackNextFire) {
            	primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv3, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
				Instantiate(laserSound, transform.position, transform.rotation);
				//audioSource.Play();
				//audioClips[0].Play();
			}
		}
		if (primaryAttack.setPrimaryAttackLevel == 4 && bursting == false){ 	//Level 4
			if (Time.time > primaryAttackNextFire) {
            	primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv4, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
				Instantiate(laserSound, transform.position, transform.rotation);
				//audioSource.Play();
				//audioClips[0].Play();
			}
		}
	}

	// ---------------------------------------------------------------------------------------------------
	//MULTI SHOT. Auto-fired. Level based on setPrimaryAttackLevel.
	// ---------------------------------------------------------------------------------------------------
	void MultiAttack(){
		if (multiAttack.setMultiAttackLevel == 1 && bursting == false){	//Level 1
			if (Time.time > multiAttackNextFire) {
            	multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv1, multiAttack.multiShotSpawnLv1L.position, multiAttack.multiShotSpawnLv1L.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv1, multiAttack.multiShotSpawnLv1R.position, multiAttack.multiShotSpawnLv1R.rotation);
	            //audioSource.Play();
			}
		}
		if (multiAttack.setMultiAttackLevel == 2 && bursting == false){	//Level 2
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
		if (multiAttack.setMultiAttackLevel == 3 && bursting == false){	//Level 3
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
	//BURST ATTACK. Fire on LMB press
	// ---------------------------------------------------------------------------------------------------

	void BurstAttack ()
	{

		//Determine Burst attack level
		burstAttackDamage = primaryAttack.setPrimaryAttackLevel + multiAttack.setMultiAttackLevel;


		//Level 1
		if (burstAttack.setBurstAttackLevel == 1) {
			
			if (Input.GetMouseButton (0) && burst > 0) {

				//Disable auto laser cannons during Burst Attack
				bursting = true;

				//Set mouseDown to true to prevent recharging while using Burst
				mouseDown = true;
	

				if (Time.time > burstAttackNextFire) {
					burstAttackNextFire = Time.time + burstFireRate * 2;
					if (burstAttackDamage == 1) {
						Instantiate (burstAttack.burstAttackLv1, burstAttack.burstShotSpawn.position, burstAttack.burstShotSpawn.rotation);
					}
					if (burstAttackDamage == 2) {
						Instantiate (burstAttack.burstAttackLv1, burstAttack.burstShotSpawn.position, burstAttack.burstShotSpawn.rotation);
					}
					if (burstAttackDamage == 3) {
						Instantiate (burstAttack.burstAttackLv2, burstAttack.burstShotSpawn.position, burstAttack.burstShotSpawn.rotation);
					}
					if (burstAttackDamage == 4) {
						Instantiate (burstAttack.burstAttackLv2, burstAttack.burstShotSpawn.position, burstAttack.burstShotSpawn.rotation);
					}
					if (burstAttackDamage == 5) {
						Instantiate (burstAttack.burstAttackLv3, burstAttack.burstShotSpawn.position, burstAttack.burstShotSpawn.rotation);
					}
					if (burstAttackDamage == 6) {
						Instantiate (burstAttack.burstAttackLv3, burstAttack.burstShotSpawn.position, burstAttack.burstShotSpawn.rotation);
					}
					if (burstAttackDamage == 7) {
						Instantiate (burstAttack.burstAttackLv4, burstAttack.burstShotSpawn.position, burstAttack.burstShotSpawn.rotation);
					}
				}
				burst -= Time.deltaTime * 20;

				 
			} else {
				//Re-enable auto laser cannons
				bursting = false;

				//Set mouseDown to false to allow recharge (below)
				mouseDown = false;
			}
		}

		//Level 2
		if (burstAttack.setBurstAttackLevel == 2){
			maxBurst = 40;

			while (Input.GetMouseButton (0) && burst > 0) {
				mouseDown = true;

				if (Time.time > burstAttackNextFire) {
					burstAttackNextFire = Time.time + burstFireRate * 2;
					Instantiate (burstAttack.burstAttackLv2, burstAttack.burstShotSpawnL.position, burstAttack.burstShotSpawnL.rotation);
					//audioSource.Play();

					burstAttackNextFire = Time.time + burstFireRate * 2;
					Instantiate (burstAttack.burstAttackLv2, burstAttack.burstShotSpawnR.position, burstAttack.burstShotSpawnR.rotation);
					//audioSource.Play();
				}
				//While mouse is pressed down, decrease burst meter at this rate
				burst -= Time.deltaTime * 20;
			} 

			mouseDown = false;
		//Set mouseDown to false to allow recharge (below)
		}

		//Burst to stop recharging at maxBurst
		if (burst >= maxBurst){
			burst = maxBurst;
		}

		//Show burst meter fill amount
		burstFillAmount = (burst / maxBurst);
		if (burstFillAmount != burstBar.fillAmount) {
			burstBar.fillAmount = burstFillAmount;
		}

		//Allow burst meter to recharge when mouse isn't pressed down
		if (mouseDown == false){
			if (Time.time > nextBurstRecharge){
				nextBurstRecharge = Time.time + burstRechargeDelay;
				if (burstAttack.setBurstAttackLevel == 1){
					burst += 0.1f;
				}
				if (burstAttack.setBurstAttackLevel == 2){
					burst += 0.2f;
				}
			}
		}
	}

	// ---------------------------------------------------------------------------------------------------
	//BOMB ATTACK. Fire on RMB press
	// ---------------------------------------------------------------------------------------------------
	void BombAttack(){
		if (Input.GetMouseButtonDown (1) && bombsAvailable >= 1) {
			Instantiate(bombAttack.bomb, bombAttack.shotSpawn.position, bombAttack.shotSpawn.rotation);
			//audioClips[2].Play();
			bombsAvailable -= 1;
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
		if (Input.GetKeyDown(".") && fireRate > .075){	//+10% Fire Rate
			fireRate = fireRate * 0.9f;
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

	public float getBombsAvailable(){
		return bombsAvailable;
	}
}

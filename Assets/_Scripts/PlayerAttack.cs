﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class PrimaryAttack 	//Collapsible menu to access Primary Attack settings.
{
	public float setPrimaryAttackLevel; //Set Primary Attack level.
	public GameObject primaryAttackLv1, primaryAttackLv2, primaryAttackLv3, primaryAttackLv4; //Place Art for each level here (in Inspector).
	public Transform primaryShotSpawn; //Where Primary Attacks will spawn.
}

[System.Serializable]
public class MultiAttack  	//Collapsible menu to access Multi Attack settings.
{
	public float setMultiAttackLevel; //Set Multi Attack level.
	public GameObject multiAttackLv1, multiAttackLv2, multiAttackLv2R, multiAttackLv2L, multiAttackLv3, multiAttackLv3R, multiAttackLv3L; //Place Art for each level here (in Inspector).
	public Transform multiShotSpawnLv1R, multiShotSpawnLv1L, multiShotSpawnLv2R,  multiShotSpawnLv2L;
}

public class PlayerAttack : MonoBehaviour {

	public PrimaryAttack primaryAttack;
	public MultiAttack multiAttack;
	public GameObject specialAttack;
	public float fireRate;
	private float primaryAttackNextFire;
	private float multiAttackNextFire;

	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		//BASIC ATTACK. Auto-fired. Level based on setPrimaryAttackLevel in Inspector.
		if (primaryAttack.setPrimaryAttackLevel == 1)	//Level 1
		{
			if (Time.time > primaryAttackNextFire) 
			{
				primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv1, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
            	audioSource.Play();
			}
		}
		if (primaryAttack.setPrimaryAttackLevel == 2)	//Level 2
		{
			if (Time.time > primaryAttackNextFire) 
			{
				primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv2, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
            	audioSource.Play();
			}
		}
		if (primaryAttack.setPrimaryAttackLevel == 3)	//Level 3
		{
			if (Time.time > primaryAttackNextFire) 
			{
            	primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv3, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
            	audioSource.Play();
			}
		}
		if (primaryAttack.setPrimaryAttackLevel == 4) 	//Level 4
		{
			if (Time.time > primaryAttackNextFire) 
			{
            	primaryAttackNextFire = Time.time + fireRate;
				Instantiate(primaryAttack.primaryAttackLv4, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
            	audioSource.Play();
			}
		}

		//MULTI SHOT. Auto-fired. Level based on setPrimaryAttackLevel in Inspector.
		if (multiAttack.setMultiAttackLevel == 1)	//Level 1
		{
			if (Time.time > multiAttackNextFire) 
			{
            	multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv1, multiAttack.multiShotSpawnLv1L.position, multiAttack.multiShotSpawnLv1L.rotation);
	            //audioSource.Play();
				multiAttackNextFire = Time.time + fireRate * 2;
				Instantiate(multiAttack.multiAttackLv1, multiAttack.multiShotSpawnLv1R.position, multiAttack.multiShotSpawnLv1R.rotation);
	            //audioSource.Play();
			}
		}
		if (multiAttack.setMultiAttackLevel == 2)	//Level 2
		{
			if (Time.time > multiAttackNextFire) 
			{
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
		if (multiAttack.setMultiAttackLevel == 3)	//Level 3
		{
			if (Time.time > multiAttackNextFire) 
			{
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

		//SPECIAL ATTACK. Fire on "space bar" press
		if (Input.GetKeyDown(KeyCode.Space))
		{
            Instantiate(specialAttack, primaryAttack.primaryShotSpawn.position, primaryAttack.primaryShotSpawn.rotation);
            audioSource.Play();
		}
	}

		void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Powerup")
		{
			fireRate = fireRate * 0.9f;
			Destroy(other.gameObject);
		}
	}
}
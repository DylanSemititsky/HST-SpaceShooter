//CONTROLS WHICH ATTACKS AN ENEMY IS USING AND RATE OF FIRE

using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyBasicAttack 	//Collapsible menu to access Primary Attack settings.
{
	public float setEnemyBasicAttackLevel; //Set Primary Attack level.
	public GameObject enemyBasicAttackLv1, enemyBasicAttackLv2, enemyBasicAttackLv3, enemyBasicAttackLv4; //Place Art for each level here (in Inspector).
	public Transform enemyBasicAttackShotSpawn; //Where Primary Attacks will spawn.
}

public class EnemyAttack : MonoBehaviour {

	public EnemyBasicAttack enemyBasicAttack;
	public float fireRate;
	private float enemyBasicAttackNextFire;

	void Update () {

		//BASIC ATTACK. Auto-fired. Level based on setPrimaryAttackLevel in Inspector.
		if (enemyBasicAttack.setEnemyBasicAttackLevel == 1)	//Level 1
		{
			if (Time.time > enemyBasicAttackNextFire) 
			{
				enemyBasicAttackNextFire = Time.time + fireRate;
				Instantiate(enemyBasicAttack.enemyBasicAttackLv1, enemyBasicAttack.enemyBasicAttackShotSpawn.position, enemyBasicAttack.enemyBasicAttackShotSpawn.rotation);
				//audioSource.Play();
			}
		}
		if (enemyBasicAttack.setEnemyBasicAttackLevel == 2)	//Level 2
		{
			if (Time.time > enemyBasicAttackNextFire) 
			{
				enemyBasicAttackNextFire = Time.time + fireRate;
				Instantiate(enemyBasicAttack.enemyBasicAttackLv2, enemyBasicAttack.enemyBasicAttackShotSpawn.position, enemyBasicAttack.enemyBasicAttackShotSpawn.rotation);
				//audioSource.Play();
			}
		}
		if (enemyBasicAttack.setEnemyBasicAttackLevel == 3)	//Level 3
		{
			if (Time.time > enemyBasicAttackNextFire) 
			{
				enemyBasicAttackNextFire = Time.time + fireRate;
				Instantiate(enemyBasicAttack.enemyBasicAttackLv3, enemyBasicAttack.enemyBasicAttackShotSpawn.position, enemyBasicAttack.enemyBasicAttackShotSpawn.rotation);
				//audioSource.Play();
			}
		}
		if (enemyBasicAttack.setEnemyBasicAttackLevel == 4)	//Level 4
		{
			if (Time.time > enemyBasicAttackNextFire) 
			{
				enemyBasicAttackNextFire = Time.time + fireRate;
				Instantiate(enemyBasicAttack.enemyBasicAttackLv4, enemyBasicAttack.enemyBasicAttackShotSpawn.position, enemyBasicAttack.enemyBasicAttackShotSpawn.rotation);
				//audioSource.Play();
			}
		}
	}
}

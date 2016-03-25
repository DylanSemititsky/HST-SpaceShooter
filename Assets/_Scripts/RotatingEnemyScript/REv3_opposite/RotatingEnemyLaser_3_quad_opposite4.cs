﻿using UnityEngine;
using System.Collections;

public class RotatingEnemyLaser_3_quad_opposite4 : MonoBehaviour {

	public float laserSpeed;
	private Vector3 direction1;
	private Vector3 direction2;

	void Start () {
		
		//grab script from rotating enemy game object
		GameObject rotatingEnemy = GameObject.Find ("RotatingEnemy_3_quad_opposite(Clone)"); 
		RotatingEnemyScript_3_quad_opposite rotatingEnemyScript = (RotatingEnemyScript_3_quad_opposite) rotatingEnemy.GetComponent(typeof(RotatingEnemyScript_3_quad_opposite));

		direction1 = rotatingEnemyScript.frontDirection;
		direction2 = rotatingEnemyScript.sideDirection;
	}
	
	void Update () {
		transform.Translate(direction2.x * -laserSpeed * Time.deltaTime,direction2.y,direction2.z  * -laserSpeed * Time.deltaTime);
	}



}
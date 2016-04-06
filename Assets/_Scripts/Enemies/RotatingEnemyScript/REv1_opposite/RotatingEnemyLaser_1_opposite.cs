using UnityEngine;
using System.Collections;

public class RotatingEnemyLaser_1_opposite : MonoBehaviour {

	public float laserSpeed;
	private Vector3 direction1;

	void Start () {
		
		//grab script from rotating enemy game object
		GameObject rotatingEnemy = GameObject.Find ("RotatingEnemy_1_opposite(Clone)"); 
		RotatingEnemyScript_opposite rotatingEnemyScript = (RotatingEnemyScript_opposite) rotatingEnemy.GetComponent(typeof(RotatingEnemyScript_opposite));

		direction1 = rotatingEnemyScript.frontDirection;
	}
	
	void Update () {
		transform.Translate(direction1.x * laserSpeed * Time.deltaTime,direction1.y,direction1.z  * laserSpeed * Time.deltaTime);
	}



}
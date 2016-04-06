using UnityEngine;
using System.Collections;

public class RotatingEnemyLaser_2_double1_opposite : MonoBehaviour {

	public float laserSpeed;
	private Vector3 direction1;

	void Start () {
		
		//grab script from rotating enemy game object
		GameObject rotatingEnemy = GameObject.Find ("RotatingEnemy_2_double_opposite(Clone)"); 
		RotatingEnemyScript_2_double_opposite rotatingEnemyScript = (RotatingEnemyScript_2_double_opposite) rotatingEnemy.GetComponent(typeof(RotatingEnemyScript_2_double_opposite));

		direction1 = rotatingEnemyScript.frontDirection;
	}
	
	void Update () {
		transform.Translate(direction1.x * laserSpeed * Time.deltaTime,direction1.y,direction1.z  * laserSpeed * Time.deltaTime);
	}



}
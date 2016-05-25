using UnityEngine;
using System.Collections;

public class HomingEnemyLaser : MonoBehaviour {

	float laserSpeed = 4;
	public Transform target;

	private Transform myTransform;

	void Awake(){
		myTransform = transform;
	}
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		if(go != null){
		target = go.transform;
		//rotate the projectile to aim at target
		myTransform.LookAt(target);
		}
	}
		

	void Update () {
		//distance moved during laste frame
		float step = laserSpeed * Time.deltaTime;

		//translate projectile in its forward direction
		myTransform.Translate (Vector3.forward * step);

		//transform.position = Vector3.MoveTowards (transform.position, target2.transform.position, step );
		//transform.position = Vector3.Lerp(transform.position, target2.transform.position, step);

	}
}

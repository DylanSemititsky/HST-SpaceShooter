using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {

	public Rigidbody missile;
	private float missleNextFire;
	public float fireRate;

	void Update () {
		//if(Input.GetKeyDown(KeyCode.Space)){
			//Instantiate(missile, transform.position, transform.rotation);
		//}

		if (Time.time > missleNextFire) 
		{
			missleNextFire = Time.time + fireRate;
			Instantiate(missile, transform.position, transform.rotation);
		}
	}
}
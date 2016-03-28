using UnityEngine;
using System.Collections;

public class Boss1Script : MonoBehaviour {

	public GameObject homingLaser;

	public int homingLaserWaveAmount;
	public float homingLaserFireRate;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine (WaitToShoot(homingLaserFireRate));

		}
	}
	IEnumerator WaitToShoot(float laserRate){
		
		for (int i = 0; i < homingLaserWaveAmount; i++) { //fires the laser X amount of times
			Instantiate (homingLaser, transform.position, Quaternion.identity); //creates laser
			yield return new WaitForSeconds (laserRate); //waits until laser is shot and shoots again
		}
	}
}

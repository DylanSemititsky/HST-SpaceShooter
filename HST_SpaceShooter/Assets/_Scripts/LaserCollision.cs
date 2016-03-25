using UnityEngine;
using System.Collections;

public class LaserCollision : MonoBehaviour {

	public GameObject explosionLaser;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}

		if (other.tag == "Enemy") {
			Instantiate (explosionLaser, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		if (other.tag == "Asteroid") {
			Instantiate (explosionLaser, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}

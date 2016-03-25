//DESTROYS ANY OBJECT WITH A TRIGGER COLLIDER BASED ON TAGS (PLAYER AND WEAPON)
//TRIGGERS EXPLOSION PARTICLES

using UnityEngine;
using System.Collections;

public class DestroyByHealth : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject explosionAsteroids;
	public GameObject cupcake;
	public float health = 50;

	
	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}

		if (other.tag == "pLaser 1") {
			health = health - 10;
		}
		if (other.tag == "pLaser 2") {
				health = health - 20;
			}
		if (other.tag == "pLaser 3") {
				health = health - 30;
			}
		if (other.tag == "pLaser 4") {
				health = health - 40;
			}
		}

	void Update (){
		if (health <= 0){
			if (tag == "Enemy"){
				Instantiate(explosion, transform.position, transform.rotation);
				Instantiate (cupcake, transform.position, transform.rotation);
			}
			if (tag == "Asteroid") {
				Instantiate(explosion, transform.position, transform.rotation);
				Instantiate (explosionAsteroids, transform.position, transform.rotation);
			}
			Destroy(gameObject);
		}
	}
}

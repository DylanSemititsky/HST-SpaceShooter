//DESTROYS ANY OBJECT WITH A TRIGGER COLLIDER BASED ON TAGS (PLAYER AND WEAPON)
//TRIGGERS EXPLOSION PARTICLES

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyByHealth : MonoBehaviour 
{
	//Set explosion for this object
	public GameObject explosion;
	public GameObject explosionAsteroids;

	//PowerUp Objects and Drop Chance Ranges
	public GameObject item1;
	public float item1DropMin;
	public float item1DropMax;
	public GameObject item2;
	public float item2DropMin;
	public float item2DropMax;
	public GameObject item3;
	public float item3DropMin;
	public float item3DropMax;


	//Random number to determine dropped item
	private float randomNumber;

	//Gives Score value to GameController
	public GameController gameController;
	public int scoreValue;

	//Set Health of enemy/object
	public float health = 50;

	//Color of damage flash when hit
	public float flashSpeed = 5f;
	public Color flashColor = new Color (1f, 0f, 0f, 0.1f);
	bool damaged;



	void Start(){
		randomNumber = Random.Range (1, 100);

		GameObject gameControllerObject = GameObject.Find ("GameController");
		//Debug.Log("GameController Found");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
	}
	


	void Update (){

		if (damaged) {
			StartCoroutine (DamageFlash ());
		}
		if (health <= 0){
			if (tag == "Enemy"){
				Instantiate(explosion, transform.position, transform.rotation);

				if (randomNumber >= item1DropMin && randomNumber <= item1DropMax) {
					Instantiate (item1, transform.position, item1.transform.rotation);
				}
				if (randomNumber >= item2DropMin && randomNumber <=item2DropMax) {
					Instantiate (item2, transform.position, item2.transform.rotation);
				}
				if (randomNumber >= item3DropMin && randomNumber <= item3DropMax) {	//DISABLED FOR DEMO
					Instantiate (item3, transform.position, item3.transform.rotation);
				}
			}
			if (tag == "Asteroid") {
				Instantiate (explosionAsteroids, transform.position, transform.rotation);
			}

			gameController.AddScore (scoreValue);
			Destroy(gameObject);
		}
	}

	public void AddDamage (float newDamageNumber){
		health -= newDamageNumber;
		damaged = true;
		return;
	}


	public IEnumerator DamageFlash(){
		
		Renderer rend = GetComponentInChildren<Renderer> ();
		for(int i = 1; i <= 4; i++){
			rend.material.SetColor ("_Color", Color.red);
			yield return new WaitForSeconds (0.1f);
			rend.material.SetColor ("_Color", Color.white);
			yield return new WaitForSeconds (0.1f);
			damaged = false;
		}
	}


	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}

		if (other.tag == "pLaser 1") {
			health = health - 10;
			damaged = true;
		}
		if (other.tag == "pLaser 2") {
			health = health - 20;
			damaged = true;
		}
		if (other.tag == "pLaser 3") {
			health = health - 30;
			damaged = true;
		}
		if (other.tag == "pLaser 4") {
			health = health - 40;
			damaged = true;
		}
	}
}

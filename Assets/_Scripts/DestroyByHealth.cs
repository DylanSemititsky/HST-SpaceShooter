//DESTROYS ANY OBJECT WITH A TRIGGER COLLIDER BASED ON TAGS (PLAYER AND WEAPON)
//TRIGGERS EXPLOSION PARTICLES

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyByHealth : MonoBehaviour 
{
	//Set explosion for this object
	public GameObject explosion;

	//PowerUp Objects and Drop Chance Ranges
	public GameObject heal;
	public float healDropMin;
	public float healDropMax;
	public GameObject bomb;
	public float bombDropMin;
	public float bombDropMax;
	public GameObject item3;
	public float item3DropMin;
	public float item3DropMax;
	//CameraShakeVariables
	//public float amplitude = 0.1f;
	//public float duration = 0.5f;


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

	//Player access for repair/bomb item drops
	PlayerController playerController;
	PlayerAttack playerAttack;


	void Start(){
		randomNumber = Random.Range (1, 100);

		GameObject gameControllerObject = GameObject.Find ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		GameObject playerObject = GameObject.Find ("Player");
		if (playerObject != null) {
			playerController = playerObject.GetComponent<PlayerController> ();
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}
	}
	


	void Update (){

		if (damaged) {
			StartCoroutine (DamageFlash ());
		}
		if (health <= 0){

			Instantiate(explosion, transform.position, transform.rotation);

			if (randomNumber >= healDropMin && randomNumber <= healDropMax && playerController.health < playerController.maxHealth) {
				Instantiate (heal, transform.position, heal.transform.rotation);
				}
			else if (randomNumber >= bombDropMin && randomNumber <=bombDropMax && playerAttack.bombAttack.bomb < playerAttack.bombAttack.maxBomb) {
				Instantiate (bomb, transform.position, bomb.transform.rotation);
				}
			else if (randomNumber >= item3DropMin && randomNumber <= item3DropMax) {	
				Instantiate (item3, transform.position, item3.transform.rotation);
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

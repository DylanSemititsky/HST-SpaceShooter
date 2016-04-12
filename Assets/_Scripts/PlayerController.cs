// ---------------------------------------------------------------------------------------------------
// PLAYER CONTROLLER
// Controls player: Health, Shield, Movement
// Controls Health bar visuals
// Controls damage sound effects
// Collects variables from GameState manager everytime a scene is loaded 
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Boundary 		//Collapsible menu to set game's playable Boundaries.
{
	public float xMin, xMax, zMin, zMax;
}



public class PlayerController : MonoBehaviour 
{
	//Movement
	private Rigidbody rb;
	public float speed;
	public float tilt;
	public Boundary boundary;

	//Health and Shield
	public int setMaxHealth;
	public int setMaxShield;
	public float maxHealth;
	public float health;
	public float maxShield;
	public float shield;
	public float rechargeDelay;
	private float nextRecharge;
	private float healthFillAmount;
	private float shieldFillAmount;
	public Image healthBar;
	public Image shieldBar;
	public GUIText healthNumbers;


	//Damage indicator
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color (1f, 0f, 0f, 0.1f);
	public GameObject explosion;
	bool damaged;

	GameState gameState;
	GameController gameController;
	public AudioSource audioSource;


	// ---------------------------------------------------------------------------------------------------
	// START
	// ---------------------------------------------------------------------------------------------------
	void Start (){
		

		//Allow "rb" to be used for GetComponent<Rigidbody>()
		rb = GetComponent<Rigidbody>(); 			
	
		//Find GameController Object to access it's script
		GameObject gameControllerObject = GameObject.Find ("GameController");	
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		//Find GameState Object to access it's script
		GameObject gameStateObject = GameObject.Find ("GameState");	
		if (gameStateObject != null) {
			gameState = gameStateObject.GetComponent<GameState> ();
		}

		//Set Health/Shield values for new scene load (from GameState manager)
		setMaxHealth = gameState.getHealth();
		setHealth();

		setMaxShield = gameState.getShield();
		setShield();

	}



	// ---------------------------------------------------------------------------------------------------
	// UPDATE
	// ---------------------------------------------------------------------------------------------------
	void Update (){

		//(MOUSE)
		float moveHorizontalMouse = Input.GetAxis ("Mouse X");
		float moveVerticalMouse = Input.GetAxis ("Mouse Y");

		Vector3 movementMouse = new Vector3 (moveHorizontalMouse, 0.0f, moveVerticalMouse);
		rb.velocity = movementMouse * speed;

		// ---------------------------------------------------------------------------------------------------
		// Health/Shield bar Adjustment
		// ---------------------------------------------------------------------------------------------------
		//Adjust Health bar when health is reduced
		healthFillAmount = (health / maxHealth);
		if (healthFillAmount != healthBar.fillAmount) {
			healthBar.fillAmount = healthFillAmount;
		}
		//Adjust Shield bar when shield is reduced
		shieldFillAmount = (shield / maxShield);
		if (shieldFillAmount != shieldBar.fillAmount) {
			shieldBar.fillAmount = shieldFillAmount;
		}

		healthNumbers.text = health + " / " + maxHealth;

		// ---------------------------------------------------------------------------------------------------
		//Full Screen flash when hit
		// ---------------------------------------------------------------------------------------------------
		//Flash screen red when damaged.
		if(damaged){
			damageImage.color = flashColor;
		}
		else{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		// ---------------------------------------------------------------------------------------------------
		// Death
		// ---------------------------------------------------------------------------------------------------
		//Death. Explosion when health reduced to 0.
		if (health <= 0){
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
			gameController.GameOver ();
		}

		// ---------------------------------------------------------------------------------------------------
		//Shield Recharge
		// ---------------------------------------------------------------------------------------------------
		//Prevent shield from exceeding maximum amount
		if (shield >= maxShield){
			return;
		}

		//Recharge Shield. +10 every "rechargeDelay" seconds.
		if (Time.time > nextRecharge){
			nextRecharge = Time.time + rechargeDelay;
			shield += 0.1f;
		}
	}


	// ---------------------------------------------------------------------------------------------------
	//FIXED UPDATE
	// ---------------------------------------------------------------------------------------------------
	void FixedUpdate ()
	{	
		// ---------------------------------------------------------------------------------------------------
		// MOVEMENT
		// ---------------------------------------------------------------------------------------------------
		/*//(WASD)
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;*/

	
		// ---------------------------------------------------------------------------------------------------
		//BOUNDARIES. Create impassable borders for the player ship at edge of screen, set Boundaries in Inspector
		// ---------------------------------------------------------------------------------------------------
		rb.position = new Vector3 
		(
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		// ---------------------------------------------------------------------------------------------------
		//TILT. Add tilt to player ship when moving from side to side
		// ---------------------------------------------------------------------------------------------------
		//rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}


	public void setHealth(){
		if (setMaxHealth == 1) {
			maxHealth = 100;
			health = maxHealth;
		}
		if (setMaxHealth == 2) {
			maxHealth = 150;
			health = maxHealth;
		}
		if (setMaxHealth == 3) {
			maxHealth = 200;
			health = maxHealth;
		}
		if (setMaxHealth == 4) {
			maxHealth = 300;
			health = maxHealth;
		}
		if (setMaxHealth == 5) {
			maxHealth = 500;
			health = maxHealth;
		}
	}

	public void setShield(){
		if (setMaxShield == 1) {
			maxShield = 30;
			shield = maxShield;
		}
		if (setMaxShield == 2) {
			maxShield = 40;
			shield = maxShield;
		}
		if (setMaxShield == 3) {
			maxShield = 55;
			shield = maxShield;
		}
		if (setMaxShield == 4) {
			maxShield = 75;
			shield = maxShield;
		}
		if (setMaxShield == 5) {
			maxShield = 100;
			shield = maxShield;
		}
	}


	// ---------------------------------------------------------------------------------------------------
	//Lose Health or Shield depeding on enemy attack "Tag".
	// ---------------------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary"){
			return;
		}
		//Lv 1 Laser hit
		if (shield > 0 && other.tag == "eLaser 1"){ //Enemy Attack Level 1 vs Shield
			shield -= 10;
			Destroy(other.gameObject);
			damaged = true;
			audioSource.Play();
		}
		if (shield <= 0 && other.tag == "eLaser 1"){ //Enemy Attack Level 1 vs Health
			Destroy(other.gameObject);
			damaged = true;
			health -= 10;
			audioSource.Play();
		}
		//Lv 2 Laser hit
		if (shield > 0 && other.tag == "eLaser 2"){ //Enemy Attack Level 2 vs Shield
			shield -= 20;
			Destroy(other.gameObject);
			damaged = true;
			audioSource.Play();
		}
		if (shield <= 0 && other.tag == "eLaser 2"){ //Enemy Attack Level 2 vs Health
			Destroy(other.gameObject);
			damaged = true;
			health -= 20;
			audioSource.Play();
		}
		//Lv 3 Laser hit
		if (shield > 0 && other.tag == "eLaser 3"){ //Enemy Attack Level 3 vs Shield
			shield -= 30;
			Destroy(other.gameObject);
			damaged = true;
			audioSource.Play();
		} 
		if (shield <= 0 && other.tag == "eLaser 3"){ //Enemy Attack Level 3 vs Health
			Destroy(other.gameObject);
			damaged = true;
			health -= 30;
			audioSource.Play();
		} 
		//Lv 4 Laser hit
		if (shield > 0 && other.tag == "eLaser 4"){ //Enemy Attack Level 4 vs Shield
			shield -= 40;
			Destroy(other.gameObject);
			damaged = true;
			audioSource.Play();
		}
		if (shield <= 0 && other.tag == "eLaser 4"){ //Enemy Attack Level 4 vs Health
			Destroy(other.gameObject);
			damaged = true;
			health -= 40;
			audioSource.Play();
		}
		//Enemy Collision Damage
		if (shield > 0 && other.tag == "Enemy"){ //Enemy Attack Level 1 vs Shield
			shield -= 20;
			damaged = true;
			audioSource.Play();
		}
		if (shield <= 0 && other.tag == "Enemy"){ //Enemy Attack Level 1 vs Health
			damaged = true;
			audioSource.Play();
			health -= 20;
		}

		//Asteroid Collision
		if (shield > 0 && other.tag == "Asteroid"){ //Enemy Attack Level 1 vs Shield
			shield -= 50;
			damaged = true;
			audioSource.Play();
		}
		if (shield <= 0 && other.tag == "Asteroid"){ //Enemy Attack Level 1 vs Health
			damaged = true;
			audioSource.Play();
			health -= 50;
		}
		if (other.tag == "powerUp_heal"){ //Heal 50 hp from PowerupHeal
			health += 50;
			Destroy(other.gameObject);
			if (health > maxHealth) {
				health = maxHealth;
			}
		}
	} 

	// ---------------------------------------------------------------------------------------------------
	// Return values for storing in GameState Manager
	// ---------------------------------------------------------------------------------------------------
	public int getHealth(){
		return setMaxHealth;
	}

	public int getShield(){
		return setMaxShield;
	}
}

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
	private bool cancelRecharge;
	private float healthFillAmount;
	private float shieldFillAmount;
	public Image healthBar;
	public Image shieldBar;
	public Text healthNumbers;
	public GameObject popupHP;

	//Shield Glow
	/*public Image shieldGlow;*/
	public GameObject shieldCanvas;
	CanvasGroup canvasGroup;
	public GameObject[] shieldGlow;

	//Damage indicator
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColorRed = new Color (1f, 0f, 0f, 0.5f);
	public Color flashColorYellow = new Color (1f, 1f, 0f, 0.1f);
	public GameObject hitYellow;
	public GameObject hitOrange;
	public GameObject hitRed;
	public GameObject hitPurple;
	public GameObject playerExplosion;
	bool damaged1;
	bool damaged2;

	//Player's credits
	public int credits;
	public Text creditsText;
	public GameObject popupCredits;


	//Game Managers
	GameState gameState;
	GameController gameController;

	//Audio
	public AudioSource audioSource;
	public GameObject deathSound;
	public GameObject healSound;
	public GameObject creditsSound;
	public GameObject shieldAbsorbSound;

	//Shaking Camera variables
	public float amplitude = 0.1f;
	public float duration = 0.5f;


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

		canvasGroup = shieldCanvas.GetComponent<CanvasGroup>();

		//Set Health/Shield values for new scene load (from GameState manager)
		setMaxHealth = gameState.getHealth();
		setHealth();

		setMaxShield = gameState.getShield();
		setShield();

		//Set credits value for new scene load (from GameState manager)
		credits = gameState.getCredits(); 

	}



	// ---------------------------------------------------------------------------------------------------
	// UPDATE
	// ---------------------------------------------------------------------------------------------------
	void Update (){

		CreditsControl();

		Movement();

		HealthBarAdjustment();

		DamageFlash();

		ShieldRecharge();

		ShieldGlow ();

		Death();

	}


	// ---------------------------------------------------------------------------------------------------
	//FIXED UPDATE
	// ---------------------------------------------------------------------------------------------------
	void FixedUpdate (){	
		// ---------------------------------------------------------------------------------------------------
		// MOVEMENT (OLD/Alternate)
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
		//TILT. Add tilt to player ship when moving from side to side (Only works well with WASD controls)
		// ---------------------------------------------------------------------------------------------------
		//rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}


	// ---------------------------------------------------------------------------------------------------
	//Credits Control
	// ---------------------------------------------------------------------------------------------------
	void CreditsControl(){
		creditsText.text = "Credits: " + credits;


		if (Input.GetKeyDown("=")){
			credits += 100;
		}
	}


	// ---------------------------------------------------------------------------------------------------
	//MOVEMENT
	// ---------------------------------------------------------------------------------------------------
	//(MOUSE)
	void Movement(){
		float moveHorizontalMouse = Input.GetAxis ("Mouse X");
		float moveVerticalMouse = Input.GetAxis ("Mouse Y");

		Vector3 movementMouse = new Vector3 (moveHorizontalMouse, 0.0f, moveVerticalMouse);
		rb.velocity = movementMouse * speed;
		}


	// ---------------------------------------------------------------------------------------------------
	// Health/Shield bar Adjustment
	// ---------------------------------------------------------------------------------------------------
	void HealthBarAdjustment(){

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
	}


	// ---------------------------------------------------------------------------------------------------
	//Full Screen flash when hit
	// ---------------------------------------------------------------------------------------------------
	void DamageFlash(){
		//Flash screen red when damaged.
		if(damaged1){
			damageImage.color = flashColorRed;
		}
		else{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged1 = false;

		//Flash screen yellow when shield damaged.
		if(damaged2){
			damageImage.color = flashColorYellow;
		}
		else{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged2 = false;
	}



	// ---------------------------------------------------------------------------------------------------
	//Shield Recharge
	// ---------------------------------------------------------------------------------------------------
	void ShieldRecharge(){
		//Prevent shield from exceeding maximum amount
		if (shield >= maxShield){
			return;
		}

		//Recharge Shield. +0.1 every "rechargeDelay" seconds.
		if (Time.time > nextRecharge && cancelRecharge == false){
			nextRecharge = Time.time + rechargeDelay;
			shield += 0.1f;
		}
	}


	// ---------------------------------------------------------------------------------------------------
	// Shield Glow
	// ---------------------------------------------------------------------------------------------------
	void ShieldGlow(){
		//Disable/Enable shield glow when shield is down/up
		if (shield <= 0) {
			shieldCanvas.SetActive (false);
		} else shieldCanvas.SetActive (true);

		if (setMaxShield == 1) {
			shieldGlow [0].SetActive (true);
			shieldGlow [1].SetActive (false);
			shieldGlow [2].SetActive (false);
			shieldGlow [3].SetActive (false);
			shieldGlow [4].SetActive (false);
		}
		if (setMaxShield == 2) {
			shieldGlow [0].SetActive (true);
			shieldGlow [1].SetActive (true);
			shieldGlow [2].SetActive (false);
			shieldGlow [3].SetActive (false);
			shieldGlow [4].SetActive (false);
		}
		if (setMaxShield == 3) {
			shieldGlow [0].SetActive (true);
			shieldGlow [1].SetActive (true);
			shieldGlow [2].SetActive (true);
			shieldGlow [3].SetActive (false);
			shieldGlow [4].SetActive (false);
		}
		if (setMaxShield == 4) {
			shieldGlow [0].SetActive (true);
			shieldGlow [1].SetActive (true);
			shieldGlow [2].SetActive (true);
			shieldGlow [3].SetActive (true);
			shieldGlow [4].SetActive (false);
		}
		if (setMaxShield == 5) {
			shieldGlow [0].SetActive (true);
			shieldGlow [1].SetActive (true);
			shieldGlow [2].SetActive (true);
			shieldGlow [3].SetActive (true);
			shieldGlow [4].SetActive (true);
		}

	}

	// ---------------------------------------------------------------------------------------------------
	// Death
	// ---------------------------------------------------------------------------------------------------
	//Death. Explosion when health reduced to 0.
	void Death(){
		if (health <= 0){
			Destroy(gameObject);
			Instantiate(playerExplosion, transform.position, transform.rotation);
			CameraShake.Instance.Shake (amplitude, duration);//call camera shake
			Instantiate(deathSound, transform.position, Quaternion.identity);
			gameController.GameOver ();
		}
	}


	public void setHealth(){
		if (setMaxHealth == 1) {
			maxHealth = 100;
			health = maxHealth;
		}
		else if (setMaxHealth == 2) {
			maxHealth = 150;
			health = maxHealth;
		}
		else if (setMaxHealth == 3) {
			maxHealth = 200;
			health = maxHealth;
		}
		else if (setMaxHealth == 4) {
			maxHealth = 300;
			health = maxHealth;
		}
		else if (setMaxHealth == 5) {
			maxHealth = 500;
			health = maxHealth;
		}
	}

	public void setShield(){
		if (setMaxShield == 1) {
			maxShield = 10;
			shield = maxShield;
		}
		else if (setMaxShield == 2) {
			maxShield = 20;
			shield = maxShield;
		}
		else if (setMaxShield == 3) {
			maxShield = 35;
			shield = maxShield;
		}
		else if (setMaxShield == 4) {
			maxShield = 55;
			shield = maxShield;
		}
		else if (setMaxShield == 5) {
			maxShield = 80;
			shield = maxShield;
		}
	}


	// ---------------------------------------------------------------------------------------------------
	//Lose Health or Shield depeding on enemy attack "Tag".
	// ---------------------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		/*if (other.tag == "Boundary"){
			return;
		}*/
		//If Shield is > 0, subtract shield based on level of enemy laser.
		if (shield > 0){
			if (other.tag == "eLaser 1"){
				shield -= 10;
				if(shield <= 0){
					shield = 0;
				}
				damaged2 = true;
				Instantiate(hitYellow, transform.position, transform.rotation);
				Instantiate(shieldAbsorbSound, transform.position, transform.rotation);
				StopAllCoroutines();
				StartCoroutine (CancelRecharge());
				Destroy(other.gameObject);
			}
			else if (other.tag == "eLaser 2"){
				shield -= 20;
				if(shield <= 0){
					shield = 0;
				}
				damaged2 = true;
				Instantiate(hitOrange, transform.position, transform.rotation);
				Instantiate(shieldAbsorbSound, transform.position, transform.rotation);
				StopAllCoroutines();
				StartCoroutine (CancelRecharge());
				Destroy(other.gameObject);
			}
			else if (other.tag == "eLaser 3"){
				shield -= 30;
				if(shield <= 0){
					shield = 0;
				}
				damaged2 = true;
				Instantiate(hitRed, transform.position, transform.rotation);
				Instantiate(shieldAbsorbSound, transform.position, transform.rotation);
				StopAllCoroutines();
				StartCoroutine (CancelRecharge());
				Destroy(other.gameObject);
			}
			else if (other.tag == "eLaser 4"){
				shield -= 40;
				if(shield <= 0){
					shield = 0;
				}
				damaged2 = true;
				Instantiate(hitPurple, transform.position, transform.rotation);
				Instantiate(shieldAbsorbSound, transform.position, transform.rotation);
				StopAllCoroutines();
				StartCoroutine (CancelRecharge());
				Destroy(other.gameObject);
			}

		}

		//If Shield is <= 0, subtract health based on level of enemy laser.
		else if (shield <= 0){
			if (other.tag == "eLaser 1"){
				health -= 10;
				damaged1 = true;
				Instantiate(hitYellow, transform.position, transform.rotation);
				audioSource.Play();
				Destroy(other.gameObject);
			}
			else if (other.tag == "eLaser 2"){
				health -= 20;
				damaged1 = true;
				Instantiate(hitOrange, transform.position, transform.rotation);
				audioSource.Play();
				Destroy(other.gameObject);
			}
			else if (other.tag == "eLaser 3"){
				health -= 30;
				damaged1 = true;
				Instantiate(hitRed, transform.position, transform.rotation);
				audioSource.Play();
				Destroy(other.gameObject);
			} 
			else if (other.tag == "eLaser 4"){
				health -= 40;
				damaged1 = true;
				Instantiate(hitPurple, transform.position, transform.rotation);
				audioSource.Play();
				Destroy(other.gameObject);
			} 
		}


		//Asteroid Collision
		if (shield > 0 && other.tag == "Asteroid"){ 
			shield -= 20;
			damaged2 = true;
			audioSource.Play();
		}
		else if (shield <= 0 && other.tag == "Asteroid"){ 
			damaged1 = true;
			audioSource.Play();
			health -= 20;
		}
		if (shield > 0 && other.tag == "Enemy"){ //Enemy Attack Level 1 vs Shield
			shield -= 20;
			damaged2 = true;
			audioSource.Play();
		}
		else if (shield <= 0 && other.tag == "Enemy"){ //Enemy Attack Level 1 vs Health
			damaged1 = true;
			audioSource.Play();
			health -= 20;
		}
		else if (other.tag == "powerUp_heal") { //Heal 50 hp from PowerupHeal
			health += 50;
			Instantiate (popupHP, transform.position, transform.rotation);
			Instantiate (healSound, transform.position, transform.rotation);
			Destroy (other.gameObject);
			if (health > maxHealth) {
				health = maxHealth;
			}
		}
		else if (other.tag == "powerUp_credits"){ //Gain +5 credits
			credits += 5;
			Instantiate (popupCredits, transform.position, transform.rotation);
			Instantiate (creditsSound, transform.position, transform.rotation);
			Destroy(other.gameObject);
		}
	} 


	//If hit, cancel the shield recharge for 2 seconds.
	IEnumerator CancelRecharge(){
		cancelRecharge = true;
		yield return new WaitForSeconds(4);
		cancelRecharge = false;
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

	public int getCredits(){
		return credits;
	}
}

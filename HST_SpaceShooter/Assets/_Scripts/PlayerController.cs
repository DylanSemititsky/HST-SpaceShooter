//CONTROLS VARIOUS ASPECTS OF THE PLAYER SHIP
//MOVEMENT, TILT, HEALTH AND SHIELD
//ALLOWS BOUNDARIES TO BE SET SO THE PLAYER CANNOT MOVE OFF THE SCREEN (MANUALLY)

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

	//Damage indicator
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color (1f, 0f, 0f, 0.1f);
	public GameObject explosion;
	bool damaged;



	void Start ()
	{
		rb = GetComponent<Rigidbody>(); 			//Allow "rb" to be used for GetComponent<Rigidbody>()
	}

	void Update (){
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
	
		//Flash screen red when damaged.
		/*if(damaged){
			damageImage.color = flashColor;
		}
		else{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;*/

		//Death. Explosion when health reduced to 0.
		if (health <= 0){
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (shield >= maxShield){
			return;
		}

		//Recharge Shield. +10 every "rechargeDelay" seconds.
		if (Time.time > nextRecharge){
			nextRecharge = Time.time + rechargeDelay;
			shield += 0.1f;
		}
	}

	void FixedUpdate ()
	{
		//MOVEMENT (currently WASD)
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		//BOUNDARIES. Create impassable borders for the player ship at edge of screen, set Boundaries in Inspector
		rb.position = new Vector3 
		(
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		//TILT. Add tilt to player ship when moving from side to side
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	//Lose Health or Shield depeding on enemy attack "Tag".
	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary"){
			return;
		}
		//Lv 1 Laser hit
		if (shield > 0 && other.tag == "eLaser 1"){ //Enemy Attack Level 1 vs Shield
			shield -= 10;
			Destroy(other.gameObject);
		}
		if (shield <= 0 && other.tag == "eLaser 1"){ //Enemy Attack Level 1 vs Health
			Destroy(other.gameObject);
			damaged = true;
			health -= 10;
		}
		//Lv 2 Laser hit
		if (shield > 0 && other.tag == "eLaser 2"){ //Enemy Attack Level 2 vs Shield
			shield -= 20;
			Destroy(other.gameObject);
		}
		if (shield <= 0 && other.tag == "eLaser 2"){ //Enemy Attack Level 2 vs Health
			Destroy(other.gameObject);
			damaged = true;
			health -= 20;
		}
		//Lv 3 Laser hit
		if (shield > 0 && other.tag == "eLaser 3"){ //Enemy Attack Level 3 vs Shield
			shield -= 30;
			Destroy(other.gameObject);
		} 
		if (shield <= 0 && other.tag == "eLaser 3"){ //Enemy Attack Level 3 vs Health
			Destroy(other.gameObject);
			damaged = true;
			health -= 30;
		} 
		//Lv 4 Laser hit
		if (shield > 0 && other.tag == "eLaser 4"){ //Enemy Attack Level 4 vs Shield
			shield -= 40;
			Destroy(other.gameObject);
		}
		if (shield <= 0 && other.tag == "eLaser 4"){ //Enemy Attack Level 4 vs Health
			Destroy(other.gameObject);
			damaged = true;
			health -= 40;
		}
	} 
}

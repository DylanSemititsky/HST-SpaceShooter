using UnityEngine;
using System.Collections;

public class ShopShield : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	private int playerShieldTemp;


	void Start () {
	
		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerController = playerObject.GetComponent<PlayerController> ();
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}
	}	

	void Update () {
	}

	public void ShowShield(){
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){
		playerController.setMaxShield += 1;
		if (playerController.setMaxShield >= 5) {
			playerController.setMaxShield = 5;
		}
		playerController.setShield();
	}

	public void Revert(){
		//playerController.setMaxHealth -= 1;
		playerAttack.disableFusion = false;
	}
}
 
using UnityEngine;
using System.Collections;

public class ShopHealth : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	private int playerHealthTemp;


	void Start () {
	
		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerController = playerObject.GetComponent<PlayerController> ();
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}
	}	

	void Update () {
	}

	public void ShowHealth(){
		playerAttack.disableFusion = true;
		//playerController.setMaxHealth += 1;
	}

	public void EnableUpgrade(){
		playerController.setMaxHealth += 1;
		if (playerController.setMaxHealth >= 5) {
			playerController.setMaxHealth = 5;
		}
		playerController.setHealth();
	}

	public void Revert(){
		//playerController.setMaxHealth -= 1;
		playerAttack.disableFusion = false;
	}
}
 
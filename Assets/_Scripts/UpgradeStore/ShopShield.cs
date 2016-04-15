using UnityEngine;
using System.Collections;

public class ShopShield : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	private int playerShieldTemp;

	public GUIText currentText;
	public GUIText upgradeText;

	void Start () {
	
		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerController = playerObject.GetComponent<PlayerController> ();
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}
	}	

	void Update () {
		UpdateUpgradeText ();
	}

	public void ShowShield(){
		playerAttack.disableFusion = true;
	}


	public void EnableUpgrade(){
		if (playerController.setMaxShield == 1 && playerController.credits >= 50) {
			playerController.setMaxShield += 1;
			playerController.credits -= 50;
		} 
		else if (playerController.setMaxShield == 2 && playerController.credits >= 100) {
			playerController.setMaxShield += 1;
			playerController.credits -= 100;
		}
		else if (playerController.setMaxShield == 3 && playerController.credits >= 150) {
			playerController.setMaxShield += 1;
			playerController.credits -= 150;
		}
		else if (playerController.setMaxShield == 4 && playerController.credits >= 200) {
			playerController.setMaxShield += 1;
			playerController.credits -= 200;
		} 
		if (playerController.setMaxShield >= 5) {
			playerController.setMaxShield = 5;
		}
		playerController.setShield();
	}


	public void Revert(){
		playerAttack.disableFusion = false;
	}

	void UpdateUpgradeText(){
		if (playerController.setMaxShield == 1) {
			currentText.text = "<b>Current:</b>  <color=cyan>30hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>40hp</color> (c: 50)";
		}
		else if (playerController.setMaxShield == 2) {
			currentText.text = "<b>Current:</b>  <color=cyan>40hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>55hp</color> (c: 100)";
		}
		else if (playerController.setMaxShield == 3) {
			currentText.text = "<b>Current:</b>  <color=cyan>55hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>75hp</color> (c: 150)";
		}
		else if (playerController.setMaxShield == 4) {
			currentText.text = "<b>Current:</b>  <color=cyan>75hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>100hp</color> (c: 200)";
		}
		else if (playerController.setMaxShield == 5) {
			currentText.text = "<b>Current:</b>  <color=cyan>100hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>(maxed)</color>";
		}
	}
}
 
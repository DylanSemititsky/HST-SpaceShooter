using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopShield : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	private int playerShieldTemp;

	public Text currentText;
	public Text upgradeText;

	public AudioSource audioSource;

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
			audioSource.Play();
		} 
		else if (playerController.setMaxShield == 2 && playerController.credits >= 100) {
			playerController.setMaxShield += 1;
			playerController.credits -= 100;
			audioSource.Play();
		}
		else if (playerController.setMaxShield == 3 && playerController.credits >= 150) {
			playerController.setMaxShield += 1;
			playerController.credits -= 150;
			audioSource.Play();
		}
		else if (playerController.setMaxShield == 4 && playerController.credits >= 200) {
			playerController.setMaxShield += 1;
			playerController.credits -= 200;
			audioSource.Play();
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
			currentText.text = "<b>Current:</b>  <color=cyan>10hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>20hp</color> (c: 50)";
		}
		else if (playerController.setMaxShield == 2) {
			currentText.text = "<b>Current:</b>  <color=cyan>20hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>35hp</color> (c: 100)";
		}
		else if (playerController.setMaxShield == 3) {
			currentText.text = "<b>Current:</b>  <color=cyan>35hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>55hp</color> (c: 150)";
		}
		else if (playerController.setMaxShield == 4) {
			currentText.text = "<b>Current:</b>  <color=cyan>55hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>80hp</color> (c: 200)";
		}
		else if (playerController.setMaxShield == 5) {
			currentText.text = "<b>Current:</b>  <color=cyan>80hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=cyan>(maxed)</color>";
		}
	}
}
 
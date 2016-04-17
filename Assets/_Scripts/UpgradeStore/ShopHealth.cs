using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopHealth : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	private int playerHealthTemp;

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

	public void ShowHealth(){
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){

		audioSource.Play();

		if (playerController.setMaxHealth == 1 && playerController.credits >= 50) {
			playerController.setMaxHealth += 1;
			playerController.credits -= 50;
		} 
		else if (playerController.setMaxHealth == 2 && playerController.credits >= 100) {
			playerController.setMaxHealth += 1;
			playerController.credits -= 100;
		}
		else if (playerController.setMaxHealth == 3 && playerController.credits >= 150) {
			playerController.setMaxHealth += 1;
			playerController.credits -= 150;
		}
		else if (playerController.setMaxHealth == 4 && playerController.credits >= 200) {
			playerController.setMaxHealth += 1;
			playerController.credits -= 200;
		}
		if (playerController.setMaxHealth >= 5) {
			playerController.setMaxHealth = 5;
		}
		playerController.setHealth();
	}

	public void Revert(){
		playerAttack.disableFusion = false;
	}

	void UpdateUpgradeText(){
		if (playerController.setMaxHealth == 1) {
			currentText.text = "<b>Current:</b>  <color=green>100hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=green>150hp</color> (c: 50)";
		}
		else if (playerController.setMaxHealth == 2) {
			currentText.text = "<b>Current:</b>  <color=green>150hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=green>200hp</color> (c: 100)";
		}
		else if (playerController.setMaxHealth == 3) {
			currentText.text = "<b>Current:</b>  <color=green>200hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=green>300hp</color> (c: 150)";
		}
		else if (playerController.setMaxHealth == 4) {
			currentText.text = "<b>Current:</b>  <color=green>300hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=green>500hp</color> (c: 200)";
		}
		else if (playerController.setMaxHealth == 5) {
			currentText.text = "<b>Current:</b>  <color=green>500hp</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=green>(maxed)</color>";
		}
	}
}
 
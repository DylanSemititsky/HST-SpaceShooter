using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopWingCannon : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	private int playerMainCannonTemp;
	private int playerMultiCannonTemp;

	public Text currentText;
	public Text upgradeText;

	public AudioSource audioSource;

	void Start () {
	
		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
			playerController = playerObject.GetComponent<PlayerController> ();
		}

		playerMainCannonTemp = playerAttack.primaryAttack.setPrimaryAttackLevel;
		playerMultiCannonTemp = playerAttack.multiAttack.setMultiAttackLevel;
	}	

	void Update () {
		UpdateUpgradeText ();
	}

	public void ShowMultiAttack(){
		playerMainCannonTemp = playerAttack.primaryAttack.setPrimaryAttackLevel;
		playerAttack.multiAttack.setMultiAttackLevel += 1;
		playerAttack.primaryAttack.setPrimaryAttackLevel = 0;
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){

		if (playerMultiCannonTemp == 0 && playerController.credits >= 100) {
			playerAttack.multiAttack.setMultiAttackLevel += 1;
			playerMultiCannonTemp += 1;
			playerController.credits -= 100;
			audioSource.Play();
		}
		else if (playerMultiCannonTemp == 1 && playerController.credits >= 200) {
			playerAttack.multiAttack.setMultiAttackLevel += 1;
			playerMultiCannonTemp += 1;
			playerController.credits -= 200;
			audioSource.Play();
		}
		else if (playerMultiCannonTemp == 2 && playerController.credits >= 400) {
			playerAttack.multiAttack.setMultiAttackLevel += 1;
			playerMultiCannonTemp += 1;
			playerController.credits -= 400;
			audioSource.Play();
		}
		else if (playerMultiCannonTemp >= 3) {
			playerMultiCannonTemp = 3;
		}
	}

	public void Revert(){
		playerAttack.primaryAttack.setPrimaryAttackLevel = playerMainCannonTemp;
		playerAttack.multiAttack.setMultiAttackLevel = playerMultiCannonTemp;
		playerAttack.disableFusion = false;
	}

	void UpdateUpgradeText(){
		if (playerMultiCannonTemp == 0) {
			currentText.text = "<b>Current:</b>  --";
			upgradeText.text = "<b>Upgrade:</b>  <color=yellow>10 dmg</color> (c: 100)";
		}
		else if (playerMultiCannonTemp == 1) {
			currentText.text = "<b>Current:</b>  <color=yellow>10 dmg</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=orange>20 dmg</color> (c: 200)";
		}
		else if (playerMultiCannonTemp == 2) {
			currentText.text = "<b>Current:</b>  <color=orange>20 dmg</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=red>30 dmg</color> (c: 400)";;
		}
		else if (playerMultiCannonTemp == 3) {
			currentText.text = "<b>Current:</b>  <color=red>30 dmg</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=red>(maxed)</color>";
		}
	}
}

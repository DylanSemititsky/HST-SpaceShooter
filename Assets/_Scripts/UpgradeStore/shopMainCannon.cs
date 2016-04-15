using UnityEngine;
using System.Collections;

public class ShopMainCannon : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	private int playerMainCannonTemp;
	private int playerMultiCannonTemp;

	public GUIText currentText;
	public GUIText upgradeText;

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

	public void ShowPrimaryAttack(){
		playerMultiCannonTemp = playerAttack.multiAttack.setMultiAttackLevel;
		playerAttack.multiAttack.setMultiAttackLevel = 0;
		playerAttack.primaryAttack.setPrimaryAttackLevel += 1;
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){
		if (playerMainCannonTemp == 1 && playerController.credits >= 50) {
			playerAttack.primaryAttack.setPrimaryAttackLevel += 1;
			playerMainCannonTemp += 1;
			playerController.credits -= 50;
		}
		else if (playerMainCannonTemp == 2 && playerController.credits >= 100) {
			playerAttack.primaryAttack.setPrimaryAttackLevel += 1;
			playerMainCannonTemp += 1;
			playerController.credits -= 100;
		}
		else if (playerMainCannonTemp == 3 && playerController.credits >= 200) {
			playerAttack.primaryAttack.setPrimaryAttackLevel += 1;
			playerMainCannonTemp += 1;
			playerController.credits -= 200;
		}
		else if (playerMainCannonTemp >= 4) {
			playerMainCannonTemp = 4;
		}
	}

	public void Revert(){
		playerAttack.primaryAttack.setPrimaryAttackLevel = playerMainCannonTemp;
		playerAttack.multiAttack.setMultiAttackLevel = playerMultiCannonTemp;
		playerAttack.disableFusion = false;
	}

	void UpdateUpgradeText(){
		if (playerMainCannonTemp == 1) {
			currentText.text = "<b>Current:</b>  <color=yellow>10 dmg</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=orange>20 dmg</color> (c: 50)";
		}
		else if (playerMainCannonTemp == 2) {
			currentText.text = "<b>Current:</b>  <color=orange>20 dmg</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=red>30 dmg</color> (c: 100)";;
		}
		else if (playerMainCannonTemp == 3) {
			currentText.text = "<b>Current:</b>  <color=red>30 dmg</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=magenta>40 dmg</color> (c: 200)";
		}
		else if (playerMainCannonTemp == 4) {
			currentText.text = "<b>Current:</b>  <color=magenta>40 dmg</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=magenta>(maxed)</color>";
		}
	}
}

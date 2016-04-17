﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopFusionBlast : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	FlashWhenFullFusion flashWhenFullFusion;

	public Text currentText;
	public Text upgradeText;

	public AudioSource audioSource;

	void Start () {

		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
			playerController = playerObject.GetComponent<PlayerController> ();
		}

		GameObject flashObject = GameObject.Find ("Canvas_yellow1");	
		if (flashObject != null) {
			flashWhenFullFusion = flashObject.GetComponent<FlashWhenFullFusion> ();
		}
	}

	void Update () {
		UpdateUpgradeText ();
	}

	public void ShowFusion(){
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){

		flashWhenFullFusion.StartFlash();

		audioSource.Play();
		 
		if (playerAttack.fusionAttack.setFusionAttackLevel == 0 && playerController.credits >= 100) {
			playerAttack.fusionAttack.setFusionAttackLevel += 1;
			playerController.credits -= 100;
		}
		else if (playerAttack.fusionAttack.setFusionAttackLevel == 1 && playerController.credits >= 200) {
			playerAttack.fusionAttack.setFusionAttackLevel += 1;
			playerController.credits -= 200;
		} 
		if (playerAttack.fusionAttack.setFusionAttackLevel >= 2) {
			playerAttack.fusionAttack.setFusionAttackLevel = 2;
		}
	}


	public void Revert(){
		playerAttack.disableFusion = false;
	}

	void UpdateUpgradeText(){
		if (playerAttack.fusionAttack.setFusionAttackLevel == 0) {
			currentText.text = "<b>Current:</b>  --";
			upgradeText.text = "<b>Upgrade:</b>  Single Blast (c: 100)";
		}
		else if (playerAttack.fusionAttack.setFusionAttackLevel == 1) {
			currentText.text = "<b>Current:</b>  Single Blast";
			upgradeText.text = "<b>Upgrade:</b>  Double Blast (c: 200)";
		}
		else if (playerAttack.fusionAttack.setFusionAttackLevel == 2) {
			currentText.text = "<b>Current:</b>  Double Blast";
			upgradeText.text = "<b>Upgrade:</b>  (maxed)";
		}
	}
}
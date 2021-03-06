﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopFusionBlast : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	FlashWhenFullFusion flashWhenFullFusion;
	CreditsFlash creditsFlash;

	public GameObject fusionIcon;
	public Image myImageComponent;
	public Sprite yellowIcon;
	public Sprite orangeIcon;
	public Sprite redIcon;
	public Sprite purpleIcon;

	public Text currentText;
	public Text upgradeText;

	public AudioSource audioSource;

	void Start () {

		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
			playerController = playerObject.GetComponent<PlayerController> ();
		}

		GameObject creditsFlashObject = GameObject.Find ("UI/Canvas_DisplayText/InGameCredits");	
		if (creditsFlashObject != null) {
			creditsFlash = creditsFlashObject.GetComponent<CreditsFlash> ();
		}

		GameObject flashObject = GameObject.Find ("Canvas_yellow1");	
		if (flashObject != null) {
			flashWhenFullFusion = flashObject.GetComponent<FlashWhenFullFusion> ();
		}

		myImageComponent = fusionIcon.GetComponent<Image> ();
	}

	void Update () {
		UpdateUpgradeText ();

		CheckFusionLevel ();
	}

	public void ShowFusion(){
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){

		flashWhenFullFusion.StartFlash();

		 
		if (playerAttack.fusionAttack.setFusionAttackLevel == 0 && playerController.credits >= 100) {
			playerAttack.fusionAttack.setFusionAttackLevel += 1;
			playerController.credits -= 200;
			audioSource.Play();
		}
		/*else if (playerAttack.fusionAttack.setFusionAttackLevel == 1 && playerController.credits >= 200) {
			playerAttack.fusionAttack.setFusionAttackLevel += 1;
			playerController.credits -= 200;
			audioSource.Play();
		} */
		if (playerAttack.fusionAttack.setFusionAttackLevel >= 1) {
			playerAttack.fusionAttack.setFusionAttackLevel = 1;
		}

		creditsFlash.StartFlash();
	}


	public void Revert(){
		playerAttack.disableFusion = false;
	}

	void UpdateUpgradeText(){
		if (playerAttack.fusionAttack.setFusionAttackLevel == 0) {
			currentText.text = "<b>Current:</b>  --";
			upgradeText.text = "<b>Upgrade:</b>  Fusion Blast (c: 200)";
		}
		/*else if (playerAttack.fusionAttack.setFusionAttackLevel == 1) {
			currentText.text = "<b>Current:</b>  Single Blast";
			upgradeText.text = "<b>Upgrade:</b>  Double Blast (c: 200)";
		}*/
		else if (playerAttack.fusionAttack.setFusionAttackLevel == 1) {
			currentText.text = "<b>Current:</b>  Fusion Blast";
			upgradeText.text = "<b>Upgrade:</b>  (maxed)";
		}
	}


	void CheckFusionLevel(){
		if (playerAttack.fusionAttack.fusionAttackDamage == 1 /*|| playerAttack.fusionAttack.fusionAttackDamage == 2*/) {
			SetYellowIcon ();
		}
		else if (playerAttack.fusionAttack.fusionAttackDamage == 2 /*|| playerAttack.fusionAttack.fusionAttackDamage == 4*/) {
			SetOrangeIcon ();
		}
		else if (playerAttack.fusionAttack.fusionAttackDamage == 3 /*|| playerAttack.fusionAttack.fusionAttackDamage == 6*/) {
			SetRedIcon ();
		}
		else if (playerAttack.fusionAttack.fusionAttackDamage == 4) {
			SetPurpleIcon ();
		}
	}


	public void SetYellowIcon(){
		myImageComponent.sprite = yellowIcon;
	}
	public void SetOrangeIcon(){
		myImageComponent.sprite = orangeIcon;
	}
	public void SetRedIcon(){
		myImageComponent.sprite = redIcon;
	}
	public void SetPurpleIcon(){
		myImageComponent.sprite = purpleIcon;
	}
}

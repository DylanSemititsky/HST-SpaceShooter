﻿using UnityEngine;
using System.Collections;

public class FlashWhenFullFusion : MonoBehaviour {

	PlayerAttack playerAttack;

	// Use this for initialization
	void Start () {

		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (playerAttack.fusionAttack.fusion >= 99 && playerAttack.fusionAttack.fusion < 100){
			StartCoroutine(Flash());
		}
		if (playerAttack.fusionAttack.fusion < 99){
			ResetAlpha();
		}
	
	}

	IEnumerator Flash(){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

		for(int i = 1; i <= 10; i++){
		yield return new WaitForSeconds(0.1f);
		canvasGroup.alpha = 0;
		yield return new WaitForSeconds(0.1f);
		canvasGroup.alpha = 1;
		}
	}

	void ResetAlpha(){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

		canvasGroup.alpha = 0;
	}
}

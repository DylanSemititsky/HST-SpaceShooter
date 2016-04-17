using UnityEngine;
using System.Collections;

public class FlashWhenFullFusion : MonoBehaviour {

	PlayerAttack playerAttack;

	void Start () {
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}
	}

	void Update () {

		if(playerAttack.fusionAttack.setFusionAttackLevel > 0){
			
			if (playerAttack.fusionAttack.fusion > 98.9 && playerAttack.fusionAttack.fusion < 100){
				StartCoroutine(Flash());
			}
			if (playerAttack.fusionAttack.fusion < 99){
				HideAlpha();
			}
		} 
		else if (playerAttack.fusionAttack.setFusionAttackLevel == 0){
			HideAlpha();
		}
		else HideAlpha();
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

	void HideAlpha(){
			CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0;
	}

	public void StartFlash(){
		StartCoroutine(Flash());
	}
}

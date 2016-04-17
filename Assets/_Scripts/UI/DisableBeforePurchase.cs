using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisableBeforePurchase : MonoBehaviour {

	PlayerAttack playerAttack;

	void Start () {

		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

		if(playerAttack.fusionAttack.setFusionAttackLevel == 0){
			canvasGroup.alpha = 0;
		}
	}
}

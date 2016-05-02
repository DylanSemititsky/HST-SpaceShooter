using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashWhenFullFusion : MonoBehaviour {

	PlayerAttack playerAttack;

	public Image myImageComponent;
	public Sprite yellowIcon;
	public Sprite orangeIcon;
	public Sprite redIcon;
	public Sprite purpleIcon;



	void Start () {
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}
	}

	void Update () {

		CheckFusionLevel ();

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

	void CheckFusionLevel(){
		if (playerAttack.fusionAttack.fusionAttackDamage == 1 && playerAttack.fusionAttack.setFusionAttackLevel > 0) {
			SetYellowIcon ();
		}
		else if (playerAttack.fusionAttack.fusionAttackDamage == 2 && playerAttack.fusionAttack.setFusionAttackLevel > 0) {
			SetOrangeIcon ();
		}
		else if (playerAttack.fusionAttack.fusionAttackDamage == 3 && playerAttack.fusionAttack.setFusionAttackLevel > 0) {
			SetRedIcon ();
		}
		else if (playerAttack.fusionAttack.fusionAttackDamage == 4 && playerAttack.fusionAttack.setFusionAttackLevel > 0) {
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

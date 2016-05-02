using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashWhenFullBomb : MonoBehaviour {

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

		CheckBombLevel ();

		if(playerAttack.bombAttack.setBombLevel > 0){

			if (playerAttack.bombAttack.bomb > 98.9 && playerAttack.bombAttack.bomb < 100){
				StartCoroutine(Flash());
			}
			if (playerAttack.bombAttack.bomb < 99){
				HideAlpha();
			}
		}
		else if (playerAttack.bombAttack.setBombLevel == 0){
			HideAlpha();
		}
		else if (playerAttack.bombAttack.setBombLevel > 0 && playerAttack.bombAttack.bomb >= 100){
			ShowAlpha();
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

	void HideAlpha(){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
	}

	void ShowAlpha(){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
	}

	public void StartFlash(){
		StartCoroutine(Flash());
	}


	void CheckBombLevel(){
		if (playerAttack.bombAttack.setBombLevel == 1 /*|| playerAttack.fusionAttack.fusionAttackDamage == 2*/) {
			SetYellowIcon ();
		}
		else if (playerAttack.bombAttack.setBombLevel == 2 /*|| playerAttack.fusionAttack.fusionAttackDamage == 4*/) {
			SetOrangeIcon ();
		}
		else if (playerAttack.bombAttack.setBombLevel == 3 /*|| playerAttack.fusionAttack.fusionAttackDamage == 6*/) {
			SetRedIcon ();
		}
		else if (playerAttack.bombAttack.setBombLevel == 4) {
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

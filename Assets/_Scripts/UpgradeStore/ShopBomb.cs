using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopBomb : MonoBehaviour {

	PlayerAttack playerAttack;
	PlayerController playerController;
	FlashWhenFullBomb flashWhenFullBomb;
	CreditsFlash creditsFlash;

	public GameObject bombIcon;
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

		GameObject flashObject = GameObject.Find ("Canvas_yellow2");	
		if (flashObject != null) {
			flashWhenFullBomb = flashObject.GetComponent<FlashWhenFullBomb> ();
		}

		myImageComponent = bombIcon.GetComponent<Image> ();

	}

	void Update () {
		UpdateUpgradeText ();
	}

	public void ShowBomb(){
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){

		flashWhenFullBomb.StartFlash();



		if (playerAttack.bombAttack.setBombLevel == 0 && playerController.credits >= 50) {
			playerAttack.bombAttack.setBombLevel += 1;
			playerController.credits -= 50;
			audioSource.Play();
		}
		else if (playerAttack.bombAttack.setBombLevel == 1 && playerController.credits >= 100) {
			playerAttack.bombAttack.setBombLevel += 1;
			playerController.credits -= 100;
			audioSource.Play();

			SetOrangeIcon ();
		}
		else if (playerAttack.bombAttack.setBombLevel == 2 && playerController.credits >= 150) {
			playerAttack.bombAttack.setBombLevel += 1;
			playerController.credits -= 150;
			audioSource.Play();

			SetRedIcon ();
		}
		else if (playerAttack.bombAttack.setBombLevel == 3 && playerController.credits >= 200) {
			playerAttack.bombAttack.setBombLevel += 1;
			playerController.credits -= 200;
			audioSource.Play();

			SetPurpleIcon ();
		}

		creditsFlash.StartFlash();

		if (playerAttack.bombAttack.setBombLevel >= 4) {
			playerAttack.bombAttack.setBombLevel = 4;
		}
	}

	public void Revert(){
		playerAttack.disableFusion = false;
	}

	void UpdateUpgradeText(){
		if (playerAttack.bombAttack.setBombLevel == 0) {
			currentText.text = "<b>Current:</b>  --";
			upgradeText.text = "<b>Upgrade:</b>  <color=yellow>85 dmg/3 secs</color> (c: 50)";
		}
		else if (playerAttack.bombAttack.setBombLevel == 1) {
			currentText.text = "<b>Current:</b>  <color=yellow>85 dmg/3 secs</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=orange>170 dmg/3 secs</color> (c: 100)";
		}
		else if (playerAttack.bombAttack.setBombLevel == 2) {
			currentText.text = "<b>Current:</b>  <color=orange>170 dmg/3 secs</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=red>255 dmg/3 secs</color> (c: 150)";
		}
		else if (playerAttack.bombAttack.setBombLevel == 3) {
			currentText.text = "<b>Current:</b>  <color=red>255 dmg/3 secs</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=magenta>340 dmg/3 secs</color> (c: 200)";
		}
		else if (playerAttack.bombAttack.setBombLevel == 4) {
			currentText.text = "<b>Current:</b>  <color=magenta>340 dmg/3 secs</color>";
			upgradeText.text = "<b>Upgrade:</b>  <color=magenta>(maxed)</color>";
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

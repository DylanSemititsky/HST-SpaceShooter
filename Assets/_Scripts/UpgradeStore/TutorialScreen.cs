using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScreen : MonoBehaviour {

	GameState gameState;
	bool tutorialFinished;
	GameObject playerShip;
	public Button continueButton;
	public GameObject upgradeButtons;

	void Start () {

		//Find GameState Object to access it's script
		GameObject gameStateObject = GameObject.Find ("GameState");	
		if (gameStateObject != null) {
			gameState = gameStateObject.GetComponent<GameState> ();
		}
		//Set bool to gameState's stored setting
		tutorialFinished = gameState.getTutuorialFinished();

		if(!tutorialFinished){
		GameObject playerShip = GameObject.Find ("Player");
		//playerShip.SetActive(false);
		playerShip.transform.position = new Vector3 (3, 0, -1.7f);
		}

		if(!tutorialFinished){

		upgradeButtons.SetActive(false);
		continueButton.interactable = false;
		} 
	}
	

	void Update () {
		if (tutorialFinished){
			gameObject.SetActive(false);
		}
	}

	public bool getTutorialFinished(){
		return tutorialFinished;
	}

	public void okButton(){
		//playerShip.SetActive(true);
		GameObject playerShip = GameObject.Find ("Player");
		playerShip.transform.position = new Vector3 (3, 0, 0);
		gameState.makeTutorialTrue();
		tutorialFinished = true;
		continueButton.interactable = true;
		upgradeButtons.SetActive(true);
	}
}

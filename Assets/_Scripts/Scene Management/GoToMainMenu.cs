// ---------------------------------------------------------------------------------------------------
// When hitting the Start button on Main Menu this script calls the GameState.startState
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
	ChangeShipColor changeShipColor;
	SceneFade sceneFade;
	GameObject gameState;

	public bool start = false;
	public GameObject fadeToBlack;


	public void StartFlash(){
		//Find GameState Object to access it's script
		GameObject gameStateObject = GameObject.Find ("GameState");	
		if (gameStateObject != null) {
			gameState = gameStateObject;
		}

		Destroy (gameState);

		StartCoroutine (Flash ());
	}


	private IEnumerator Flash(){

		fadeToBlack.SetActive (true);
		FadeActivate ();

		for(int i = 0; i <= 8; i++){
			GetComponent<Image> ().color = Color.white;
			yield return new WaitForSeconds(0.1f);
			GetComponent<Image> ().color = Color.green;
			yield return new WaitForSeconds(0.1f);
		}
		SceneManager.LoadScene ("Main Menu");
	}

	public void FadeActivate(){
		sceneFade = fadeToBlack.GetComponent<SceneFade> ();
		sceneFade.fadeActivate = true;
	}
}
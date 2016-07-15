using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMainMenuScript : MonoBehaviour {

	GameObject gameState;

	public bool start = false;

	public void ReturnToMainMenu(){
		//Find GameState Object to access it's script
		GameObject gameStateObject = GameObject.Find ("GameState");	
		if (gameStateObject != null) {
			gameState = gameStateObject;
		}

		Destroy (gameState);
		Time.timeScale = 1f; 
		SceneManager.LoadScene (0);
	}
}

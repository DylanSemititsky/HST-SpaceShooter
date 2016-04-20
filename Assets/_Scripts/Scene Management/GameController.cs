// ---------------------------------------------------------------------------------------------------
//
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	GameState gameState;
	SceneFade sceneFade;
	public GameObject fadeToBlack;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text levelCompleteText;

	private bool levelComplete;
	private bool gameOver;
	private bool restart;
	private int score;


	// ---------------------------------------------------------------------------------------------------
	// START
	// ---------------------------------------------------------------------------------------------------
	void Start (){
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		levelCompleteText.text = "";
		score = 0;
		UpdateScore ();

		//Find GameState Object to access it's script
		GameObject gameStateObject = GameObject.Find ("GameState");	
		if (gameStateObject != null) {
			gameState = gameStateObject.GetComponent<GameState> ();
		}
	}

	// ---------------------------------------------------------------------------------------------------
	// UPDATE
	// ---------------------------------------------------------------------------------------------------
	void Update (){
		if (restart){
			if (Input.GetMouseButton(0)){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}


	// ---------------------------------------------------------------------------------------------------
	// Restart text "if" gameOver condition
	// ---------------------------------------------------------------------------------------------------
	void Restart (){
		
		if (gameOver){
			restartText.text = "Click to restart level";
			restart = true;
		}

	}

	public void GameOver (){
		gameOverText.text = "You Died!";
		gameOver = true;
		Restart ();
	}

	// ---------------------------------------------------------------------------------------------------
	// Add score whenever an enemy with a newScoreValue is killed, then Update Score
	// ---------------------------------------------------------------------------------------------------
	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore (){
		scoreText.text = "Score: " + score;
	}


	// ---------------------------------------------------------------------------------------------------
	// When LevelComplete is called, begin FadeActivate and return to Main Menu
	// ---------------------------------------------------------------------------------------------------
	private IEnumerator CoLevelComplete(){
		levelCompleteText.text = "Level Complete!";
		yield return new WaitForSeconds (2);
		FadeActivate ();
		yield return new WaitForSeconds (3);
		Debug.Log ("About to load scene");
		gameState.StoreVariables ();
		SceneManager.LoadScene ("UpgradeShop");
	}

	public void LevelComplete(){
		StartCoroutine (CoLevelComplete());
	}

	public void FadeActivate(){
		Debug.Log ("FadeActivate");
		sceneFade = fadeToBlack.GetComponent<SceneFade> ();
		sceneFade.fadeActivate = true;
	}
}


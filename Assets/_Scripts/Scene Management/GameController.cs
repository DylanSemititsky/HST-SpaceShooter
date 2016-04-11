// ---------------------------------------------------------------------------------------------------
//
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	SceneFade sceneFade;
	public GameObject fadeToBlack;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	//public GUIText levelCompleteText;

	//private bool levelComplete;
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
		//levelCompleteText.text = "";
		score = 0;
		UpdateScore ();
	}

	// ---------------------------------------------------------------------------------------------------
	// UPDATE
	// ---------------------------------------------------------------------------------------------------
	void Update (){
		if (restart){
			if (Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene("Main Menu");
			}
		}

		if (Input.GetKeyDown(KeyCode.U)){
			SceneManager.LoadScene("UpgradeShop");
		}
	
	}

	// ---------------------------------------------------------------------------------------------------
	// Restart text "if" gameOver condition
	// ---------------------------------------------------------------------------------------------------
	void Restart (){
		
		if (gameOver){
			restartText.text = "Press 'R' to return to Main Menu";
			restart = true;
		}

	}

	public void GameOver (){
		gameOverText.text = "GAME OVER";
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
		//levelCompleteText.text = "Level Complete!";
		yield return new WaitForSeconds (2);
		FadeActivate ();
		yield return new WaitForSeconds (3);
		Debug.Log ("About to load scene");
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


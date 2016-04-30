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
	CanvasGroup canvasGroup;

	public GameObject fadeToBlack;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text levelCompleteText;

	private bool levelComplete;
	private bool gameOver;
	[HideInInspector]
	public  bool restart;
	private int score;

	public GameObject pauseSound;
	public Image pauseImage;
	private bool isPausing;

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

		//Find Pause Canvas and set alpha to 0.
		GameObject pauseObject = GameObject.Find ("Canvas_Pause");	
		if (pauseObject != null) {
			canvasGroup = pauseObject.GetComponent<CanvasGroup> ();
			canvasGroup.alpha = 0;
		}

		GameObject fadeObject = GameObject.Find ("Canvas_FadeToBlack");	
		if (fadeObject != null) {
			sceneFade = fadeToBlack.GetComponent<SceneFade> ();
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

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (isPausing == false) 
			{
				Time.timeScale = 0f;
				isPausing = true;
				canvasGroup.alpha = 1;
				Instantiate (pauseSound, transform.position, transform.rotation);
			} 

			else 

			{
				Time.timeScale = 1f; 
				isPausing = false;
				canvasGroup.alpha = 0;
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
		gameState.StoreVariables ();
		SceneManager.LoadScene ("UpgradeShop");
	}

	public void LevelComplete(){
		StartCoroutine (CoLevelComplete());
	}

	public void FadeActivate(){
		fadeToBlack.SetActive (true);
		sceneFade = fadeToBlack.GetComponent<SceneFade> ();
		sceneFade.fadeActivate = true;
		print (sceneFade.fadeActivate);
	}
}


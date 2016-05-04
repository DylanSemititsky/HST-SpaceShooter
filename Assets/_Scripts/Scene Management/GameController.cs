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
	RedMove redMove;

	public GameObject fadeToBlack;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text levelCompleteText;
	public Text livesRemaining;

	public int extraLives;

	private bool levelComplete;
	private bool gameOver;
	[HideInInspector]
	public  bool restart;
	public int score;

	public GameObject pauseSound;
	public Image pauseImage;
	private bool isPausing;

	public bool lastLevel;
	public bool hardMode;

	// ---------------------------------------------------------------------------------------------------
	// START
	// ---------------------------------------------------------------------------------------------------
	void Start (){

		//Debug.Log ("score = " + score);
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		levelCompleteText.text = "";
		livesRemaining.text = ""; 

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

		GameObject redMoveObject = GameObject.Find ("Terrain");	
		if (redMoveObject != null) {
			redMove = redMoveObject.GetComponent<RedMove> ();
		}

		extraLives = gameState.getExtraLives ();
		//Debug.Log ("start: " + extraLives);

		score = gameState.getScore();

		RedMove.oneTime = false;

		UpdateScore ();
	}

	// ---------------------------------------------------------------------------------------------------
	// UPDATE
	// ---------------------------------------------------------------------------------------------------
	void Update (){
		if (restart) {
			
			if (Input.GetMouseButton (0)) {
				if (extraLives > 0) {
					RedMove.oneTime = false;
					gameState.updateExtraLives ();
					SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
				} else {
					gameState.StoreVariables ();
					SceneManager.LoadScene ("HighScores");
				}
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
			if (extraLives > 0) {
				restartText.text = "Click to restart level";
				restart = true;
			} else {
				restartText.text = "Click to Continue";
				restart = true;
			}
		}

	}

	public void GameOver (){
		if (extraLives > 0) {
			gameOverText.text = "You Died!";
			livesRemaining.text = "x" + extraLives + " Lives Remaining!"; 
			gameOver = true;
			Restart ();
		} else {
			gameOverText.text = "Game Over!";
			gameOver = true;
			Restart ();
		}
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
	}

	public int getScore(){
		return score;
	}


	public IEnumerator CoEndGame(){
		levelCompleteText.text = "Level Complete!";
		yield return new WaitForSeconds (2);
		FadeActivate ();
		yield return new WaitForSeconds (3);
		gameState.StoreVariables ();
		SceneManager.LoadScene ("HighScores");	
	}

	public void EndGame(){
		StartCoroutine (CoEndGame ());
	}

	public int getExtraLives(){
		return extraLives;
	}

	public void CheckLevel(){
		if (lastLevel) {
			EndGame ();
		} else
			LevelComplete ();
	}
}


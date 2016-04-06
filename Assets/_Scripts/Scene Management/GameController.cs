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
	public GUIText levelCompleteText;
	public GUIText thankYouForPlaying;

	private bool levelComplete;
	private bool gameOver;
	private bool restart;
	private int score;

	void Start (){
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		levelCompleteText.text = "";
		thankYouForPlaying.text = "";
		score = 0;
		UpdateScore ();
	}

	void Update (){
		if (restart){
			if (Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene("Main Menu");
			}
		}

		if (levelComplete) {
			LevelComplete ();
		}
	}

	void Restart (){
		
		if (gameOver){
			restartText.text = "Press 'R' to return to Main Menu";
			restart = true;
		}

	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore (){
		scoreText.text = "Score: " + score;
	}

	public void GameOver (){
		gameOverText.text = "GAME OVER";
		gameOver = true;
		Restart ();
	}

	private IEnumerator CoLevelComplete(){
		levelCompleteText.text = "Level Complete!";
		thankYouForPlaying.text = "Thank You for Playing!";
		yield return new WaitForSeconds (2);
		FadeActivate ();
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Main Menu");
	}

	public void LevelComplete(){
		StartCoroutine (CoLevelComplete());
	}

	public void FadeActivate(){
		sceneFade = fadeToBlack.GetComponent<SceneFade> ();
		sceneFade.fadeActivate = true;
	}
}


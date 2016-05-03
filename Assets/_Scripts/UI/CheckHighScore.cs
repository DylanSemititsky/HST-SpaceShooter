using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckHighScore : MonoBehaviour {


	GameController gameController;
	int score;
	public InputField playerName;


	// Use this for initialization
	void Start () {
		
		score = GetComponent<GameController> ().score;

	}
	
	public void InitialsEntered(){
		GetComponent<HighScores> ().CheckForHighScore (score, playerName.text);
	}
}

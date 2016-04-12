using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnToTheNext : MonoBehaviour 
{
	static int addScene = 2;

	PlayerController playerController;
	PlayerAttack playerAttack;
	GameState gameState;

	private float playerSpeedTemp;

	// Use this for initialization
	void Start () 
	{
		addScene++;	

		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerController = playerObject.GetComponent<PlayerController> ();
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}

		GameObject gameStateObject = GameObject.Find ("GameState");
		if (gameStateObject != null) {
			gameState = gameStateObject.GetComponent<GameState>();
		}

		playerSpeedTemp = playerController.speed;
		playerController.speed = 0;

	}

	public void LoadNext (){
		playerController.speed = playerSpeedTemp;
		gameState.StoreVariables ();
		SceneManager.LoadScene (addScene);
	}
}

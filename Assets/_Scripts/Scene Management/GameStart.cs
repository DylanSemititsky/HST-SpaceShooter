// ---------------------------------------------------------------------------------------------------
// When hitting the Start button on Main Menu this script calls the GameState.startState
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
 
public class GameStart : MonoBehaviour
{
	StartGameObject startGame;

	// ---------------------------------------------------------------------------------------------------
	// START
	// ---------------------------------------------------------------------------------------------------
	void Start(){

		//Find Start button object in scene to access it's scripts
		GameObject startObject = GameObject.Find ("Start");	
		if (startObject != null) {
			startGame = startObject.GetComponent<StartGameObject> ();
		}
	}

	// ---------------------------------------------------------------------------------------------------
	// If start == true in the startGame script, call beginGame()
	// triggers the startState() in the GameState script
	// ---------------------------------------------------------------------------------------------------
    void OnGUI (){
    	if (startGame.start == true){	
        	beginGame();
        }
    }
   
    private void beginGame(){
           
        DontDestroyOnLoad(GameState.Instance);
        GameState.Instance.startState();       
    }
}
using UnityEngine;
using System.Collections;
 
public class GameStart : MonoBehaviour
{
 		StartGame startGame;

		void Start(){
			GameObject startObject = GameObject.Find ("Start");	
			if (startObject != null) {
				startGame = startObject.GetComponent<StartGame> ();
			}
		}

        void OnGUI (){
        	if (startGame.start == true){
            	Debug.Log("Start = true");	
            	beginGame();
            }
        }
       
        private void beginGame(){
            print("Starting game");
               
            DontDestroyOnLoad(GameState.Instance);
            GameState.Instance.startState();       
        }
}
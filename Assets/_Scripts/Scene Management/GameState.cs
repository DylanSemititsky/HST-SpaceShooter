// ---------------------------------------------------------------------------------------------------
// Holds all player variables in a GameState Instance object.
// Sets starting variables and stores saved variables just before any scene is loaded
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
       
        // Player Variables 
        private static GameState instance;
        private string activeLevel;                     			// Active level
        private int setMaxHealth;                             		// Max HP
		private int setMaxShield;                                  	// Max shield
        private int setPrimaryAttackLevel;                      	// Primary Attack Level
        private int setMultiAttackLevel ;                        	// Multi Attack level
		private int setFusionAttackLevel;							// Fusion Attack level
		private int setBombAttackLevel;								// Bomb Attack level
		private float fireRate;                            			// Fire Rate
        private int credits;                                 		// credits
		private int score;											// score
		private int extraLives;										//extra lives
		private bool setTutorialFinished;

		
		PlayerController playerController;
        PlayerAttack playerAttack;
		GameController gameController;
		TutorialScreen tutorialScreen;
       
       
       
        // ---------------------------------------------------------------------------------------------------
        // gamestate()
        // ---------------------------------------------------------------------------------------------------
        // Creates an instance of gamestate as a gameobject if an instance does not exist
        // ---------------------------------------------------------------------------------------------------
        public static GameState Instance{
            get{
                if(instance == null){
                    instance = new GameObject("GameState").AddComponent<GameState> ();
                }

                return instance;
            }
        }      
       
        // Sets the instance to null when the application quits
        public void OnApplicationQuit(){
                instance = null;
        }
		
        // ---------------------------------------------------------------------------------------------------
        // startState()
        // ---------------------------------------------------------------------------------------------------
        // Creates a new game state
        // ---------------------------------------------------------------------------------------------------
        public void startState(){
                print ("Creating a new game state");
                
                // Set default properties:
                activeLevel = "Level 1";
                setMaxHealth = 1;
                setMaxShield = 1;
                setPrimaryAttackLevel = 1;
                setMultiAttackLevel = 0;
				setFusionAttackLevel = 0;
				setBombAttackLevel = 0;
                fireRate = 0.25f;
                credits = 200;
				score = 0;
				extraLives = 3;
				OnToTheNext.addScene = 3;
				setTutorialFinished = false;
                              
                // Start Game
                SceneManager.LoadScene ("UpgradeShop");
        }
       
       
       
        // ---------------------------------------------------------------------------------------------------
        // getLevel()
        // ---------------------------------------------------------------------------------------------------
        // Returns the currently active level
        // ---------------------------------------------------------------------------------------------------
        public string getLevel(){
                return activeLevel;
        }
       
       
        // ---------------------------------------------------------------------------------------------------
        // setLevel()
        // ---------------------------------------------------------------------------------------------------
        // Sets the currently active level to a new value
        // ---------------------------------------------------------------------------------------------------
        public void setLevel(string newLevel){
                // Set activeLevel to newLevel
                activeLevel = newLevel;
        }
       
       
       
        // ---------------------------------------------------------------------------------------------------
        // getHealth()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters health
        // ---------------------------------------------------------------------------------------------------
        public int getHealth(){
                return setMaxHealth;
        }
       
        // ---------------------------------------------------------------------------------------------------
        // getShield()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters shield
        // ---------------------------------------------------------------------------------------------------
        public int getShield(){
                return setMaxShield;
        }

		// ---------------------------------------------------------------------------------------------------
        // getPrimaryAttackLevel()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters PrimaryAttackLevel
        // ---------------------------------------------------------------------------------------------------
        public int getPrimaryAttackLevel(){
                return setPrimaryAttackLevel;
        }

		// ---------------------------------------------------------------------------------------------------
        // getMultiAttackLevel()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters MultiAttackLevel
        // ---------------------------------------------------------------------------------------------------
        public int getMultiAttackLevel(){
                return setMultiAttackLevel;
        }

		// ---------------------------------------------------------------------------------------------------
		// getFusionAttackLevel()
		// ---------------------------------------------------------------------------------------------------
		// Returns the characters FusionAttackLevel
		// ---------------------------------------------------------------------------------------------------
		public int getFusionAttackLevel(){
			return setFusionAttackLevel;
		}

		// ---------------------------------------------------------------------------------------------------
		// getBombAttackLevel()
		// ---------------------------------------------------------------------------------------------------
		// Returns the characters BombAttackLevel
		// ---------------------------------------------------------------------------------------------------
		public int getBombAttackLevel(){
			return setBombAttackLevel;
		}


		// ---------------------------------------------------------------------------------------------------
        // getFireRate()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters FireRate
        // ---------------------------------------------------------------------------------------------------
        public float getFireRate(){
                return fireRate;
        }

		// ---------------------------------------------------------------------------------------------------
        // getCredits()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters credits
        // ---------------------------------------------------------------------------------------------------
        public int getCredits(){
                return credits;
        }

		// ---------------------------------------------------------------------------------------------------
		// getScore()
		// ---------------------------------------------------------------------------------------------------
		// Returns the characters Score
		// ---------------------------------------------------------------------------------------------------
		public int getScore(){
			return score;
		}

		// ---------------------------------------------------------------------------------------------------
		// getExtraLives()
		// ---------------------------------------------------------------------------------------------------
		// Returns the characters Extra Lives
		// ---------------------------------------------------------------------------------------------------
		public int getExtraLives(){
			return extraLives;
		}

		// ---------------------------------------------------------------------------------------------------
		// updateExtraLives()
		// ---------------------------------------------------------------------------------------------------
		// Updates the characters Extra Lives
		// ---------------------------------------------------------------------------------------------------
		public void updateExtraLives(){
			Debug.Log ("Game State - before UpdateExtraLives: " + extraLives);
			extraLives -= 1;
			Debug.Log ("Game State - after UpdateExtraLives: " + extraLives);
		}

		// ---------------------------------------------------------------------------------------------------
		// tutorialScreen
		// ---------------------------------------------------------------------------------------------------
		// Retrieves tutorialScreen bool
		// ---------------------------------------------------------------------------------------------------
		public bool getTutuorialFinished(){
			return setTutorialFinished;
		}

		public void makeTutorialTrue(){
			setTutorialFinished = true;
		}


		// ---------------------------------------------------------------------------------------------------
        //Collect values before scene change (Must be called during(before) scene change)
		// ---------------------------------------------------------------------------------------------------

        public void StoreVariables(){

			/*GameObject tutorialScreenObject = GameObject.Find ("Tutorial");
			print (tutorialScreenObject);	
			if (tutorialScreenObject != null) {
				tutorialScreen = tutorialScreenObject.GetComponent<TutorialScreen> ();
			}*/

	        GameObject playerObject = GameObject.Find ("Player");	
			if (playerObject != null) {
				playerController = playerObject.GetComponent<PlayerController> ();
				playerAttack = playerObject.GetComponent<PlayerAttack>();
			}

			GameObject gameControllerObject = GameObject.Find ("GameController");
			print (gameControllerObject);	
			if (gameControllerObject != null) {
				gameController = gameControllerObject.GetComponent<GameController> ();
			}



			setMaxHealth = playerController.getHealth();
			setMaxShield = playerController.getShield();
			setPrimaryAttackLevel = playerAttack.getPrimaryAttack();
			setMultiAttackLevel = playerAttack.getMultiAttack();
			fireRate = playerAttack.getFireRate();
			setFusionAttackLevel = playerAttack.getFusionAttack();
			setBombAttackLevel = playerAttack.getBombAttack();
			credits = playerController.getCredits ();
			score = gameController.getScore();
			extraLives = gameController.getExtraLives ();
			/*setTutorialFinished = tutorialScreen.getTutorialFinished();*/
			}


		//Load Scene by Hotkeys
		void Update(){
			/*if(Input.GetKey("l")){
				StoreVariables();
				SceneManager.LoadScene("RedPlanet");
			}
			if(Input.GetKey("k")){
				StoreVariables();
				SceneManager.LoadScene("GreenPlanet");
			}*/
			if (Input.GetKeyDown(KeyCode.U)){
				StoreVariables();
				SceneManager.LoadScene("UpgradeShop");
			}
		}
}
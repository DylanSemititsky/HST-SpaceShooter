// ---------------------------------------------------------------------------------------------------
// Holds all player variables in a GameState Instance object.
// Sets starting variables and stores saved variables just before any scene is loaded
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
       
        // Declare properties
        private static GameState instance;
        private string activeLevel;                     		// Active level
        private float maxHealth = 100;                                  // Max HP
        private float health = 100;                                     // Current HP
		private float maxShield;                                  // Max shield
        private float shield;                                     // Current Shield
        private int setPrimaryAttackLevel;                      // Primary Attack Level
        private int setMultiAttackLevel;                        // Multi Attack level
        private float fireRate;                                 // Fire Rate
        private int currency;
		private int bombsAvailable = 5;                                   // Currency

		PlayerController playerController;
        PlayerAttack playerAttack;
       
       
       
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
                maxHealth = 100;
                health = 100;
                maxShield = 20;
                shield = 20;
                setPrimaryAttackLevel = 1;
                setMultiAttackLevel = 0;
                fireRate = 0.5f;
                currency = 0;
                              
                // Load level 1
                SceneManager.LoadScene ("GreenPlanet");
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
        public float getHealth(){
                return health;
        }
       
        // ---------------------------------------------------------------------------------------------------
        // getShield()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters shield
        // ---------------------------------------------------------------------------------------------------
        public float getShield(){
                return shield;
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
        // getFireRate()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters FireRate
        // ---------------------------------------------------------------------------------------------------
        public float getFireRate(){
                return fireRate;
        }

		// ---------------------------------------------------------------------------------------------------
        // getCurrency()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters Currency
        // ---------------------------------------------------------------------------------------------------
        public int getCurrency(){
                return currency;
        }


		// ---------------------------------------------------------------------------------------------------
        //Collect values before scene change (Must be called during(before) scene change)
		// ---------------------------------------------------------------------------------------------------

        public void StoreVariables(){
        	
	        GameObject playerObject = GameObject.Find ("Player");	
			if (playerObject != null) {
				playerController = playerObject.GetComponent<PlayerController> ();
				playerAttack = playerObject.GetComponent<PlayerAttack>();
			}

			maxHealth = playerController.getMaxHealth();
			maxShield = playerController.getMaxShield();
			setPrimaryAttackLevel = playerAttack.primaryAttack.setPrimaryAttackLevel;
			setMultiAttackLevel = playerAttack.multiAttack.setMultiAttackLevel;
			fireRate = playerAttack.fireRate;
		}


		//Load Scene by Hotkeys
		void Update(){
			if(Input.GetKey("l")){
				StoreVariables();
				SceneManager.LoadScene("RedPlanet");
			}
			if(Input.GetKey("k")){
				StoreVariables();
				SceneManager.LoadScene("GreenPlanet");
			}
		}
}
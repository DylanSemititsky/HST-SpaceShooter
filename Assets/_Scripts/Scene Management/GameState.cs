using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
       
        // Declare properties
        private static GameState instance;
        private string activeLevel;                     		// Active level
        private int maxHealth;                                  // Max HP
        private int health;                                     // Current HP
		private int maxShield;                                  // Max shield
        private int shield;                                     // Current Shield
        private int setPrimaryAttackLevel;                      // Primary Attack Level
        private int setMultiAttackLevel;                        // Multi Attack level
        private float fireRate;                                 // Fire Rate
        private int currency;                                   // Currency
       
       
       
        // ---------------------------------------------------------------------------------------------------
        // gamestate()
        // ---------------------------------------------------------------------------------------------------
        // Creates an instance of gamestate as a gameobject if an instance does not exist
        // ---------------------------------------------------------------------------------------------------
        public static GameState Instance{
                get{
                        if(instance == null){
                                instance = new GameObject("gamestate").AddComponent<GameState> ();
                        }
 
                        return instance;
                }
        }      
       
        // Sets the instance to null when the application quits
        public void OnApplicationQuit(){
                instance = null;
        }
        // ---------------------------------------------------------------------------------------------------
       
       
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
                health = maxHealth;
                maxShield = 20;
                shield = maxShield;
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
        public int getHealth(){
                return health;
        }
       
        // ---------------------------------------------------------------------------------------------------
        // getShield()
        // ---------------------------------------------------------------------------------------------------
        // Returns the characters shield
        // ---------------------------------------------------------------------------------------------------
        public int getShield(){
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
}
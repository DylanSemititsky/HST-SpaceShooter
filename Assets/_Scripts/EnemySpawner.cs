using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveOptions
{
	public GameObject[] Wave; //for player to select/create horizontal wave
	public float bufferSpace; 
	public bool horz,vert; 
}


public class EnemySpawner : MonoBehaviour {

	public int spawnTimer; // Keeps track of game time once started
	public GameObject EnemySpawned; // enemy to spawn
	public int[] TimeToSpawn; // when the prefab will be spawned
	//public int numOfWaves;
	//public int timeBtwnWaves;
	private float time;// float convert variable to int
	private int spawnCounter;

	public WaveOptions waveOptions;

	private bool wave; //checks to see if player is using a wave
	private bool once;// call spawn function once

	void Start()
	{
		spawnTimer = 0; //set spawn timer to 0
		spawnCounter = 0;
		CheckWave ();
	}

	void Update()
	{
		TimeCount();//keeps track and sets time in integers
		TimeSpawnerCheck();
	}








	void SpawnEnemy(){ //spawn enemy function, instantiate
		if (wave == false) {
			Instantiate (EnemySpawned, transform.position, Quaternion.identity); //create single prefab
		}
		else if(wave == true)
		{
			Vector3 newPosition = this.transform.position;
			for (int i = 0; i < waveOptions.Wave.Length; i++)// create horizontal wave
			{
				if (waveOptions.horz == true) {
					newPosition.x -= waveOptions.bufferSpace;
				}
				else if(waveOptions.vert == true){
					newPosition.z -= waveOptions.bufferSpace;

				}

				Instantiate (waveOptions.Wave[i],newPosition, Quaternion.identity);

			}
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, Vector3.one);
	}
	void TimeCount()
	{
		time += Time.deltaTime;
		spawnTimer = (int)Mathf.Round (time);
		//print (spawnTimer);
	}
	void CheckWave(){
		if (waveOptions.Wave.Length > 0) { //check to see if horizontal wave is used
			wave = true;
		}
	}
	void TimeSpawnerCheck(){
		if (spawnTimer == TimeToSpawn[spawnCounter] && once == false) //once timer hits timeToSpawn time then output an enemy
		{
			InvokeRepeating ("SpawnEnemy", 0, 0);
			//once = true; //do this function once per call
			//print(spawnCounter);
			if (spawnCounter < TimeToSpawn.Length - 1) {//this if statment check to see if the end of the array list is over
				spawnCounter++;
			} else {
				once = true;
			}
			print(spawnCounter);

		}
	}
}

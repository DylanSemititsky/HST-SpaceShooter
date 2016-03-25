using UnityEngine;
using System.Collections;

public class DiagonalSpawner : MonoBehaviour {


	public int timer;
	public int[] timeToSpawn;
	public GameObject enemy;
	public int waveLength;
	private int waveCounter;
	public float bufferSpace;
	private float time;
	private int spawnTime;
	private bool once;



	void Start () {
		spawnTime = 0;
		timer = 0;
		waveCounter = 0;
		once = false;
	}


	void Update () {
		TimeCount ();
		SpawnerCheck ();

	}
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, Vector3.one);
	}



	void TimeCount()
	{
		time += Time.deltaTime;
		timer = (int)Mathf.Round (time);

		//print (spawnTimer);
	}
	void SpawnerCheck(){
		if (timer == timeToSpawn [spawnTime] && once == false)
		{

			InvokeRepeating ("SpawnEnemy", 0, bufferSpace);

			if (spawnTime < timeToSpawn.Length-1 ) {//this if statment check to see if the end of the array list is over
				spawnTime++;
				print (spawnTime);
			} else {
				once = true;
			}
		}
	}
	void SpawnEnemy(){ //spawn enemy function, instantiate
		if (waveCounter < waveLength)
			Instantiate (enemy, transform.position, Quaternion.identity); //create single prefab
		waveCounter++;
	}

}
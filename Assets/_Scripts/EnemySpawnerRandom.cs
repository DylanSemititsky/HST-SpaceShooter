//ALLOWS ENEMIES TO CONSTANTLY APPEAR RANDOMLY IN A DESIRED RANGE.
//CAN SET SPEED OF SPAWNRATE AND AREA TO SPAWN.

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemySpawnerRandom : MonoBehaviour 
{

	public GameObject hazard; 	//Place enemy prefab here.
	public Vector3 spawnValues;	//Set the range of where the enemies should spawn, based on axis.
	public int hazardCount;		//How many enemies should appear in a wave.
	public float spawnWait;		//Time between each enemy spawn.
	public float startWait;		//Initial time before enemies start spawning.
	public float waveWait;		//Time between each wave of enemies.
	public float endSpawns;
	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (Time.time < endSpawns)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);
		}
	}
}


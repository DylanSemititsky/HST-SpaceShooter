using UnityEngine;
using System.Collections;

public class RedMove : MonoBehaviour {

	//this script is responsible for the desert actions, including the infinite loop land

	private Vector3 pos;
	public int speed;

	[HideInInspector]

	public static bool oneTime = false;

	void Start () {
		print (oneTime);
		pos = transform.position; //get current position of terrain

	}

	void Update () {

		SpawnTerrain();//see if a collider is active then create new land
		transform.Translate(Vector3.forward * speed * Time.deltaTime);

		pos = transform.position;

		if (gameObject == null) 
		{
			pos.z += 99;

			Instantiate(gameObject, pos , Quaternion.identity);

			oneTime= true;

			pos = transform.position;

		}
	}

	void SpawnTerrain(){

		if(!oneTime){

			pos.z += 99;

			Instantiate(gameObject, pos , Quaternion.identity);

			oneTime= true;

			pos = transform.position;

		}

	}

	void OnTriggerExit(Collider other)
	{
		
		Destroy(gameObject);
		oneTime = false;
	}

}
using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	bool isPausing = false;


	// Use this for initialization
	void Start () 
	{
	 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (isPausing == false) 
			{
				Time.timeScale = 0f;
				isPausing = true;
				gameObject.GetComponent<MeshRenderer> ().enabled = true;
			} 

			else 
			
			{
				Time.timeScale = 1f; 
				isPausing = false;
				gameObject.GetComponent<MeshRenderer> ().enabled = false;
			}
				
		}
				
	}
}

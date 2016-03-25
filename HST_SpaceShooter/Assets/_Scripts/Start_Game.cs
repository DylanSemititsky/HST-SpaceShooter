using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Start_Game : MonoBehaviour 
{

	void OnTriggerEnter(Collider other)
	{
		SceneManager.LoadScene("Asteroid");
	}
		
		

}

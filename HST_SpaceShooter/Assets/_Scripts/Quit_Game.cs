using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Quit_Game : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		Application.Quit();
	}

}

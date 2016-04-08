// ---------------------------------------------------------------------------------------------------
// Code for the Start button in Main Menu scene. 
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameObject : MonoBehaviour 
{
	SceneFade sceneFade;

	public bool start = false;
	public GameObject fadeToBlack;

	private AudioSource audioSource;
	public GameObject explosion;

	// ---------------------------------------------------------------------------------------------------
	// START
	// ---------------------------------------------------------------------------------------------------
	void Start() {
		audioSource = GetComponent<AudioSource>();

	}

	// ---------------------------------------------------------------------------------------------------
	// Play audio, trigger explosion, begin FadeToBlack, flash Start button Red and Green, make start = true for GameStart script
	// ---------------------------------------------------------------------------------------------------
	IEnumerator OnTriggerEnter(Collider other){
		audioSource.Play();
		Instantiate(explosion, transform.position, transform.rotation);

		Renderer rend = GetComponent<Renderer> ();

		FadeActivate ();

		for(int i = 1; i <= 8; i++){
			rend.material.SetColor ("_TintColor", Color.green);
			yield return new WaitForSeconds (0.1f);
			rend.material.SetColor ("_TintColor", Color.red);
			yield return new WaitForSeconds (0.1f);
		}
		start = true;
		//SceneManager.LoadScene ("Asteroid");
	}

	public void FadeActivate(){
		sceneFade = fadeToBlack.GetComponent<SceneFade> ();
		sceneFade.fadeActivate = true;
	}
}



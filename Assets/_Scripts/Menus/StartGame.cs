using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour 
{
	SceneFade sceneFade;
	public GameObject fadeToBlack;

	private AudioSource audioSource;
	public GameObject explosion;

	void Start() {
		audioSource = GetComponent<AudioSource>();

	}

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
		SceneManager.LoadScene ("Asteroid");
	}

	public void FadeActivate(){
		sceneFade = fadeToBlack.GetComponent<SceneFade> ();
		sceneFade.fadeActivate = true;
	}
}



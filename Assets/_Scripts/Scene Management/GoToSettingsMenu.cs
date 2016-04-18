using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToSettingsMenu : MonoBehaviour 
{
	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	IEnumerator OnTriggerEnter(Collider other){
		audioSource.Play();

		Renderer rend = GetComponent<Renderer> ();

		for(int i = 1; i <= 8; i++){
			rend.material.SetColor ("_TintColor", Color.green);
			yield return new WaitForSeconds (0.1f);
			rend.material.SetColor ("_TintColor", Color.red);
			yield return new WaitForSeconds (0.1f);
		}

		//yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Settings");
	}
}
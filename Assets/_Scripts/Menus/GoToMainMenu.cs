﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour {

	public AudioSource audioSource;
	public GameObject explosion;

	ChangeShipColor changeShipColor;

	void GoToSettings() {
		audioSource = GetComponent<AudioSource>();

	}

	IEnumerator OnTriggerEnter(Collider other){
		audioSource.Play();
		Instantiate(explosion, transform.position, transform.rotation);

		Renderer rend = GetComponent<Renderer> ();
		float fadeTime = GameObject.Find ("Fade").GetComponent<Fading>().BeginFade(1);

		for(int i = 1; i <= 8; i++){
			rend.material.SetColor ("_TintColor", Color.green);
			yield return new WaitForSeconds (0.1f);
			rend.material.SetColor ("_TintColor", Color.red);
			yield return new WaitForSeconds (0.1f);
		}

		//yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Main Menu");
	}

}

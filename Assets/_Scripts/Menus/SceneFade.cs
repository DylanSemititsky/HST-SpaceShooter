﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour {

	public bool fadeActivate;

	void Start(){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		fadeActivate = false;
	}

	void Update(){
		if (fadeActivate == true) {
			Debug.Log ("fadeActive is True!");
			StartCoroutine (FadeInCo());
		}
	}


	private IEnumerator FadeInCo()
 	{
     	CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
     	while (canvasGroup.alpha < 1) {
     		canvasGroup.alpha += Time.deltaTime / 100;
     		yield return null;
     	}

     	yield return null;
 	}

	IEnumerator FadeOut()
 	{
     	CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
     	while (canvasGroup.alpha > 0) {
     		canvasGroup.alpha -= Time.deltaTime / 2;
     		yield return null;
     	}

     	yield return null;
 	}

	/*public void FadeIn(){
		StartCoroutine (FadeInCo);
	}*/
}
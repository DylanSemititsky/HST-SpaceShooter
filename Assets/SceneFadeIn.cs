﻿using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour {


	void Start () {
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
		StartCoroutine (FadeFromBlack());
	}


	IEnumerator FadeFromBlack()
	{
		Debug.Log ("Coroutine Started");
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime / 2;
			yield return null;
		}

		yield return null;
	}
}

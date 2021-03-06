﻿using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour {


	void Start () {
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
		StartCoroutine (FadeFromBlack());

		gameObject.SetActive(false);
	}


	IEnumerator FadeFromBlack()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime / 2;
			yield return null;
		}
			
		yield return null;
	}
}

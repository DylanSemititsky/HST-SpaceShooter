﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFadeInOut : MonoBehaviour {


	IEnumerator Start(){

		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		yield return new WaitForSeconds(6);
		StartCoroutine(FadeIn());
		yield return new WaitForSeconds(8);
		StartCoroutine(FadeOut());

	}

	IEnumerator FadeIn()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		while (canvasGroup.alpha < 1) {
			canvasGroup.alpha += Time.deltaTime / 2;
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
}
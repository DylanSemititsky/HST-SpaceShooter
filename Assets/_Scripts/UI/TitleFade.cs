using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleFade : MonoBehaviour {


IEnumerator Start(){

		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		yield return new WaitForSeconds(1);
		StartCoroutine(FadeIn());
		yield return new WaitForSeconds(3);
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
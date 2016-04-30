using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour {

	public bool fadeActivate;

	void Start(){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		fadeActivate = false;

		gameObject.SetActive(false);
		Debug.Log ("Scene Fade Start");
	}

	void Update(){
		Debug.Log ("before coroutine");
		if (fadeActivate == true) {
			StartCoroutine (FadeInCo());
			Debug.Log ("coroutine started");
		}
	}


	private IEnumerator FadeInCo()
 	{
     	CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
     	while (canvasGroup.alpha < 1) {
     		canvasGroup.alpha += Time.deltaTime / 50;
     		yield return null;
     	}

     	yield return null;
 	}


}
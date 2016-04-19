using UnityEngine;
using System.Collections;

public class FadeObject : MonoBehaviour {

	Color objectMaterial;

	void start(){
		
	}

	void Update () {
		
		StartCoroutine (FadeColor ());



	}
	IEnumerator FadeColor(){ //fade shield away
		objectMaterial = transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color;

		for (int i = 0; objectMaterial.a > 0.01; i++) {
			
			objectMaterial.a -= 0.01f;
			transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = objectMaterial;

			yield return new WaitForSeconds (0.4f);
		}
		
	}
}

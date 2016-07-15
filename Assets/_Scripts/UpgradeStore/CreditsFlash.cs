using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsFlash : MonoBehaviour {

public Text credits;
Color temp;

	void Start(){
		credits = GetComponent<Text>();
		temp = credits.color;
	}


	IEnumerator FlashCreditsText(){
			
		for(int i = 1; i <= 5; i++){
			credits.color = Color.red;
			yield return new WaitForSeconds(0.1f);
			credits.color = Color.yellow;
			yield return new WaitForSeconds(0.1f);
		}
		credits.color = temp;
	}

	public void StartFlash(){
		StartCoroutine(FlashCreditsText());	
	}
}

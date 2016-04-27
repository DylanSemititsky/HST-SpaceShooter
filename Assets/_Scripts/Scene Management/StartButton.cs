// ---------------------------------------------------------------------------------------------------
// When hitting the Start button on Main Menu this script calls the GameState.startState
// ---------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

	SceneFade sceneFade;

	public bool start = false;
	public GameObject fadeToBlack;

	// ---------------------------------------------------------------------------------------------------
	// START
	// ---------------------------------------------------------------------------------------------------
	void Start(){

	}

	void Update(){
		if (start == true) {
			beginGame ();
		}
	}

	public void StartFlash(){
		StartCoroutine (Flash ());
	}


	private IEnumerator Flash(){

		fadeToBlack.SetActive (true);
		FadeActivate ();

		for(int i = 0; i <= 8; i++){
			GetComponent<Image> ().color = Color.white;
			yield return new WaitForSeconds(0.1f);
			GetComponent<Image> ().color = Color.green;
			yield return new WaitForSeconds(0.1f);
		}
		start = true;
	}


    private void beginGame(){

        DontDestroyOnLoad(GameState.Instance);
        GameState.Instance.startState();       
    }

	public void FadeActivate(){
		sceneFade = fadeToBlack.GetComponent<SceneFade> ();
		sceneFade.fadeActivate = true;
	}
}
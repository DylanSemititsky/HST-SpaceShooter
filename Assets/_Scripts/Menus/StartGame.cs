using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour 
{
	SceneFade sceneFade;
	public GameObject fadeToBlack;

	private AudioSource audioSource;
	public GameObject explosion;
	//public Image fadeToBlackImage;
	//public float flashSpeed;
	//public Color flashColor = new Color (0f, 0f, 0f, 0.1f);

	void Start() {
		audioSource = GetComponent<AudioSource>();

	}

	IEnumerator OnTriggerEnter(Collider other){
		audioSource.Play();
		Instantiate(explosion, transform.position, transform.rotation);

		Renderer rend = GetComponent<Renderer> ();

		//fadeToBlackImage.color = flashColor;
		//fadeToBlackImage.color = Color.Lerp (fadeToBlackImage.color, Color.black, flashSpeed * Time.deltaTime);

		FadeActivate ();

		for(int i = 1; i <= 8; i++){
			rend.material.SetColor ("_TintColor", Color.green);
			yield return new WaitForSeconds (0.1f);
			rend.material.SetColor ("_TintColor", Color.red);
			yield return new WaitForSeconds (0.1f);
		}

		//yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Asteroid");
	}

	public void FadeActivate(){
		sceneFade = fadeToBlack.GetComponent<SceneFade> ();
		sceneFade.fadeActivate = true;
	}

	/*IEnumerator LoadScene(){
		float fadeTime = GameObject.Find ("Fade").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Asteroid");
	}*/
}



using UnityEngine;
using System.Collections;

public class MainMenuMusicPlayer : MonoBehaviour {

	static bool AudioBegin = false;
	public AudioSource audioClip;
	public bool isFadingMusic;

	static MainMenuMusicPlayer instance = null;

	void Awake()
	{
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}

	}
	void Update(){
		if (Application.loadedLevelName == "UpgradeShop") {

			audioClip.Stop ();
			AudioBegin = false;
			Destroy (gameObject);
		}
		if (isFadingMusic) {
			
			audioClip.volume -= Time.deltaTime * 0.35f;

		}


	}
	public void FadeOutMusic(){
		isFadingMusic = true;
	}
}

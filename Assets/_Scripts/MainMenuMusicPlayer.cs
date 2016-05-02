using UnityEngine;
using System.Collections;

public class MainMenuMusicPlayer : MonoBehaviour {

	static bool AudioBegin = false;
	public AudioSource audioClip;
	public bool isFadingMusic;

	void Awake()
	{
		if (!AudioBegin) {
			audioClip.Play ();
			DontDestroyOnLoad (gameObject);
			AudioBegin = true;
		}

	}
	void Update(){
		if (Application.loadedLevelName == "UpgradeShop") {

			audioClip.Stop ();
			AudioBegin = false;
		}
		if (isFadingMusic) {
			
			audioClip.volume -= Time.deltaTime * 0.25f;

		}


	}
	public void FadeOutMusic(){
		isFadingMusic = true;
	}
}

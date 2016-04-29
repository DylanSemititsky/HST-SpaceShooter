using UnityEngine;
using System.Collections;

public class BossMusicScript : MonoBehaviour {

	GameObject boss1;
	GameObject boss2;
	GameObject boss3;
	public AudioSource bossMusic;
	public float fadeMusicSpeed = 0.25f;
	bool playOnce;
	bool turnVolumeUp = true;
	GameObject otherMusicPlayer;

	void Start () {
		otherMusicPlayer = GameObject.Find ("MusicPlayer");
	}
	
	void Update () {
		boss1 = GameObject.Find ("Boss1(Clone)");
		if (boss1 != null) {
			PlayBossMusic ();

		}
		boss2 = GameObject.Find ("Boss2(Clone)");
		if (boss2 != null) {
			PlayBossMusic ();
		}
		boss3 = GameObject.Find ("Boss3(Clone)");
		if (boss3 != null) {
			PlayBossMusic ();
		}
	}

	void PlayBossMusic(){
		if (turnVolumeUp) {
			bossMusic.volume += Time.deltaTime * fadeMusicSpeed;
		}

		if (bossMusic.volume > 0.45f) {
			turnVolumeUp = false;
		}

		if (!playOnce) {
			playOnce = true;
			bossMusic.Play ();
		}
		//fade-out other music
		otherMusicPlayer.GetComponent<AudioSource>().volume -= Time.deltaTime * fadeMusicSpeed;
	}
}

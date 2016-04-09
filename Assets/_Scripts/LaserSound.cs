using UnityEngine;
using System.Collections;

public class LaserSound : MonoBehaviour {

	public AudioClip spaceGun;
	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint(spaceGun, new Vector3(0, 30, 5), 1f);
	}
}

using UnityEngine;
using System.Collections;

public class LaserSound : MonoBehaviour {

	public AudioSource laserSound;

	// Use this for initialization
	void Start () {
		laserSound.Play ();
	}
}

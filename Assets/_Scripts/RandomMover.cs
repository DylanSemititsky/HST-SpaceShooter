using UnityEngine;
using System.Collections;

public class RandomMover : MonoBehaviour {

	public float speed = 0.5f;

	void Start () {
		Vector3 down = new Vector3 (0, 0, -1);
		transform.Rotate (down);
	}
	

	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}

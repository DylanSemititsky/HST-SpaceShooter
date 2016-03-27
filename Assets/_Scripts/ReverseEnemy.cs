using UnityEngine;
using System.Collections;

public class ReverseEnemy : MonoBehaviour {

	public float speed;

	void Start () {
	
	}
	
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);

	}
}

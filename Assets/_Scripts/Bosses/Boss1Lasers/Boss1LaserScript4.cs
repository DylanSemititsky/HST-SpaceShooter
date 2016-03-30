using UnityEngine;
using System.Collections;

public class Boss1LaserScript4 : MonoBehaviour {

	public float laserSpeed;

	void Start () {
		transform.eulerAngles = new Vector3 (0, -25, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward *laserSpeed * Time.deltaTime);
	}
}

using UnityEngine;
using System.Collections;

public class LeftDiagonalEnemy : MonoBehaviour {
	
	public float speed;
	private float rotateSpeed = 50;

	void Start () {
		transform.eulerAngles = new Vector3 (0, 300, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (Vector3.forward * -speed * Time.deltaTime);
		//transform.Translate (Vector3. * -speed * Time.deltaTime);
	//	transform.Rotate (Vector3.down * rotateSpeed * Time.deltaTime);
	//	transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
	}
}

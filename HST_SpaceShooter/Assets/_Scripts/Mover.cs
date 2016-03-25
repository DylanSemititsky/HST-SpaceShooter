//CONTROLS THE DIRECTION AND SPEED OF ANY OBJECT

using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	public float direction;

	void Start (){

		rb = GetComponent<Rigidbody>();

		//Direction 0: Directly forward (up) on the z-axis. Use negative numbers to go backwards (down).
		if (direction == 0){
			rb.velocity = transform.forward * speed;
		}

		//Direction 1: Set to go at a 7.25 degree angle (forward and to the right). Used for Multi Laser Cannon.
		if (direction == 1){
			rb.velocity = new Vector3(0.1f, 0.0f, 0.8f) * speed;
		}

		//Direction 2: Set to go at a 7.25 degree angle (forward and to the left). Used for Multi Laser Cannon.
		if (direction == 2){
			rb.velocity = new Vector3(-0.1f, 0.0f, 0.8f) * speed;
		}

		if (direction == 3){
			rb.velocity = new Vector3(0.0f, 0.0f, 1.0f) * speed;
		}
	}
}

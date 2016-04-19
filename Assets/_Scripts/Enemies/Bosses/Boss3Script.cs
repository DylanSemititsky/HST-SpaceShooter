using UnityEngine;
using System.Collections;

public class Boss3Script : MonoBehaviour {

	//EnterPlaySpace Variables
	public float enterSpeed;
	bool hasEntered = false;


	//Movement Variables
	public float horizontalSpeed; //bosses horizontal speed
	public float hAplitude; // the max width range the boss with travel
	public float originalAplitude;
	Vector3 tempPosition ;
	Vector3 originPos;
	bool setMovement;
	Vector3 otherTempPos;

	void Start () {
		originalAplitude = hAplitude;//SET THESE VARIABLES TO 0
		hAplitude = 0;
	}
	
	void Update () {
		EnterPlaySpace (); //enter play space
		BossMovement (); //move left to right

	
	}

	//enter play space
	//movement, left right
	//shoot home laser turret
	//main laser turret
	//check health
	//death
	void BossMovement(){

		tempPosition.x = Mathf.Sin (Time.realtimeSinceStartup * horizontalSpeed) * hAplitude;
		transform.position = tempPosition;

		if (hasEntered){
			if (!setMovement) {
				tempPosition.z += 9.5f;
				transform.position = tempPosition;
				setMovement = true;
			}
		}

	}

	void EnterPlaySpace(){
		if (hasEntered == false) {
			
			transform.Translate (Vector3.forward * -enterSpeed * Time.deltaTime);

			if (transform.position.z <= 9.5f) {
				hasEntered = true;

				Vector3 temp = transform.position; // sets the z position to an absolute one
				temp.z = 9.5f;
				transform.position = temp;
				//tempPosition = transform.position;
				//originPos = transform.position;
			}
		}
	}

}

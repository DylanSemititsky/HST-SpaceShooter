using UnityEngine;
using System.Collections;

public class Boss1Script3 : MonoBehaviour {
	//boss 3 script takes in input for the arm of the boss
	Boss1Script boss1Script;

	void Start () {
		boss1Script = GameObject.Find ("Boss1").GetComponent<Boss1Script> ();
	}
	

	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}
		if (other.tag == "pLaser 1") {
			if(gameObject.name == "leftHand")
				boss1Script.leftArmHealth -= 10;
			if(gameObject.name == "rightHand")
				boss1Script.rightArmHealth -= 10;
		}
		if (other.tag == "pLaser 2") {
			if(gameObject.name == "leftHand")
				boss1Script.leftArmHealth -= 20;
			if(gameObject.name == "rightHand")
				boss1Script.rightArmHealth -= 20;
		}
		if (other.tag == "pLaser 3") {
			if(gameObject.name == "leftHand")
				boss1Script.leftArmHealth -= 30;
			if(gameObject.name == "rightHand")
				boss1Script.rightArmHealth -= 30;
		}
		if (other.tag == "pLaser 4") {
			if(gameObject.name == "leftHand")
				boss1Script.leftArmHealth -= 40;
			if(gameObject.name == "rightHand")
				boss1Script.rightArmHealth -= 40;
		}
	}
}

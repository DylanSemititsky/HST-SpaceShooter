using UnityEngine;
using System.Collections;

public class Boss1Script3 : MonoBehaviour {
	//boss 3 script takes in input for the arm of the boss
	Boss1Script boss1Script;
	private bool damaged;

	void Start () {
		boss1Script = GameObject.Find ("Boss1(Clone)").GetComponent<Boss1Script> ();
	}
	

	void Update () {
		if (damaged) {
			StartCoroutine (DamageFlash ());
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}
		if (other.tag == "pLaser 1") {
			damaged = true;
			if(gameObject.name == "leftHand")
				boss1Script.leftArmHealth -= 10;
			if(gameObject.name == "rightHand")
				boss1Script.rightArmHealth -= 10;
		}
		if (other.tag == "pLaser 2") {
			damaged = true;
			if(gameObject.name == "leftHand")
				boss1Script.leftArmHealth -= 20;
			if(gameObject.name == "rightHand")
				boss1Script.rightArmHealth -= 20;
		}
		if (other.tag == "pLaser 3") {
			damaged = true;
			if(gameObject.name == "leftHand")
				boss1Script.leftArmHealth -= 30;
			if(gameObject.name == "rightHand")
				boss1Script.rightArmHealth -= 30;
		}
		if (other.tag == "pLaser 4") {
			damaged = true;
			if(gameObject.name == "leftHand")
				boss1Script.leftArmHealth -= 40;
			if(gameObject.name == "rightHand")
				boss1Script.rightArmHealth -= 40;
		}

	}
	public IEnumerator DamageFlash(){

		Renderer rend = GetComponentInChildren<Renderer> ();
		for(int i = 1; i <= 4; i++){
			rend.material.SetColor ("_Color", Color.red);
			yield return new WaitForSeconds (0.1f);
			rend.material.SetColor ("_Color", Color.white);
			yield return new WaitForSeconds (0.1f);
			damaged = false;
		}
	}
}

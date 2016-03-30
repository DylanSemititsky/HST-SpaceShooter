using UnityEngine;
using System.Collections;

public class Boss1Script2 : MonoBehaviour {
	//boss 2 script keeps track of damage intake by the head of the boss
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
		if (boss1Script.bossHeadActive == true) {
			if (other.tag == "pLaser 1") {
				boss1Script.bossHealth -= 10;
			}
			if (other.tag == "pLaser 2") {
				boss1Script.bossHealth -= 20;
			}
			if (other.tag == "pLaser 3") {
				boss1Script.bossHealth -= 30;
			}
			if (other.tag == "pLaser 4") {
				boss1Script.bossHealth -= 40;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class Boss1Script2 : MonoBehaviour {
	//boss 2 script keeps track of damage intake by the head of the boss
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
		if (boss1Script.bossHeadActive == true) {



			if (other.tag == "pLaser 1") {
				damaged = true;
				boss1Script.bossHealth -= 10;
			}
			if (other.tag == "pLaser 2") {
				damaged = true;
				boss1Script.bossHealth -= 20;
			}
			if (other.tag == "pLaser 3") {
				damaged = true;
				boss1Script.bossHealth -= 30;
			}
			if (other.tag == "pLaser 4") {
				damaged = true;
				boss1Script.bossHealth -= 40;
			}
		}
	}
	public IEnumerator DamageFlash(){

		Renderer rend = GetComponent<Renderer> ();
		for(int i = 1; i <= 4; i++){
			rend.material.SetColor ("_Color", Color.red);
			yield return new WaitForSeconds (0.1f);
			rend.material.SetColor ("_Color", Color.white);
			yield return new WaitForSeconds (0.1f);
			damaged = false;
		}
	}
}

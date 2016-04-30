using UnityEngine;
using System.Collections;

public class Boss3Turret : MonoBehaviour {

	private bool damaged;
	Boss3Script boss3script;
	Color originalColor;

	void Start () {
	//	boss3script = GameObject.Find("Boss3").GetComponent<Boss3Script> ();
		originalColor = GetComponent<Renderer> ().material.color;
		boss3script = GameObject.Find("Boss3(Clone)").GetComponent<Boss3Script> ();
	}
	
	// Update is called once per frame
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
			if(gameObject.name == "Turret1" && boss3script.turret1Exists == true)
				boss3script.turret1Health -= 10;
			else if(gameObject.name == "Turret2"&& boss3script.turret2Exists == true)
				boss3script.turret2Health -= 10;
			else if(gameObject.name == "Turret3" && boss3script.turret3Exists == true)
				boss3script.turret3Health -= 10;
			else if(gameObject.name == "Turret4" && boss3script.turret4Exists == true)
				boss3script.turret4Health -= 10;
		}
		else if (other.tag == "pLaser 2") {
			damaged = true;
			if(gameObject.name == "Turret1"&& boss3script.turret1Exists == true)
				boss3script.turret1Health -= 20;
			else if(gameObject.name == "Turret2" && boss3script.turret2Exists == true)
				boss3script.turret2Health -= 20;
			else if(gameObject.name == "Turret3" && boss3script.turret3Exists == true)
				boss3script.turret3Health -= 20;
			else if(gameObject.name == "Turret4"&& boss3script.turret4Exists == true)
				boss3script.turret4Health -= 20;
		}
		else if (other.tag == "pLaser 3") {
			damaged = true;
			if(gameObject.name == "Turret1"&& boss3script.turret1Exists == true)
				boss3script.turret1Health -= 30;
			else if(gameObject.name == "Turret2" && boss3script.turret2Exists == true)
				boss3script.turret2Health -= 30;
			else if(gameObject.name == "Turret3" && boss3script.turret3Exists == true)
				boss3script.turret3Health -= 30;
			else if(gameObject.name == "Turret4"&& boss3script.turret4Exists == true)
				boss3script.turret4Health -= 30;
		}
		else if (other.tag == "pLaser 4") {
			damaged = true;
			if(gameObject.name == "Turret1"&& boss3script.turret1Exists == true)
				boss3script.turret1Health -= 40;
			else if(gameObject.name == "Turret2" && boss3script.turret2Exists == true)
				boss3script.turret2Health -= 40;
			else if(gameObject.name == "Turret3" && boss3script.turret3Exists == true)
				boss3script.turret3Health -= 40;
			else if(gameObject.name == "Turret4"&& boss3script.turret4Exists == true)
				boss3script.turret4Health -= 40;
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
		rend.material.color = originalColor;
	}
}

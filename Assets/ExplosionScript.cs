using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	private float nextDetonate;
	private float detonateTime = 0.5f;
	public float damage1 = 50;
	public float damage2 = 25;
	float timer = 0.0f;
	float trigger = 3.0f;

	void Start () {
		timer = 0.0f;
		nextDetonate = Time.time + detonateTime;
	}


	void Update () {
		Detonate();
	}


	void Detonate(){
		if(Time.time > nextDetonate){
			 
			nextDetonate = Time.time + detonateTime;
			Detonation();
		}

		timer += Time.deltaTime;

		if(timer >= trigger){
			Destroy(gameObject);
		}
	}


	public void Detonation(){

			StartCoroutine(ExplosionDamage1());
	}



IEnumerator ExplosionDamage1(){
		Debug.Log("ExplosionDamage1 Started");
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, 8);

		foreach (Collider hit in colliders){
			GameObject hitObject = hit.gameObject;
			DestroyByHealth enemy = hitObject.GetComponent<DestroyByHealth>();

			Debug.Log("Before Null");

			if(enemy != null){
				enemy.AddDamage(damage1);
				Debug.Log("Damage 1 applied");
			}
		}
		yield return new WaitForSeconds(0.3f);
	} 
}

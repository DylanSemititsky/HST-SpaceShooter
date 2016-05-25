using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	private float nextDetonate;
	private float detonateTime = 0.5f;
	public float damage = 10;
	float timer = 0.0f;
	float trigger = 3.0f;

	//Shaking Camera variables
	public float amplitude = 0.1f;
	public float duration = 0.5f;

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
			ExplosionDamage1();
		}

		timer += Time.deltaTime;

		if(timer >= trigger){
			Destroy(gameObject);
		}
	}


	void ExplosionDamage1(){
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, 8);
		CameraShake.Instance.Shake (amplitude, duration);//call camera shake

		foreach (Collider hit in colliders){
			GameObject hitObject = hit.gameObject;
			DestroyByHealth enemy = hitObject.GetComponent<DestroyByHealth>();
			if (hit.gameObject.tag == "eLaser 1" || hit.gameObject.tag == "eLaser 2" || hit.gameObject.tag == "eLaser 3" || hit.gameObject.tag == "eLaser 4") {
				Destroy (hit.gameObject);
			}
				
			if(enemy != null){
				enemy.AddDamage(damage);
			}
		}
	} 
}

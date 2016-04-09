using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	private float nextDetonate;
	private float detonateTime = 2;
	public GameObject explosion;

	public float damage1 = 200;
	public float damage2 = 25;

	private Rigidbody rb;
	public float speed;

	void Start () {

		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;

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
	}


	public void OnTriggerEnter(Collider other){
		
		if(other.tag == "Enemy"){
			Instantiate(explosion, transform.position, transform.rotation);

			Detonation();
		}
	}


	public void Detonation(){
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);

		ExplosionDamage1();

		Destroy(gameObject);
	}



	void ExplosionDamage1(){
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, 8);

		foreach (Collider hit in colliders){
			GameObject hitObject = hit.gameObject;
			DestroyByHealth enemy = hitObject.GetComponent<DestroyByHealth>();

			if(enemy != null){
				enemy.AddDamage(damage1);
			}
		}
	} 
}

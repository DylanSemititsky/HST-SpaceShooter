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
	bool nextRoutine1 = false;
	bool nextRoutine2 = false;
	bool nextRoutine3 = false;

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
			Debug.Log("Collider with Enemy Occured");
			Instantiate(explosion, transform.position, transform.rotation);

			Detonation();
		}
	}


	public void Detonation(){
			Debug.Log("Detonation Coroutine Started");
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);

			StartCoroutine(ExplosionDamage1());

		Destroy(gameObject);
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

using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	private float nextDetonate;
	private float detonateTime = 3;
	public GameObject explosion;

	private Rigidbody rb;
	public float speed;

	void Start () {

		rb = GetComponent<Rigidbody>();

		rb.velocity = transform.forward * speed;

	}


	// Update is called once per frame
	void Update () {

		//Detonate();
	}

	void Detonate(){
		if(Time.time > nextDetonate){
			nextDetonate = Time.time + detonateTime;
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}

	/*void OnTriggerEnter(Collider other){
		if(other.tag != "Player")
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}*/
}

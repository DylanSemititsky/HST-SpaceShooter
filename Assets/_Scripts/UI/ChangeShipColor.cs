using UnityEngine;
using System.Collections;

public class ChangeShipColor : MonoBehaviour {

	//public AudioSource audioSource;
	public static ChangeShipColor Instance;


	void Start() {
		//audioSource = GetComponent<AudioSource>();
		//Renderer rend = GetComponent<Renderer> ();
		//rend = GlobalControl.Instance.rend;
	}

	public void OnTriggerEnter(Collider other){
		//audioSource.Play();
		Renderer rend = GetComponent<Renderer> ();

		if(other.tag == "red"){
			rend.sharedMaterial.SetColor ("_Color", Color.red);
		}
		if(other.tag == "cyan"){
			rend.sharedMaterial.SetColor ("_Color", Color.cyan);
		}
		if(other.tag == "green"){
			rend.sharedMaterial.SetColor ("_Color", Color.green);
		}
		if(other.tag == "magenta"){
			rend.sharedMaterial.SetColor ("_Color", Color.magenta);
		}
		if(other.tag == "yellow"){
			rend.sharedMaterial.SetColor ("_Color", Color.yellow);
		}
		if(other.tag == "gray"){
			rend.sharedMaterial.SetColor ("_Color", Color.gray);
		}
	}

	/*public void SavePlayer(){
		Renderer rend = GetComponent<Renderer> ();
		GlobalControl.Instance.rend = rend;
	}*/
}

//OPTIONAL METHOD TO DESTROY ANY OBJECT BASED ON TIME

using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour 
{

	public float lifetime;

	void Start () 
	{
		Destroy (gameObject, lifetime);
	}
}

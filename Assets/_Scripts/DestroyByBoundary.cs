//DESTROYS ANY OBJECT WITH A TRIGGER COLLIDER THAT LEAVES THE GAME SCREEN AREA

using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerExit(Collider other)
	{
		if (other.tag != "boss2Arms") {
			Destroy (other.gameObject);
		}
	}
}

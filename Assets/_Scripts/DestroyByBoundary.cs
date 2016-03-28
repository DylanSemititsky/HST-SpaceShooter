//DESTROYS ANY OBJECT WITH A TRIGGER COLLIDER THAT LEAVES THE GAME SCREEN AREA

using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerExit(Collider other)
	{

		Destroy(other.gameObject);

	}
}

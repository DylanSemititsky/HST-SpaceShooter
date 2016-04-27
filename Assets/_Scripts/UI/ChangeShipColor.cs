using UnityEngine;
using System.Collections;

public class ChangeShipColor : MonoBehaviour {

	public GameObject player;
	public static ChangeShipColor Instance;

	public void ChangeColorGray(){
		Renderer rend = player.GetComponent<Renderer> ();
		rend.sharedMaterial.SetColor ("_Color", Color.white);
	}

	public void ChangeColorRed(){
		Renderer rend = player.GetComponent<Renderer> ();
		rend.sharedMaterial.SetColor ("_Color", Color.red);
	}

	public void ChangeColorGreen(){
		Renderer rend = player.GetComponent<Renderer> ();
		rend.sharedMaterial.SetColor ("_Color", Color.green);
	}

	public void ChangeColorMagenta(){
		Renderer rend = player.GetComponent<Renderer> ();
		rend.sharedMaterial.SetColor ("_Color", Color.magenta);
	}

	public void ChangeColorCyan(){
		Renderer rend = player.GetComponent<Renderer> ();
		rend.sharedMaterial.SetColor ("_Color", Color.cyan);
	}

	public void ChangeColorYellow(){
		Renderer rend = player.GetComponent<Renderer> ();
		rend.sharedMaterial.SetColor ("_Color", Color.yellow);
	}
}

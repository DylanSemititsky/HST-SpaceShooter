//USED TO SCROLL BACKGROUND TILEABLE IMAGE.
//CONTROLS OFFSET OF BACKGROUND MATERIAL.

using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

	public float scrollSpeed; //Set scroll speed.

	void Start () {
	
	}

	void Update () {
		float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (0, y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}

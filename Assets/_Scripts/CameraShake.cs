using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public static CameraShake Instance;

	private float _amplitude = 0.1f;
	                                        
	private Vector3 startPos;
	bool isShaking = false;


	void Start () {
		Instance = this; //so there is only one camera shake at once
		startPos = transform.localPosition; //start psoiton
	}
	
	void Update () {
		if (isShaking) { //if shaking is true then activate it
			transform.localPosition = startPos + Random.insideUnitSphere * _amplitude;
		}
	}

	public void Shake(float amplitude, float duration){
		_amplitude = amplitude;
		isShaking = true;
		CancelInvoke ();
		Invoke ("StopShaking", duration);
	}
	public void StopShaking(){
		isShaking = false;
	}
}

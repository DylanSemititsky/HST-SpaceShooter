using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl instance;

	public float maxHealth;
	public float health;
	public float maxShield;
	public float shield;

	public float fireRate;
	public float setPrimaryAttackLevel;
	public float setMultiAttackLevel;

	public float currency;
	public float cupcakes;

	void Awake(){
		if (instance == null){
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else if (instance != this){
			Destroy(gameObject);
		}
	}
}

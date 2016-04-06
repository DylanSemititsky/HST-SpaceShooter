using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour {

public static GlobalControl Instance;

	public float maxHealth;
	public float health;
	public float maxShield;
	public float shield;

	public Renderer rend;

	public float fireRate;
	public float setPrimaryAttackLevel;
	public float setMultiAttackLevel;

	public float currency;
	public float cupcakes;

	void Awake(){
		if (Instance == null){
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this){
			Destroy(gameObject);
		}
	}
}

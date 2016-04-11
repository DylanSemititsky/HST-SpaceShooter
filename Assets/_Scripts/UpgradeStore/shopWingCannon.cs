using UnityEngine;
using System.Collections;

public class shopWingCannon : MonoBehaviour {

	PlayerAttack playerAttack;
	private int playerMainCannonTemp;
	private int playerMultiCannonTemp;

	// Use this for initialization
	void Start () {
	
		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}

		playerMainCannonTemp = playerAttack.primaryAttack.setPrimaryAttackLevel;
		playerMultiCannonTemp = playerAttack.multiAttack.setMultiAttackLevel;
	}	
	// Update is called once per frame
	void Update () {
		Debug.Log ("primary attack = " + playerAttack.primaryAttack.setPrimaryAttackLevel);
	}

	public void ShowMultiAttack(){
		playerMainCannonTemp = playerAttack.primaryAttack.setPrimaryAttackLevel;
		playerAttack.multiAttack.setMultiAttackLevel += 1;
		playerAttack.primaryAttack.setPrimaryAttackLevel = 0;
		playerAttack.disableBurst = true;
	}

	public void EnableUpgrade(){
		playerAttack.multiAttack.setMultiAttackLevel += 1;
		playerMultiCannonTemp += 1;
	}

	public void Revert(){
		playerAttack.primaryAttack.setPrimaryAttackLevel = playerMainCannonTemp;
		playerAttack.multiAttack.setMultiAttackLevel = playerMultiCannonTemp;
		playerAttack.disableBurst = false;
	}
}

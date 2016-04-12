using UnityEngine;
using System.Collections;

public class ShopWingCannon : MonoBehaviour {

	PlayerAttack playerAttack;
	private int playerMainCannonTemp;
	private int playerMultiCannonTemp;


	void Start () {
	
		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
		}

		playerMainCannonTemp = playerAttack.primaryAttack.setPrimaryAttackLevel;
		playerMultiCannonTemp = playerAttack.multiAttack.setMultiAttackLevel;
	}	

	void Update () {
	}

	public void ShowMultiAttack(){
		playerMainCannonTemp = playerAttack.primaryAttack.setPrimaryAttackLevel;
		playerAttack.multiAttack.setMultiAttackLevel += 1;
		playerAttack.primaryAttack.setPrimaryAttackLevel = 0;
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){
		playerAttack.multiAttack.setMultiAttackLevel += 1;
		playerMultiCannonTemp += 1;
	}

	public void Revert(){
		playerAttack.primaryAttack.setPrimaryAttackLevel = playerMainCannonTemp;
		playerAttack.multiAttack.setMultiAttackLevel = playerMultiCannonTemp;
		playerAttack.disableFusion = false;
	}
}

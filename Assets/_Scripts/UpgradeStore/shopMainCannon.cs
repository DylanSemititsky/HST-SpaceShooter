using UnityEngine;
using System.Collections;

public class ShopMainCannon : MonoBehaviour {

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
	}

	public void ShowPrimaryAttack(){
		playerMultiCannonTemp = playerAttack.multiAttack.setMultiAttackLevel;
		playerAttack.multiAttack.setMultiAttackLevel = 0;
		playerAttack.primaryAttack.setPrimaryAttackLevel += 1;
		playerAttack.disableFusion = true;
	}

	public void EnableUpgrade(){
		playerAttack.primaryAttack.setPrimaryAttackLevel += 1;
		playerMainCannonTemp += 1;
	}

	public void Revert(){
		playerAttack.primaryAttack.setPrimaryAttackLevel = playerMainCannonTemp;
		playerAttack.multiAttack.setMultiAttackLevel = playerMultiCannonTemp;
		playerAttack.disableFusion = false;
	}
}

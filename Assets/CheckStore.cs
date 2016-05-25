using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckStore : MonoBehaviour {

	PlayerController playerController;
	PlayerAttack playerAttack;
	OnToTheNext onToTheNext;

	public GameObject warningText;

	int mainCannon;
	int mainCannonCost;
	int wingCannon;
	int wingCannonCost;
	int health;
	int healthCost;
	int shield;
	int shieldCost;
	int fusionBlast;
	int fusionBlastCost;
	int bomb;
	int bombCost;


	void Start () {
		GameObject playerObject = GameObject.Find ("Player");	
		if (playerObject != null) {
			playerAttack = playerObject.GetComponent<PlayerAttack> ();
			playerController = playerObject.GetComponent<PlayerController> ();
		}

		onToTheNext = GetComponent<OnToTheNext>();

		warningText.SetActive(false);

	}


	public void CheckUpgrades(){
		CheckMainCannon();
		print(mainCannonCost);
		CheckWingCannon();
		print(wingCannonCost);
		CheckHealth();
		print(healthCost);
		CheckShield();
		print(shieldCost);
		CheckFusionBlast();
		print(fusionBlastCost);
		CheckBomb();
		print(bombCost);

		print(playerController.credits);

		if(playerController.credits >= mainCannonCost ||
			playerController.credits >= wingCannonCost ||
			playerController.credits >= healthCost ||
			playerController.credits >= shieldCost ||
			playerController.credits >= fusionBlastCost ||
			playerController.credits >= bombCost){
				warningText.SetActive(true);
			} else onToTheNext.LoadNext();
	}

	public void Yes(){
		onToTheNext.LoadNext();
	}

	public void No(){
		warningText.SetActive(false);
	}


	public void CheckMainCannon(){
		mainCannon = playerAttack.getPrimaryAttack();

		if (mainCannon == 1){
			mainCannonCost = 50;
		}
		else if (mainCannon == 2){
			mainCannonCost = 100;
		}
		else if (mainCannon == 3){
			mainCannonCost = 200;
		} 
	}

	public void CheckWingCannon(){
		wingCannon = playerAttack.getMultiAttack();

		if (wingCannon == 0){
			wingCannonCost = 100;
		}
		else if (wingCannon == 1){
			wingCannonCost = 200;
		}
		else if (wingCannon == 2){
			wingCannonCost = 400;
		} 
	}

	public void CheckHealth(){
		health = playerController.getHealth();

		if (health == 1){
			healthCost = 50;
		}
		else if (health == 2){
			healthCost = 100;
		}
		else if (health == 3){
			healthCost = 150;
		}
		else if (health == 4){
			healthCost = 200;
		}  
	}

	public void CheckShield(){
		shield = playerController.getShield();

		if (shield == 1){
			shieldCost = 50;
		}
		else if (shield == 2){
			shieldCost = 100;
		}
		else if (shield == 3){
			shieldCost = 150;
		}
		else if (shield == 4){
			shieldCost = 200;
		}  
	}

	public void CheckFusionBlast(){
		fusionBlast = playerAttack.getFusionAttack();

		if (fusionBlast == 0){
			fusionBlastCost = 100;
		}
		else if (fusionBlast == 1){
			fusionBlastCost = 200;
		}
	}

	public void CheckBomb(){
		bomb = playerAttack.getBombAttack();

		if (bomb == 0){
			bombCost = 50;
		}
		else if (bomb == 1){
			bombCost = 100;
		}
		else if (bomb == 2){
			bombCost = 150;
		}
		else if (bomb == 3){
			bombCost = 200;
		}  
	}
}

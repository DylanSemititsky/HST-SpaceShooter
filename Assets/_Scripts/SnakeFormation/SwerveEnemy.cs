using UnityEngine;
using System.Collections;

public class SwerveEnemy : MonoBehaviour {

	public float speed;
	public float rotateSpeed;
	public float swerveFrequency;
	private float swerveTimer;
	public bool switchSwerve = false;
	private bool initial;

	void Start () {
		
	}
	
	void Update () {
		
		transform.Translate (Vector3.forward * -speed * Time.deltaTime);//moving forward

		swerveTimer += Time.deltaTime;//timer for swerve frequency
		//print(swerveTimer);


		if (swerveTimer >= 3f && swerveTimer <= 7.25f) {
			transform.Rotate (Vector3.down * rotateSpeed * Time.deltaTime);
		} if (swerveTimer >= 6.5f) {
			transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
		}
		if (swerveTimer >= 10.75f) {
			transform.Rotate (Vector3.down * rotateSpeed * Time.deltaTime);
		}


		/*
		if (switchSwerve == true) {
			transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
			if(swerveTimer >= swerveFrequency) //once serve frequency 
			{
				switchSwerve = false;
				swerveTimer = 0;
			}
		} else if (switchSwerve == false) {
			transform.Rotate (Vector3.down * rotateSpeed * Time.deltaTime);

				if(swerveTimer >=swerveFrequency )
				{
					switchSwerve = true;
					swerveTimer = 0;
				}
			}*/
		}
}

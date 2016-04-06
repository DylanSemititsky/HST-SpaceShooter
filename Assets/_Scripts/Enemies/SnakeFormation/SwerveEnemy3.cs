using UnityEngine;
using System.Collections;

public class SwerveEnemy3 : MonoBehaviour {

	public float speed;
	public float rotateSpeed;
	public float swerveFrequency;
	private float swerveTimer;
	public bool switchSwerve = false;
	private bool initial;

	void Start () {
		transform.eulerAngles = new Vector3(0,110,0);
	}

	void Update () {

		transform.Translate (Vector3.forward * -speed * Time.deltaTime);//moving forward

		swerveTimer += Time.deltaTime;//timer for swerve frequency
		print(swerveTimer);


		if (swerveTimer >= 1.5f && swerveTimer <= 4.25f) {
			transform.Rotate (Vector3.down * rotateSpeed * Time.deltaTime);
		} if (swerveTimer > 4.5f) {
			transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
		}
		if (swerveTimer >= 8.25f) {
			transform.Rotate (Vector3.down * rotateSpeed * Time.deltaTime);
		}
	}
}

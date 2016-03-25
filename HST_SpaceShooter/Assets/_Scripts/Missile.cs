using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class Missile : MonoBehaviour {
    public List <Transform> Enemies;
    public Transform SelectedTarget; 
    //private Rigidbody rb;
	public float tilt;
	public float speed; 

    void Start () 
    {
        SelectedTarget = null;
        Enemies = new List<Transform>();
        AddEnemiesToList();
		//rb = GetComponent<Rigidbody>();
    }
 
    public void AddEnemiesToList()
    {
        GameObject[] ItemsInList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject _Enemy in ItemsInList)
        {
            AddTarget(_Enemy.transform);
        }
    }

    public void AddTarget(Transform enemy)
    {
        Enemies.Add(enemy);
    }
 
    public void DistanceToTarget()
    {
        Enemies.Sort(delegate( Transform t1, Transform t2){ 
            return Vector3.Distance(t1.transform.position,transform.position).CompareTo(Vector3.Distance(t2.transform.position,transform.position)); 
        });
    }

    public void TargetedEnemy() 
    {
        if(SelectedTarget == null)
        {
            DistanceToTarget();
            SelectedTarget = Enemies[0];
        }
	}
	void Update () {
        TargetedEnemy();
        float dist = Vector3.Distance(SelectedTarget.transform.position,transform.position);
		if (dist < 150) {
			transform.position = Vector3.MoveTowards (transform.position, SelectedTarget.position, 10 * Time.deltaTime);
		} 	
		else {
			transform.Translate(0, 0, Time.deltaTime);
			}

		//rb.rotation = Quaternion.Euler (0.0f, rb.velocity.x * tilt, 0.0f);
 	}
 }
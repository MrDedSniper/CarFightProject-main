using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBreakScript : MonoBehaviour
{

	//[SerializeField] private CarControls _carControls;
	
	public GameObject unbrokenColumn;
	public GameObject brokenColumn;

	//this determines whether the column will be broken or unbroken at the at runtime
	public bool isBroken;
	
	void Start()
	{
		if (isBroken) 
		{
			BreakColumn ();
		} 
		else 
		{
			unbrokenColumn.SetActive (true);
			brokenColumn.SetActive (false);
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("PlayerCar") || collision.gameObject.CompareTag("Projectile"))
		{
			BreakColumn();
		}
	}

	void BreakColumn()
	{
		isBroken = true;
		unbrokenColumn.SetActive (false);
		brokenColumn.SetActive (true);
	}

}
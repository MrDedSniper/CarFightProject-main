using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBreakScript : MonoBehaviour
{
	[SerializeField] private GameObject _unbrokenColumn;
	[SerializeField] private GameObject _brokenColumn;

	[SerializeField] private bool _isBroken;

	[SerializeField] private int _columnHp = 15;
	
	private void Start()
	{
		if (!_isBroken)
		{
			_columnHp = 15;
			_unbrokenColumn.SetActive (true);
			_brokenColumn.SetActive (false);
		} 
		else if (_isBroken)
		{
			_columnHp = 0;
			BreakColumn ();
		}
	}

	/*private void Update()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			TakeDamage(1);
		}
	}*/
	
	internal void TakeDamage(int damage)
	{
		_columnHp -= damage;
		if (_columnHp <= 0)
		{
			BreakColumn();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("PlayerCar"))
		{
			BreakColumn();
		}
	}

	private void BreakColumn()
	{
		_isBroken = true;
		_unbrokenColumn.SetActive (false);
		_brokenColumn.SetActive (true);
	}
}
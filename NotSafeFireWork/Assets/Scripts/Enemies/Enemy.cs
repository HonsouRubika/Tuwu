using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[Header("Params")]
	[SerializeField] int maxHealthPoints;
	int currentHealthPoints;
	public int CurrentHealthPoints { get => currentHealthPoints;}

	internal virtual void Start()
	{
		Debug.Log("eusifhs");
		currentHealthPoints = maxHealthPoints;
	}

	public abstract void DealDamage(int _damages);

	internal void Hit(int _damages)
	{
		if(currentHealthPoints - _damages <= 0)
		{
			Death();
		}
		else
		{
			currentHealthPoints -= _damages;
		}
	}

	void Death()
	{
		Destroy(gameObject);
	}
}

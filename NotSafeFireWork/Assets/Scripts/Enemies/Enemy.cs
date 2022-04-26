using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[Header("Params")]
	[SerializeField] int maxHealthPoints;
	int currentHealthPoints;
	public int CurrentHealthPoints { get => currentHealthPoints;}

	bool stunned = false;
	public bool Stunned { get => stunned;}
	[SerializeField] float stunDuration = 1.5f;
	Clock stunTimer;

	internal void Init()
	{
		currentHealthPoints = maxHealthPoints;
		stunTimer = new Clock();
		stunTimer.ClockEnded += OnStunTimerEnded;
	}

	#region Health system

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

	#endregion

	public void Stun()
	{
		stunned = true;
		stunTimer.SetTime(stunDuration);
	}

	private void OnStunTimerEnded()
	{
		stunned = false;
	}
}
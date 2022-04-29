using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[Header("Params")]
	public int maxHealthPoints;
	int currentHealthPoints;
	public int CurrentHealthPoints { get => currentHealthPoints;}

	bool stunned = false;
	public bool Stunned { get => stunned;}
	public float stunDuration = 1.5f;
	Clock stunTimer;

	bool hitStunned = false;
	public bool HitStunned { get => hitStunned; }
	public float hitStunDuration = .2f;
	Clock hitStunTimer;

	public int enemyScore = 10;

	//sounds
	[HideInInspector]
	public SoundManager soundManager;
	protected void Init()
	{
		currentHealthPoints = maxHealthPoints;
		stunTimer = new Clock();
		stunTimer.ClockEnded += OnStunTimerEnded;
		hitStunTimer = new Clock();
		hitStunTimer.ClockEnded += OnHitStunTimerEnded;
		soundManager = SoundManager.Instance;
	}

	#region Health system

	public abstract void DealDamage(int _damages);

	protected void Hit(int _damages)
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
		LevelHandler.Instance.RoomClearFeedback();
		AddScore();
		Destroy(gameObject);
	}

	void AddScore()
    {
		GameManager.Instance.playerScore += enemyScore;
	}

	#endregion

	#region Stuns

	public virtual void Stun()
	{
		stunned = true;
		stunTimer.SetTime(stunDuration);
	}

	public void HitStun()
	{
		hitStunned = true;
		hitStunTimer.SetTime(hitStunDuration);
	}

	private void OnStunTimerEnded()
	{
		stunned = false;
	}

	private void OnHitStunTimerEnded()
	{
		hitStunned = false;
	}

	#endregion
}
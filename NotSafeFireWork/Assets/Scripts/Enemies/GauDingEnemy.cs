using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class GauDingEnemy : Enemy
{
	[Header("Params")]
	[SerializeField] float moveSpeed;
	[Space]
	[SerializeField] float minDistanceWithPlayers;
	[SerializeField] float maxDistanceWithPlayers;
	[Space]
	[SerializeField] Transform leftBorder;
	[SerializeField] Transform rightBorder;
	[SerializeField] Transform topBorder;
	[SerializeField] Transform bottomBorder;
	[Space]
	[SerializeField] float lionCooldown;
	[SerializeField] float cracklingCooldown;
	[SerializeField] float shotgunCooldown;
	[SerializeField] float splitterCooldown;

	[Header("References")]
	[SerializeField] BulletEmitter lionEmitter;
	[SerializeField] BulletEmitter cracklingEmitter;
	[SerializeField] BulletEmitter shotgunEmitter;
	[SerializeField] BulletEmitter splitterEmitter;

	Transform player1, player2;
	public GauDingAttackState attackState = GauDingAttackState.Crackling;
	HorizontalMoveDir moveDir = HorizontalMoveDir.Left;

	bool canAttack = true;
	Clock attackTimer;

	bool isInit = false;

	//animations
	public Animator animator;
	public override void DealDamage(int _damages)
	{
		Hit(_damages);
		if(CurrentHealthPoints < maxHealthPoints * .5f)
		{
			NextState();
		}
	}

	private void Start()
	{
		Init();
		attackTimer = new Clock();
		attackTimer.ClockEnded += OnAttackCooldownEnded;
	}

	private void Update()
	{
		if (!isInit)
		{
			if (GameManager.Instance.playerControllers != null)
			{
				if (GameManager.Instance.playerControllers.Count == 2)
				{
					player1 = GameManager.Instance.playerControllers[0].transform;
					player2 = GameManager.Instance.playerControllers[1].transform;
					isInit = true;
				}
			}
			return;
		}

		if (Stunned || HitStunned)
			return;

		if (Move())
			Attack();
	}

	bool Move()
	{
		if (Mathf.Abs(transform.position.y - player1.transform.position.y) < minDistanceWithPlayers ||
			Mathf.Abs(transform.position.y - player2.transform.position.y) < minDistanceWithPlayers)
		{
			moveDir = HorizontalMoveDir.Left;

			if(transform.position.y < topBorder.position.y)
			{
				transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
			}

			return false;
		}

		if (Mathf.Abs(transform.position.y - player1.transform.position.y) > maxDistanceWithPlayers ||
			Mathf.Abs(transform.position.y - player2.transform.position.y) > maxDistanceWithPlayers)
		{
			moveDir = HorizontalMoveDir.Left;

			if(transform.position.y > bottomBorder.position.y)
			{
				transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
			}

			return false;
		}

		switch(moveDir)
		{
			case HorizontalMoveDir.Left:
				if (transform.position.x > leftBorder.position.x)
				{
					transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
				}
				else
				{
					moveDir = HorizontalMoveDir.Right;
				}
				break;

			case HorizontalMoveDir.Right:
				if(transform.position.x < rightBorder.position.x)
				{
					transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
				}
				else
				{
					moveDir = HorizontalMoveDir.Left;
				}
				break;

			default:
				throw new System.ArgumentException("case not implemented");
		}

		return true;

	}

	void Attack()
	{
		if (!canAttack)
			return;

		canAttack = false;

		switch (attackState)
		{
			case GauDingAttackState.Lion:
				attackTimer.SetTime(lionCooldown);
				lionEmitter.Play();
				break;
			case GauDingAttackState.Crackling:
				attackTimer.SetTime(cracklingCooldown);
				cracklingEmitter.Play();
				break;
			case GauDingAttackState.Shotgun:
				attackTimer.SetTime(shotgunCooldown);
				shotgunEmitter.Play();
				break;
			case GauDingAttackState.Splitter:
				attackTimer.SetTime(splitterCooldown);
				splitterEmitter.Play();
				break;
			default:
				break;
		}
	}

	public override void Stun()
	{
		base.Stun();
		NextState();
	}

	void NextState()
	{
		switch (attackState)
		{
			case GauDingAttackState.Lion:
				attackState = GauDingAttackState.Crackling;
				break;
			case GauDingAttackState.Crackling:
				attackState = GauDingAttackState.Shotgun;
				break;
			case GauDingAttackState.Shotgun:
				attackState = GauDingAttackState.Splitter;
				break;
			case GauDingAttackState.Splitter:
				attackState = GauDingAttackState.Lion;
				break;
			default:
				break;
		}
	}

	void OnAttackCooldownEnded()
	{
		canAttack = true;
	}

	public void OnHitByBullet(Bullet _bullet, Vector3 _position)
	{
		DealDamage((int)_bullet.moduleParameters.GetFloat("_PowerLevel"));
	}

	enum HorizontalMoveDir
	{
		Left,
		Right
	}

	public enum GauDingAttackState
	{
		Lion,
		Crackling,
		Shotgun,
		Splitter
	}
}

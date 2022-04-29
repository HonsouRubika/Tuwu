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
	[SerializeField] GameObject emitterObject;
	[SerializeField] BulletEmitter lionEmitter;
	[SerializeField] BulletEmitter cracklingEmitter;
	[SerializeField] BulletEmitter shotgunEmitter;
	[SerializeField] BulletEmitter splitterEmitter;

	Transform player1, player2;
	Transform target;
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
		if (CurrentHealthPoints <= 0)
		{
			animator.SetBool("isDeath", true);
		}
	}

	private void Start()
	{
		Init();
		attackState = (GauDingAttackState)Random.Range(0, 4);
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
        {
			//degats
            if (Stunned)
            {
				animator.SetTrigger("Hurt");
            }

        }

			Attack();
	}

	void Attack()
	{
		if (!canAttack)
			return;

		canAttack = false;

		int ran = Random.Range(0, 2);
		if (ran == 0)
			target = player1;
		else
			target = player2;

		animator.SetTrigger("Attack");

		switch (attackState)
		{
			case GauDingAttackState.Lion:
				attackTimer.SetTime(lionCooldown);
				emitterObject.transform.parent.up = emitterObject.transform.parent.position - target.position;
				lionEmitter.Play();
				break;
			case GauDingAttackState.Crackling:
				attackTimer.SetTime(cracklingCooldown);
				emitterObject.transform.parent.up = emitterObject.transform.parent.position - target.position;
				cracklingEmitter.Play();
				break;
			case GauDingAttackState.Shotgun:
				attackTimer.SetTime(shotgunCooldown);
				emitterObject.transform.parent.up = emitterObject.transform.parent.position - target.position;
				shotgunEmitter.Play();
				break;
			case GauDingAttackState.Splitter:
				attackTimer.SetTime(splitterCooldown);
				emitterObject.transform.parent.up = emitterObject.transform.parent.position - target.position;
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
				animator.SetInteger("Current", 0);
				animator.SetTrigger("Swap");
				attackState = GauDingAttackState.Crackling;
				break;
			case GauDingAttackState.Crackling:
				animator.SetInteger("Current", 1);
				animator.SetTrigger("Swap");
				attackState = GauDingAttackState.Shotgun;
				break;
			case GauDingAttackState.Shotgun:
				animator.SetInteger("Current", 2);
				animator.SetTrigger("Swap");
				attackState = GauDingAttackState.Splitter;
				break;
			case GauDingAttackState.Splitter:
				animator.SetInteger("Current", 3);
				animator.SetTrigger("Swap");
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

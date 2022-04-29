using UnityEngine;
using BulletPro;

public class LionEnemy : Enemy
{
	Transform target;
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float minDistance = 5f;
	[SerializeField] float maxDistance = 6f;
	[SerializeField] float attackCooldown = 1f;
	Clock attackCooldownTimer;
	public Animator animator;
	[Space]
	[SerializeField] Transform leftBorder;
	[SerializeField] Transform rightBorder;
	[SerializeField] Transform topBorder;
	[SerializeField] Transform bottomBorder;

	BulletEmitter emitter;

	bool canAttack = true;

	public override void DealDamage(int _damages)
	{
		//Change 400 with tier 1 damages
		if (_damages < 400)
		{
			soundManager.PlaySFX("armorResist", soundManager.fxSource);
			
		}
        else
        {
			Hit(_damages);
			soundManager.PlaySFX("lionHurt", soundManager.fxSource);
		}
	}

	private void Start()
	{
		Init();
		emitter = GetComponent<BulletEmitter>();
		target = GameManager.Instance.playerControllers[Random.Range(0, 2)].transform;
		attackCooldownTimer = new Clock();
		attackCooldownTimer.ClockEnded += OnAttackCooldownEnded;
	}

	private void Update()
	{
		if (Stunned || HitStunned)
		{
            if (Stunned)
			{
				animator.SetBool("isStun", true);
			}
			else if (HitStunned)
			{
				animator.SetBool("isHit", true);
			}
		}
        else
        {
			animator.SetBool("isStun", false);
			animator.SetBool("isHit", false);
        }

		float _targetDistance = Vector2.Distance(target.position, transform.position);

		if (_targetDistance > maxDistance)
		{
			MoveTowardsTargetPlayer();
			animator.SetBool("isMove", true);
			animator.SetBool("isFire", false);
		}
		else if(_targetDistance < minDistance)
		{
			FleeTargetPlayer();
			animator.SetBool("isMove", true);
			animator.SetBool("isFire", false);
		}
		else if(canAttack)
		{
			canAttack = false;
			attackCooldownTimer.SetTime(attackCooldown);

			soundManager.PlaySFX("shootEnemies", soundManager.fxSource);
			animator.SetBool("isMove", false);
			animator.SetBool("isFire", true);
		}

		CheckBorders();
	}

	private void CheckBorders()
	{
		Vector2 _position = transform.position;

		if (_position.x <= leftBorder.position.x)
			transform.position = new Vector2(leftBorder.position.x, transform.position.y);

		if (_position.x >= rightBorder.position.x)
			transform.position = new Vector2(rightBorder.position.x, transform.position.y);

		if (_position.x <= bottomBorder.position.y)
			transform.position = new Vector2(transform.position.x, bottomBorder.position.y);

		if (_position.x >= topBorder.position.y)
			transform.position = new Vector2(transform.position.x, topBorder.position.y);
	}

	void MoveTowardsTargetPlayer()
	{
		transform.Translate((target.position - transform.position).normalized * Time.deltaTime * moveSpeed);
	}

	void FleeTargetPlayer()
	{
		transform.Translate((transform.position - target.position).normalized * Time.deltaTime * moveSpeed);
	}

	void OnAttackCooldownEnded()
	{
		canAttack = true;
	}

	public void OnHitByBullet(Bullet _bullet, Vector3 _position)
	{
		DealDamage((int)_bullet.moduleParameters.GetFloat("_PowerLevel"));
	}
}
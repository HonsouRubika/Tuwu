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
	[SerializeField] SpriteRenderer sr;
	[Space]
	[SerializeField] Transform leftBorder;
	[SerializeField] Transform rightBorder;
	[SerializeField] Transform topBorder;
	[SerializeField] Transform bottomBorder;

	[SerializeField] BulletEmitter emitter;

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
		target = GameManager.Instance.playerControllers[Random.Range(0, 2)].transform;
		attackCooldownTimer = new Clock();
		attackCooldownTimer.ClockEnded += OnAttackCooldownEnded;
	}

	private void Update()
	{
		if (GameManager.Instance.playerControllers == null)
			return;
		if (GameManager.Instance.playerControllers.Count != 2)
			return;

		if(target == null)
			target = GameManager.Instance.playerControllers[Random.Range(0, 2)].transform;

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

			emitter.transform.parent.up = emitter.transform.parent.position - target.position;
			emitter.Play();

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
		UpdateSpriteFlipX();
	}

	void FleeTargetPlayer()
	{
		transform.Translate((transform.position - target.position).normalized * Time.deltaTime * moveSpeed);
		UpdateSpriteFlipX();
	}

	void UpdateSpriteFlipX()
	{
		if((transform.position - target.position).x > 0f)
		{
			sr.flipX = false;
		}
		else if ((transform.position - target.position).x < 0f)
		{
			sr.flipX = true;
		}
	}

	void OnAttackCooldownEnded()
	{
		canAttack = true;
	}

	public void OnHitByBullet(Bullet _bullet, Vector3 _position)
	{
		DealDamage((int)_bullet.moduleParameters.GetFloat("_PowerLevel"));
		if(_bullet != null)
			_bullet.Die();
	}
}
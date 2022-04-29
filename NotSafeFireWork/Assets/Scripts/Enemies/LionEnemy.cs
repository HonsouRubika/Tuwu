using UnityEngine;

public class LionEnemy : Enemy
{
	Transform target;
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float minDistance = 5f;
	[SerializeField] float maxDistance = 6f;
	public Animator animator;


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
		else
		{
			//Shoot at target player
			Debug.Log("boom");
			soundManager.PlaySFX("shootEnemies", soundManager.fxSource);
			animator.SetBool("isMove", false);
			animator.SetBool("isFire", true);
		}
	}

	void MoveTowardsTargetPlayer()
	{
		transform.Translate((target.position - transform.position).normalized * Time.deltaTime * moveSpeed);
	}

	void FleeTargetPlayer()
	{
		transform.Translate((transform.position - target.position).normalized * Time.deltaTime * moveSpeed);
	}
}
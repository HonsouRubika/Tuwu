using UnityEngine;

public class LionEnemy : Enemy
{
	Transform target;
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float minDistance = 5f;
	[SerializeField] float maxDistance = 6f;


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
			return;

		float _targetDistance = Vector2.Distance(target.position, transform.position);

		if (_targetDistance > maxDistance)
		{
			MoveTowardsTargetPlayer();
		}
		else if(_targetDistance < minDistance)
		{
			FleeTargetPlayer();
		}
		else
		{
			//Shoot at target player
			soundManager.PlaySFX("shootEnemies", soundManager.fxSource);
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
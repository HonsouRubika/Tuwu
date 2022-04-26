using UnityEngine;

public class LionEnemy : Enemy
{
	Transform target;
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float triggerDistance = 5f;

	public override void DealDamage(int _damages)
	{
		//Change 400 with tier 1 damages
		if (_damages < 400)
			return;

		Hit(_damages);
	}

	private void Start()
	{
		Init();
		target = GameManager.Instance.playerControllers[Random.Range(0, 2)].transform;
	}

	private void Update()
	{
		if (Stunned)
			return;

		if (Vector2.Distance(target.position, transform.position) > triggerDistance)
		{
			MoveTowardsTargetPlayer();
		}
		else
		{
			//Shoot at target player
		}
	}

	void MoveTowardsTargetPlayer()
	{
		transform.Translate((target.position - transform.position).normalized * Time.deltaTime * moveSpeed);
	}
}
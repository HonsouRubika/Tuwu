using UnityEngine;
using BulletPro;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	[Header("Params")]
	[SerializeField] float attackLifeTime;
	Clock attackLifeTimer;

	[SerializeField] float attackCooldown;
	Clock attackCooldownTimer;
	bool canAttack = true;

	[Header("References")]
	[SerializeField] BulletReceiver bulletReceiver;
	[SerializeField] CircleCollider2D stunCollider;

	private void Start()
	{
		attackCooldownTimer = new Clock();
		attackCooldownTimer.ClockEnded += OnAttackCooldownEnded;

		attackLifeTimer = new Clock();
		attackLifeTimer.ClockEnded += OnAttackLifeEnded;
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (!context.started)
			return;

		if (!canAttack)
			return;

		bulletReceiver.enabled = true;
		stunCollider.enabled = true;

		canAttack = false;
		attackLifeTimer.SetTime(attackLifeTime);
		attackCooldownTimer.SetTime(attackCooldown);
	}

	void OnAttackCooldownEnded()
	{
		canAttack = true;
	}

	void OnAttackLifeEnded()
	{
		bulletReceiver.enabled = false;
		stunCollider.enabled = false;
	}

	public void OnHitByBullet(Bullet bullet, Vector3 position)
	{
		//change bullet dir
	}
}

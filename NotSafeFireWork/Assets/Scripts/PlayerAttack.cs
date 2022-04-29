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

	//sounds
	SoundManager soundManager;
	private void Start()
	{
		soundManager = SoundManager.Instance;
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
		soundManager.PlaySFX("swingBat", soundManager.fxSource);
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
		EmitterProfile _profile = bullet.emitter.emitterProfile;
		bullet.Die();
		BulletEmitter _emitter = gameObject.AddComponent<BulletEmitter>();
		_emitter.emitterProfile = _profile;
		_emitter.Play();
		//Destroy(_emitter);
		//bullet.moduleMovement.Rotate()
		//change bullet dir
	}
}

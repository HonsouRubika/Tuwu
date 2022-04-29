using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;


/// <summary>
/// 100 a 1000
/// 1 pallier tout les 300
/// 
/// </summary>
// This script is supported by the BulletPro package for Unity.
// Template author : Simon Albou <albou.simon@gmail.com>

// This script is actually a MonoBehaviour for coding advanced things with Bullets.
public class SoundExplosion : BaseBulletBehaviour {

	// You can access this.bullet to get the parent bullet script.
	// After bullet's death, you can delay this script's death : use this.lifetimeAfterBulletDeath.

	// Use this for initialization (instead of Start)

	bool didCollide = false;

	public override void OnBulletBirth ()
	{
		base.OnBulletBirth();

		// Your code here
	}
	
	// Update is (still) called once per frame
	public override void Update ()
	{
		base.Update();

		// Your code here
	}

	// This gets called when the bullet dies
	public override void OnBulletDeath()
	{
		base.OnBulletDeath();

		if(!didCollide)
		{
			for (int i = 2; i < bullet.moduleVFX.availableVFX.Count; i++)
			{
				bullet.moduleVFX.PlayVFX(i);
			}
		}
	}

	// This gets called after the bullet has died, it can be delayed.
	public override void OnBehaviourDeath()
	{
		base.OnBehaviourDeath();

		// Your code here
	}

	// This gets called whenever the bullet collides with a BulletReceiver. The most common callback.
	public override void OnBulletCollision(BulletReceiver br, Vector3 collisionPoint)
	{
		base.OnBulletCollision(br, collisionPoint);

		didCollide = true;

		if (br.tag != "PlayerAttack")
        {
			
			for (int i = 2; i< bullet.moduleVFX.availableVFX.Count; i++)
            {
				bullet.moduleVFX.PlayVFX(i);
			}


			float power = bullet.moduleParameters.GetFloat("_PowerLevel");
            if (power >= 1000)
            {
                SoundManager.Instance.PlaySFX("fwExplosionStrong", SoundManager.Instance.fwSource);
            }
            else if (power >= 700)
            {
                SoundManager.Instance.PlaySFX("fwExplosionStrong", SoundManager.Instance.fwSource);
            }
            else if (power >= 400)
            {
                SoundManager.Instance.PlaySFX("fwExplosionMedium", SoundManager.Instance.fwSource);
            }
            else if (power >= 100)
            {
                SoundManager.Instance.PlaySFX("fwExplosionWeak", SoundManager.Instance.fwSource);
            }
        }
        // Your code here
    }

	// This gets called whenever the bullet collides with a BulletReceiver AND was not colliding during the previous frame.
	public override void OnBulletCollisionEnter(BulletReceiver br, Vector3 collisionPoint)
	{
		base.OnBulletCollisionEnter(br, collisionPoint);

		// Your code here
	}

	// This gets called whenever the bullet stops colliding with any BulletReceiver.
	public override void OnBulletCollisionExit()
	{
		base.OnBulletCollisionExit();

		// Your code here
	}

	// This gets called whenever the bullet shoots a pattern.
	public override void OnBulletShotAnotherBullet(int patternIndex)
	{
		base.OnBulletShotAnotherBullet(patternIndex);

		// Your code here
	}
}

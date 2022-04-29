using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

// This script is supported by the BulletPro package for Unity.
// Template author : Simon Albou <albou.simon@gmail.com>

// This script is actually a MonoBehaviour for coding advanced things with Bullets.
public class ExplosionAndCollider : BaseBulletBehaviour {

	// You can access this.bullet to get the parent bullet script.
	// After bullet's death, you can delay this script's death : use this.lifetimeAfterBulletDeath.

	public bool isBulletActive = false;

	public ParticleSystem ps;
	public float[] timeSincePlayerIn;
	public float nbSecoundForDamages = 2f;
	public PlayerController[] pc;

	// Use this for initialization (instead of Start)
	public override void OnBulletBirth ()
	{
		base.OnBulletBirth();

		isBulletActive = true;

		ps = GetComponent<ParticleSystem>();
		ps.Play();

		//init var
		pc = new PlayerController[2];
		timeSincePlayerIn = new float[2];
	}

    /*public void OnTriggerEnter(Collider other)
    {
        *//*if (isBulletActive && other.tag == "Player")
        {
			PlayerController _pc = other.gameObject.GetComponent<PlayerController>();
			if (_pc.isPlayerA)
            {
				pc[0] = _pc;
				timeSincePlayerIn[0] = 0;
			}
            else
			{
				pc[1] = _pc;
				timeSincePlayerIn[1] = 0;
			}
        }*//*
    }

    public void OnTriggerStay(Collider other)
    {
		*//*if (isBulletActive && other.tag == "Player")
		{
			if (pc[0] != null)
			{
				timeSincePlayerIn[0] += Time.deltaTime;
                if (timeSincePlayerIn[0] > nbSecoundForDamages)
				{

					//TODO: Inflict Damages to player 1
					Debug.Log("player 1 takes X damages");

					timeSincePlayerIn[0] = 0;
				}
			}
			if (pc[1] != null)
			{
				timeSincePlayerIn[1] = Time.deltaTime;
				if (timeSincePlayerIn[1] > nbSecoundForDamages)
				{

					//TODO: Inflict Damages to player 2
					Debug.Log("player 2 takes X damages");

					timeSincePlayerIn[1] = 0;
				}
			}
		}*//*
	}

    public void OnTriggerExit(Collider other)
    {
		*//*if (isBulletActive && other.tag == "Player")
		{
			Debug.Log("oui");
			PlayerController _pc = other.gameObject.GetComponent<PlayerController>();
			if (_pc.isPlayerA)
			{
				pc[0] = null;
			}
			else
			{
				pc[1] = null;
			}
		}*//*
	}*/

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

		// Your code here
		ps.Stop();
		isBulletActive = false;
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

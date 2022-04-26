using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionEnemy : Enemy
{
	public override void DealDamage(int _damages)
	{
		//Change 400 with tier 1 damages
		if (_damages < 400)
			return;

		Hit(_damages);
	}

	internal override void Start()
	{
		base.Start();
	}

	private void Update()
	{
		
	}
}